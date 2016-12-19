using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2SaveBackup
{
    class SaveFile
    {
        private string fileName;
        
        public string FileName
        {
            get { return fileName; }
        }

        private int backupCount;

        public int BackupCount
        {
            get { return backupCount; }
            set { backupCount = value; }
        }

        private int firstBU;

        public int FirstBU
        {
            get { return firstBU; }
            set { firstBU = value; }
        }

        private int lastBU;

        public int LastBU
        {
            get { return lastBU; }
            set { lastBU = value; }
        }

        // creates a save file with the specified file name
        public SaveFile(string s)
        {
            this.fileName = s;
            this.backupCount = 0;
            this.firstBU = Int32.MaxValue;
            this.lastBU = Int32.MinValue;
        }
    }
}
