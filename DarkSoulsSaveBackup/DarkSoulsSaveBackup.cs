/* DS Save Backup
 * Main functionality of the program is contained in here.
 * 
 * May 2016
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
        /// <summary>
        /// The location that indicates the save location.
        /// </summary>
        private SaveLoc saveLoc;

        /// <summary>
        /// The location where backup files will be stored to.
        /// </summary>
        private BackUpLoc buLoc;

        /// <summary>
        /// State variable indicating whether we are currently backing up.
        /// </summary>
        private bool backingUp;

        /// <summary>
        /// Indicates whether the user has been warned of issues with restoration.
        /// </summary>
        private bool restoreWarned;

        public DarkSoulsSaveBackup()
        {
            InitializeComponent();
            restoreWarned = false;
        }

        /// <summary>
        /// Executes when the app is initially loaded.
        /// This attempts to populate the default save location, backup location, and save files.
        /// </summary>
        private void DS2SB_OnShow(object sender, EventArgs e)
        {
            // We start not backing up. Create a new save and backup location to the defaults.
            backingUp = false;
            saveLoc = new SaveLoc();
            buLoc = new BackUpLoc(saveLoc.FolderPath);

            // Update the save location textbox
            tbSaveLoc.Text = saveLoc.FolderPath;
            tbSaveLoc.Select(tbSaveLoc.Text.Length, 0);

            // If the save location is valid, populate the save listbox
            if (saveLoc.ValidLoc)
            {
                this.populateSaveList();
                tbBULoc.Text = buLoc.FolderPath;
            }
            // If the save location is not valid, ask the user to provide a valid save location.
            else
            {
                MessageBox.Show("No suitable directory could be automatically detected. " + 
                       "Please navigate to the directory of your save files.", "Directory Detection Error");

                // Keep trying to find a valid save location until we get one.
                while (true)
                {
                    // Prompt for a file location with a file dialog.
                    if (saveFolBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // Validate the save location. If valid, we can break out.
                        string path = saveFolBrowser.SelectedPath;
                        if (saveLoc.validatePath(path))
                        {
                            this.updateSaveLoc(saveFolBrowser.SelectedPath);
                            break;
                        }
                    }
                    else
                    {
                        // If they cancel out of the file dialog, simply exit the application.
                        saveLoc.ValidLoc = true;
                        Application.Exit();
                    }
                }
            }

            // Update the backup information display
            this.dispBUInfo();
        }

        /// <summary>
        /// Allow the user to change the save file location with a file browser.
        /// </summary>
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

        /// <summary>
        /// Allow the user to change the backup file location with a file browser.
        /// </summary>
        private void bBULoc_Click(object sender, EventArgs e)
        {
            if (buFolBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.updateBULoc(buFolBrowser.SelectedPath);
            }
        }

        /// <summary>
        /// Updates the save location. This performs the following steps:
        ///     - updates the saveLoc instance
        ///     - updates the textbox
        ///     - updates the listbox 
        ///     - checks if the user wants to update the backup location as well
        /// </summary>
        /// <param name="s">new save file location</param>
        private void updateSaveLoc(string s)
        {
            // Do all of the above
            saveLoc.FolderPath = s;
            tbSaveLoc.Text = s;
            saveList.Items.Clear();
            saveLoc.populateFiles();
            this.populateSaveList();

            // Ask the user if they want to update the backup location as well. 
            if (MessageBox.Show("Would you like to automatically update the backup directory as well?", 
                   "Automatically Update Backup", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.updateBULoc(s + @"\Backups");
            }
        }

        /// <summary>
        /// Updates the backup location. This performs the following steps:
        ///     - updates the buLoc instance
        ///     - populates and updates the backup information data
        /// </summary>
        /// <param name="s">new backup file location</param>
        private void updateBULoc(string s)
        {
            string newPath = s;
            buLoc.FolderPath = newPath;
            buLoc.populateBUs();
            tbBULoc.Text = newPath;
            this.dispBUInfo();
        }

        /// <summary>
        /// Populates the save listbox with data using the saveLoc's save file list.
        /// </summary>
        private void populateSaveList()
        {
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                saveList.Items.Add(s.FileName);
            }
        }

        /// <summary>
        /// Empty the backup directory of files.
        /// </summary>
        private void bEmptyBU_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will delete ALL files with an .sl2 extension in this directory. "+
                    "Ensure this is the backup directory and not the save directory. Would you like to continue?",
                    "Delete Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                // Empty the directory
                buLoc.emptyLoc();

                // Update the text for the backup information display. 
                if (buLoc.NumBackedUp == 0)
                {
                    lbBUNum.Text = "No backup files exist in this location yet.";
                }
                else
                {
                    lbBUNum.Text = "There are a total of " + buLoc.NumBackedUp + " backup files in this directory.";
                }

                // Reset the backup count for each save file in the save location.
                foreach (SaveFile s in saveLoc.SaveFiles)
                {
                    s.FirstBU = Int32.MaxValue;
                    s.BackupCount = 0;
                }
            }
        }

        /// <summary>
        /// Actions when the "Activate" button is pressed. Mainly, start backing up.
        /// </summary>
        private async void bActivate_Click(object sender, EventArgs e)
        {
            // Verify the update frequency was set to a valid number.
            if (udFreq.Value == 0)
            {
                MessageBox.Show("Invalid value set for backup frequency. Try another one.", "Invalid Frequency");
                return;
            }

            // Verify that the backup directory and the save directory are different.
            if (tbBULoc.Text.Equals(tbSaveLoc.Text))
            {
                MessageBox.Show("Please do not backup to the save directory. Pick another location.", "Backup Directory is Save Directory");
                return;
            }

            // Finally, make sure we are backing up files in a valid location. If so, update
            // the GUI to indicate that backing up is happening.
            if (saveLoc.ValidLoc)
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
                lbStatus.Text = String.Format("Currently backing up every {0} minutes. Click Deactivate to stop.",
                                                udFreq.Value);
                backingUp = true;
            }

            // Back up a file every so often, set by the update frequently, as long as we are backing up.
            while (backingUp)
            {
                this.backup();
                await Task.Delay((int)udFreq.Value * 60000);
            }
        }

        /// <summary>
        /// Actions when the "Deactivate" button is clicked. Mainly, stop backing up.
        /// </summary>
        private void bDeactivate_Click(object sender, EventArgs e)
        {
            // Update GUI to indicate that backing up is no longer happening.
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

            // Make sure out backup location accurately contains all of the backup files.
            buLoc.populateBUs();

            // Ensure the backup information is up to date.
            this.dispBUInfo();
        }

        /// <summary>
        /// Display backup information, namely about the number of backup files and their total size.
        /// </summary>
        private void dispBUInfo()
        {
            if (buLoc.NumBackedUp == 0)
            {
                lbBUNum.Text = "No backup files exist in this location yet.";
            }
            else
            {
                lbBUNum.Text = String.Format("There are a total of {0} backup files in this directory for {1} MB of space.",
                    buLoc.NumBackedUp, buLoc.FileSize);
            }
        }

        /// <summary>
        /// Open the backup location in a File Explorer window.
        /// </summary>
        private void bOpenBU_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(buLoc.FolderPath);
        }

        /// <summary>
        /// Handles the "Restore" functionality by replacing the save file with the latest save file.
        /// This will warn the user about the dangers of this process if this is their first time.
        /// </summary>
        private void bReplace_Click(object sender, EventArgs e)
        {
            // If necessary, warn the user.
            if (!restoreWarned)
            {
                if (MessageBox.Show("This will replace your save files with the latest detected backups. " + 
                        "Please use caution with this feature. I am not responsible for lost or corrupt save files. " +
                        "I am not responsible for any bans occurring due to save file replacement. " + 
                        "Make sure you are not loaded into the game (you can be on the menu).\n\n" + 
                        "Click OK if you accept the risks. This message will not show again if you click OK. " +
                        "\n\nYou will be given a more detailed confirmation later before restoring.", 
                        "Restore Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    // Exit out if the user does not which to restore.
                    return;
                }
                else
                {
                    // If they do wish to restore, indicate that they've been warned and continue the process.
                    restoreWarned = true;
                }
            }

            // Get the latest backed up save file.
            buLoc.findBUs(saveLoc);
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                // Check that a backup file exists. If so, continue.
                if (s.LastBU != Int32.MinValue)
                {
                    // Copy the file.
                    string buPath = buLoc.FolderPath + @"\" + s.FileName + "(" + s.LastBU + ")";
                    string buLastWritten = System.IO.File.GetLastWriteTime(buPath).ToString();
                    string newPath = saveLoc.FolderPath + @"\" + s.FileName;
                    string saveLastWritten = System.IO.File.GetLastWriteTime(newPath).ToString();
                    if (MessageBox.Show("Backup file: " + s.FileName + "(" + s.LastBU + ")\nLast written: " + buLastWritten + "\n\nWill replace: " + s.FileName + "\nLast written: " + saveLastWritten + "\n\nOk?", "Backup File", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.IO.File.Copy(buPath, newPath, true);
                    }
                }
                // No backup file exists...warn and return.
                else
                {
                    MessageBox.Show("No backup files were found for file " + s.FileName + 
                                    ". Start the backup process first.", "No Backups Found");
                    return;
                }
            }
        }

        /// <summary>
        /// There's a checkbox that determines whether or not save files should be deleted at a set interval.
        /// This function mainly updates the GUI based on whether it is checked or not.
        /// </summary>
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

        /// <summary>
        /// Handles the backup procedure. This is essentially copying the save file to the backup directory.
        /// Add a number to the end indicating which backup it is in sequence. This number serves as an identifier.
        /// Also update the first and last backup files when appropriate for tracking.
        /// </summary>
        private void backup()
        {
            // Copy the save file to the backup directory
            buLoc.findBUs(saveLoc);
            foreach (SaveFile s in saveLoc.SaveFiles)
            {
                if (s.BackupCount == 0)
                {
                    // If it's the first backup, indicate that this is both the first and last.
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

            // Check if automatic deletion is requested. If so, delete the latest.
            if (!cbDel.Checked)
            {
                int delFreq = (int)udDel.Value;

                // Check each save file to see how many times they have been backed up.
                foreach (SaveFile s in saveLoc.SaveFiles)
                {
                    while (s.BackupCount > delFreq)
                    {
                        int firstBU = s.FirstBU;
                        System.IO.File.Delete(buLoc.FolderPath + @"\" + s.FileName + "(" + firstBU + ")");
                        s.BackupCount--;
                        firstBU++;
                        s.FirstBU++;

                        // Verify that the new first backup actually exists...maybe it was deleted out of turn
                        while (!System.IO.File.Exists(buLoc.FolderPath + @"\" + s.FileName + "(" + firstBU + ")"))
                        {
                            firstBU++;
                        }
                    }
                }
            }

            // Update the backup information display.
            buLoc.populateBUs();
            this.dispBUInfo();
        }

        /// <summary>
        /// Handles when the user requests an instant backup.
        /// </summary>
        private void bBUNow_Click(object sender, EventArgs e)
        {
            this.backup();
        }
    }
}
