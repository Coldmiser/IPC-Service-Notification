using System;
using System.Windows.Forms;

namespace TaskTrayApplication
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            showMessageCheckBox.Checked = TaskTray.Properties.Settings.Default.ShowMessage;
        }

        private void SaveSettings(object sender, FormClosingEventArgs e)
        {
            // If the user clicked "Save"
            if (this.DialogResult == DialogResult.OK)
            {
                TaskTray.Properties.Settings.Default.ShowMessage = showMessageCheckBox.Checked;
                TaskTray.Properties.Settings.Default.Save();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }
    }
}