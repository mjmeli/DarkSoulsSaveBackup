using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DS2SaveBackup
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

        public SaveLoc()
        {
            if (Properties.Settings.Default.saveDir == "" || Properties.Settings.Default.saveDir == @"C:\Users\Michael\AppData\Roaming\DarkSoulsII\0110000103c637eb")
            {
                this.getDefSaveLocation();
            }
            else
            {
                validatePath(Properties.Settings.Default.saveDir);
                if (validLoc)
                {
                    folderPath = Properties.Settings.Default.saveDir;
                }
            }

            if (validLoc)
            {
                this.populateFiles();
            }
        }

        // gets the default save location
        // checks to see if the default location exists and picks it, otherwise will set validPath to false
        private void getDefSaveLocation()
        {
            this.folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\DarkSoulsII";
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
                Properties.Settings.Default.saveDir = folderPath;
                Properties.Settings.Default.Save();
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
                Properties.Settings.Default.saveDir = "";
                Properties.Settings.Default.Save();
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
                    Properties.Settings.Default.saveDir = "";
                    Properties.Settings.Default.Save();
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
