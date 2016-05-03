/* DS2 Save Backup
 * Main functionality of the program is contained in here.
 * 
 * Meli
 * May 2014
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkSoulsSaveBackup
{
    public partial class DarkSoulsSaveBackup : Form
    {
        private SaveLoc saveLoc;
        private BackUpLoc buLoc;
        private bool backingUp;
        private bool restoreWarned;

        public DarkSoulsSaveBackup()
        {
            InitializeComponent();
            restoreWarned = false;
        }

        // executes when the app is loaded
        // populates default save location, backup location, save files (if applicable), etc.
        private void DS2SB_OnShow(object sender, EventArgs e)
        {
            backingUp = false;
            saveLoc = new SaveLoc();
            buLoc = new BackUpLoc(saveLoc.FolderPath);

            tbSaveLoc.Text = saveLoc.FolderPath;
            tbSaveLoc.Select(tbSaveLoc.Text.Length, 0);

            if (saveLoc.ValidLoc)
            {
                this.populateSaveList();
                tbBULoc.Text = buLoc.FolderPath;
            }
            else
            {
                MessageBox.Show("No suitable directory could be automatically detected. Please navigate to the directory of your save files.", "Directory Detection Error");
                while (!saveLoc.ValidLoc)
                {
                    if (saveFolBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string path = saveFolBrowser.SelectedPath;
                        if (saveLoc.validatePath(path))
                        {
                            this.updateSaveLoc(saveFolBrowser.SelectedPath);
                        }
                    }
                    else
                    {
                        // give up
                        saveLoc.ValidLoc = true;
                        Application.Exit();
                    }
                }
            }

            this.dispBUInfo();
        }

        // change save file location
        private void bSaveLoc_Click(object sender, EventArgs e)
        {
            if (saveFolBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = saveFolBrowser.SelectedPath;
                if (saveLoc.validatePath(path))
                {
                    this.updateSaveLoc(saveFolBrowser.SelectedPath);
                }
            }

        }

        // change backup file location
        private void bBULoc_Click(object sender, EventArgs e)
        {
            if (buFolBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.updateBULoc(buFolBrowser.SelectedPath);
            }
        }

        // update the save location
        // this will perform the following steps: update the saveLoc instance, update the text box, update the listbox
        // takes in a string of the new save location
        private void updateSaveLoc(string s)
        {
            saveLoc.FolderPath = s;
            tbSaveLoc.Text = s;
            saveList.Items.Clear();
            saveLoc.populateFiles();
            this.populateSaveList();

            if (MessageBox.Show("Would you like to automatically update the backup directory as well?", "Automatically Update Backup", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.updateBULoc(s + @"\Backups");
            }
        }

        // update the backup location
        // takes in a string of the new backup location
        private void updateBULoc(string s)
        {
            string newPath = s;
            buLoc.FolderPath = newPath;
            buLoc.populateBUs();
            tbBULoc.Text = newPath;
            this.dispBUInfo();
        }

        // populate the save list with data
        // takes the data from the saveLoc instance's list property
        private void populateSaveList()
        {
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                saveList.Items.Add(s.FileName);
            }
        }

        // empty the backup directory
        private void bEmptyBU_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete ALL files with an .sl2 extension in this directory. Ensure this is the backup directory and not the save directory. Would you like to continue?"
                , "Delete Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                buLoc.emptyLoc();

                if (buLoc.NumBackedUp == 0)
                {
                    lbBUNum.Text = "No backup files exist in this location yet.";
                }
                else
                {
                    lbBUNum.Text = "There are a total of " + buLoc.NumBackedUp + " backup files in this directory.";
                }

                foreach (SaveFile s in saveLoc.SaveFiles)
                {
                    s.FirstBU = Int32.MaxValue;
                    s.BackupCount = 0;
                }
            }
        }

        // activated
        private async void bActivate_Click(object sender, EventArgs e)
        {
            if (udFreq.Value == 0)
            {
                MessageBox.Show("Invalid value set for backup frequency. Try another one.", "Invalid Frequency");
            }
            if (tbBULoc.Text.Equals(tbSaveLoc.Text))
            {
                MessageBox.Show("Please do not backup to the save directory. Pick another location.", "Backup Directory is Save Directory");
            }
            else if (saveLoc.ValidLoc)
            {
                bSaveLoc.Enabled = false;
                bBULoc.Enabled = false;
                bEmptyBU.Enabled = false;
                bActivate.Enabled = false;
                udFreq.Enabled = false;
                bDeactivate.Enabled = true;
                tbBULoc.Enabled = false;
                tbSaveLoc.Enabled = false;
                saveList.Enabled = false;
                cbDel.Enabled = false;
                udDel.Enabled = false;
                progressBar1.MarqueeAnimationSpeed = 50;
                progressBar1.Maximum = 100;
                lbStatus.Text = String.Format("Currently backing up every {0} minutes. Click Deactivate to stop.", udFreq.Value);
                backingUp = true;
            }

            // starting backing up
            while (backingUp)
            {
                this.backup();
                await Task.Delay((int)udFreq.Value * 60000);
            }
        }

        // deactivated
        private void bDeactivate_Click(object sender, EventArgs e)
        {
            backingUp = false;
            bSaveLoc.Enabled = true;
            bBULoc.Enabled = true;
            bEmptyBU.Enabled = true;
            bActivate.Enabled = true;
            udFreq.Enabled = true;
            bDeactivate.Enabled = false;
            tbBULoc.Enabled = true;
            tbSaveLoc.Enabled = true;
            saveList.Enabled = true;
            cbDel.Enabled = true;
            if (!cbDel.Checked)
            {
                udDel.Enabled = true;
            }
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Maximum = 0;
            lbStatus.Text = "Currently not active.";
            backingUp = true;

            buLoc.populateBUs();
            this.dispBUInfo();
        }

        // displays backup information
        private void dispBUInfo()
        {
            if (buLoc.NumBackedUp == 0)
            {
                lbBUNum.Text = "No backup files exist in this location yet.";
            }
            else
            {
                lbBUNum.Text = String.Format("There are a total of {0} backup files in this directory for {1} MB of space.", buLoc.NumBackedUp, buLoc.FileSize);
            }
        }

        // opens the backup directory
        private void bOpenBU_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(buLoc.FolderPath);
        }

        // automatically replaces save file with latest backup file 
        private void bReplace_Click(object sender, EventArgs e)
        {
            if (!restoreWarned)
            {
                if (MessageBox.Show("This will replace your save files with the latest detected backups. Please use caution with this feature. I am not responsible for lost or corrupt save files. Make sure you are not loaded into the game (you can be on the menu).\n\nClick OK if you accept the risks. This message will not show again if you click OK. \n\nYou will be given a more detailed confirmation later before restoring.", "Restore Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    restoreWarned = true;
                }
            }
            buLoc.findBUs(saveLoc);
            // get latest save file
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                if (s.LastBU != Int32.MinValue)
                {
                    string buPath = buLoc.FolderPath + @"\" + s.FileName + "(" + s.LastBU + ")";
                    string buLastWritten = System.IO.File.GetLastWriteTime(buPath).ToString();
                    string newPath = saveLoc.FolderPath + @"\" + s.FileName;
                    string saveLastWritten = System.IO.File.GetLastWriteTime(newPath).ToString();
                    if (MessageBox.Show("Backup file: " + s.FileName + "(" + s.LastBU + ")\nLast written: " + buLastWritten + "\n\nWill replace: " + s.FileName + "\nLast written: " + saveLastWritten + "\n\nOk?", "Backup File", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.IO.File.Copy(buPath, newPath, true);
                    }
                }
                else
                {
                    MessageBox.Show("No backup files were found for file " + s.FileName + ". Start the backup process first.", "No Backups Found");
                    return;
                }
            }
        }

        // checkbox to delete is changed
        private void cbDel_Checked(object sender, EventArgs e)
        {
            if (cbDel.Checked)
            {
                lbDel1.Enabled = false;
                lbDel2.Enabled = false;
                udDel.Enabled = false;
            }
            else
            {
                lbDel1.Enabled = true;
                lbDel2.Enabled = true;
                udDel.Enabled = true;
            }
        }

        // handles the backup procedure
        private void backup()
        {
            // copy the save file to the backup directory
            buLoc.findBUs(saveLoc);
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                if (s.BackupCount == 0)
                {
                    string newFileName = s.FileName + "(0)";
                    newFileName = System.IO.Path.Combine(buLoc.FolderPath, newFileName);
                    string oldFileName = System.IO.Path.Combine(saveLoc.FolderPath, s.FileName);
                    System.IO.File.Copy(oldFileName, newFileName, true);
                    s.LastBU = 0;
                    s.FirstBU = 0;
                    s.BackupCount++;
                }
                else if (s.FirstBU == 0 && (s.LastBU - s.BackupCount + 1 == 0))
                {
                    string newFileName = s.FileName + String.Format("({0})", s.BackupCount);
                    newFileName = System.IO.Path.Combine(buLoc.FolderPath, newFileName);
                    string oldFileName = System.IO.Path.Combine(saveLoc.FolderPath, s.FileName);
                    System.IO.File.Copy(oldFileName, newFileName, true);
                    s.LastBU = s.BackupCount;
                    s.BackupCount++;
                }
                else
                {
                    string newFileName = s.FileName + String.Format("({0})", s.LastBU + 1);
                    newFileName = System.IO.Path.Combine(buLoc.FolderPath, newFileName);
                    string oldFileName = System.IO.Path.Combine(saveLoc.FolderPath, s.FileName);
                    System.IO.File.Copy(oldFileName, newFileName, true);
                    s.LastBU = s.BackupCount;
                    s.BackupCount++;
                }
            }

            // check if automatic deletion is desired
            if (!cbDel.Checked)
            {
                int delFreq = (int)udDel.Value;

                // check each save file to see how many times they have been backed up
                foreach (SaveFile s in saveLoc.SaveFiles)
                {
                    while (s.BackupCount > delFreq)
                    {
                        int firstBU = s.FirstBU;
                        System.IO.File.Delete(buLoc.FolderPath + @"\" + s.FileName + "(" + firstBU + ")");
                        s.BackupCount--;
                        firstBU++;
                        s.FirstBU++;

                        // verify that the new first backup actually exists...maybe it was deleted out of turn
                        while (!System.IO.File.Exists(buLoc.FolderPath + @"\" + s.FileName + "(" + firstBU + ")"))
                        {
                            firstBU++;
                        }
                    }
                }
            }

            buLoc.populateBUs();
            this.dispBUInfo();
        }

        // instant backup button
        private void bBUNow_Click(object sender, EventArgs e)
        {
            this.backup();
        }
    }
}
