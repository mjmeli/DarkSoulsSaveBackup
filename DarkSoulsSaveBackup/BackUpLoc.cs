using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSoulsSaveBackup
{
    class BackUpLoc
    {
        private string folderPath;
        private int numBackedUp;
        private double fileSize;

        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        public int NumBackedUp
        {
            get { return numBackedUp; }
            set { numBackedUp = value; }
        }

        public double FileSize
        {
            get { return fileSize; }
        }

        // constructor adds \Backups to the end of the specified string pathname
        public BackUpLoc(string s)
        {
            folderPath = s + @"\Backups";
            this.populateBUs();
        }

        // checks current path for any existing backup files
        public void populateBUs()
        {
            System.IO.Directory.CreateDirectory(folderPath);
            string[] files = System.IO.Directory.GetFiles(folderPath, "*.sl2");
            this.numBackedUp = files.Length;
            
            this.fileSize = 0;
            
            if (this.numBackedUp > 0)
            {
                foreach (string s in files)
                {
                    fileSize += new System.IO.FileInfo(s).Length / 1000000;
                }
            }
        }

        // emptys the current directory of .sl2 files
        public void emptyLoc()
        {
            string[] files = System.IO.Directory.GetFiles(folderPath, "*.sl2");

            foreach (string s in files)
            {
                System.IO.File.Delete(s);
            }

            populateBUs();
        }

        // determines number of each backup for each file in a SaveLoc
        public void findBUs(SaveLoc saveLoc)
        {
            string[] files = System.IO.Directory.GetFiles(folderPath, "*.sl2");

            foreach (SaveFile save in saveLoc.SaveFiles)
            {
                save.BackupCount = 0;
                save.FirstBU = Int32.MaxValue;
                save.LastBU = Int32.MinValue;
                foreach (string str in files)
                {
                    if (str.Contains(save.FileName))
                    {
                        save.BackupCount++;

                        int start = str.IndexOf('(') + 1;
                        int end = str.IndexOf(')');
                        int fileNum = Convert.ToInt32(str.Substring(start, end - start));

                        if (fileNum < save.FirstBU)
                        {
                            save.FirstBU = fileNum;
                        }
                        if (fileNum > save.LastBU)
                        {
                            save.LastBU = fileNum;
                        }
                    }
                }
            }
        }
    }
}
