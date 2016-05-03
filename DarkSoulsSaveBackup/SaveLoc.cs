﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkSoulsSaveBackup
{
    class SaveLoc
    {
        private string folderPath;
        private List<SaveFile> saveFiles = new List<SaveFile>();
        private bool validLoc;

        public bool ValidLoc
        {
            get { return validLoc; }
            set { validLoc = value; }
        }

        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        public List<SaveFile> SaveFiles
        {
            get { return saveFiles; }
            set { saveFiles = value; }
        }

        /// <summary>
        /// When SaveLoc is instantiated, set it to point to the default save location, and if that is
        /// a valid location, populate the save file list.
        /// </summary>
        public SaveLoc()
        {
            this.getDefSaveLocation();

            if (validLoc)
            {
                this.populateFiles();
            }
        }

        /// <summary>
        /// Gets the default save location on the system. This attempts to find the folder for the **latest** Dark
        /// Souls game. In other words, it will look in the order DS3 -> DS2 -> DS1. If no default save locations
        /// exist, returns error.
        /// </summary>
        private void getDefSaveLocation()
        {
            this.folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DarkSoulsIII";
            this.validLoc = false;

            // verify the default path exists
            if (System.IO.Directory.Exists(folderPath))
            {
                string[] fpDirs = System.IO.Directory.GetDirectories(folderPath);

                switch (fpDirs.Length)
                {
                    case 0:
                        folderPath = folderPath = Environment.CurrentDirectory;        // no save folders exist - will handle later
                        break;
                    case 1:
                        folderPath = fpDirs[0];
                        break;
                    default:
                        // get the most recently accessed folder, and tell the user we did this
                        DateTime newTime = new DateTime(1980, 1, 1);
                        string newDir = "";
                        foreach (string s in fpDirs)
                        {
                            DateTime lastAccessed = System.IO.Directory.GetLastWriteTime(s);

                            if (lastAccessed > newTime)
                            {
                                newTime = lastAccessed;
                                newDir = s;
                            }
                        }
                        folderPath = newDir;
                        MessageBox.Show("We detected more than one possible save folder in the default directory. Automatically choosing the most recently modified one.", "Multiple Folders Detected");
                        break;
                }
            }
            else
            {
                folderPath = Environment.CurrentDirectory;
            }

            // ensure save files exist
            string[] files = System.IO.Directory.GetFiles(folderPath, "*.sl2");
            if (files.Length > 0)
            {
                validLoc = true;
            }
        }

        // populates the save file list property
        public void populateFiles()
        {
            string[] files = System.IO.Directory.GetFiles(folderPath, "*.sl2");

            saveFiles.Clear();

            if (files.Length == 0)
            {
                validLoc = false;
                MessageBox.Show("No save files could be found in that directory. Try again.", "No Save Files");
            }
            else
            {
                foreach (string f in files)
                {
                    saveFiles.Add(new SaveFile(System.IO.Path.GetFileName(f)));
                }
            }
        }

        // checks to see if the path has save files in it
        public bool validatePath(string path)
        {
            validLoc = false;

            // verify the path contains save files
            if (System.IO.Directory.Exists(path))
            {
                string[] files = System.IO.Directory.GetFiles(path, "*.sl2");

                if (files.Length == 0)
                {
                    MessageBox.Show("No save files could be found in that directory. Try again.", "No Save Files");
                    return false;
                }
                else
                {
                    validLoc = true;
                    return true;
                }
            }

            return false;
        }
    }
}
