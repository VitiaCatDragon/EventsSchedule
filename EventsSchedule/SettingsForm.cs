using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventsSchedule
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            languageSelect.SelectedIndex = int.Parse(MainForm.Language);
            if(MainForm.Language == "1")
            {
                Text = "Настройки";
                label1.Text = "Язык:";
                applyButton.Text = "Применить";
                cancelButton.Text = "Отмена";
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            Database.EditSetting("language", languageSelect.SelectedIndex.ToString());
            if (languageSelect.SelectedIndex.ToString() != MainForm.Language)
            {
                if(MessageBox.Show("Restart programm to change language", "Language change", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                    MainForm.allowExit = true;
                    Application.Exit();
                }
            }
        }
    }
}
