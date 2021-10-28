using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace EventsSchedule
{

    public partial class MainForm : Form
    {
        public Database database;
        public static string Language;

        private readonly RegistryKey _run = null;
        private string[] _args;

        public static bool allowExit = false;

        private WMPLib.WindowsMediaPlayer _player = new WMPLib.WindowsMediaPlayer();

        public MainForm(string[] args)
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            database = new Database();
            Language = Database.GetSetting("language");
            _args = args;
            if (IsAdministrator())
            {
                _run = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Run", true);
            }
            else
            {
                _run = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Run");
            }
        }


        private void addEventButton_Click(object sender, EventArgs e)
        {
            new AddEvent(this).ShowDialog();
        }

        private void editEventButton_Click(object sender, EventArgs e)
        {
            if(eventsList.SelectedItems.Count > 0)
            {
                var item = eventsList.SelectedItems[0];
                new EditEvent(this, item).ShowDialog();
            }
        }

        private void removeEventButton_Click(object sender, EventArgs e)
        {
            if (eventsList.SelectedItems.Count > 0)
            {
                Database.DeleteRecord(eventsList.SelectedItems[0].Index+1);
                eventsList.Items.Remove(eventsList.SelectedItems[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            autoRunButton.Checked = _run.GetValue("EventsSchedule") != null;
            if(Language == "1")
            {
                addEventButton.Text = "Добавить";
                editEventButton.Text = "Изменить";
                removeEventButton.Text = "Удалить";
                ColumnName.Text = "Название";
                ColumnDate.Text = "Дата";
                columnFinished.Text = "Завершен?";
                columnStartsAt.Text = "Начнётся через";
                autoRunButton.Text = "Запускать вместе с Windows";
                settingsButton.Text = "Настройки";
            }
            foreach (var record in database.Records)
            {
                AddItem(record);
            }
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
                string[] split = toastArgs.Argument.Split('=');
                if(split[0] == "delete")
                {
                    Database.DeleteRecord(int.Parse(split[1]));
                    var record = database.Records.Where(r => r.Id == int.Parse(split[1])).FirstOrDefault();
                    database.Records.Remove(record);
                    Action a = () => eventsList.Items.Remove(record.ViewItem);
                    eventsList.Invoke(a);
                }
            };
        }

        public void AddItem(Record record)
        {
            ListViewItem item = new ListViewItem(new string[] { record.Id.ToString(), record.Name, DateTimeOffset.FromUnixTimeSeconds(record.Date).ToLocalTime().ToString("yyyy, dd MMMM HH:mm"), Language == "1" ? "Завершен" : "Finished", record.Finished ? (Language == "1" ? "Да" : "Yes ") : (Language == "1" ? "Нет" : "No") })
            {
                Tag = record
            };
            record.ViewItem = item;
            eventsList.Items.Add(item);
        }

        public void EditItem(int id, Record record)
        {
            ListViewItem item = new ListViewItem(new string[] { record.Id.ToString(), record.Name, DateTimeOffset.FromUnixTimeSeconds(record.Date).ToLocalTime().ToString("yyyy, dd MMMM HH:mm"), "Unknown", "No" })
            {
                Tag = record
            };
            record.ViewItem = item;
            eventsList.Items[id] = item;
        }

        private void eventsList_DoubleClick(object sender, EventArgs e)
        {
            if (eventsList.SelectedItems.Count > 0)
            {
                var item = eventsList.SelectedItems[0];
                new EditEvent(this, item).ShowDialog();
            }
        }

        private void eventsList_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (eventsList.SelectedItems.Count > 0)
                {

                    if(!e.Shift)
                        if (MessageBox.Show(Language == "1" ? "Вы действительно хотите удалить это событие?\n\nИспользуйте Shift + Delete чтобы удалять без подтверждения": "Are you sure want to delete this event?\n\nUse Shift + Delete to delete events without confirmation", Language == "1" ? "Подтверждение" : "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return;
                    Database.DeleteRecord(eventsList.SelectedItems[0].Index + 1);
                    eventsList.Items.Remove(eventsList.SelectedItems[0]);
                }
            }
        }

        private void eventChecker_Tick(object sender, EventArgs e)
        {
            foreach (var record in database.Records.Where(r => !r.Finished))
            {
                if(record.Date < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                {
                    record.Finished = true;
                    Database.EditRecord(record);
                    if (Language == "1")
                    {
                        record.ViewItem.SubItems[3].Text = "Завершен";
                        record.ViewItem.SubItems[4].Text = "Да";
                        new ToastContentBuilder()
                        .AddText("Событие началось")
                        .AddText($"Событие с названием \"{record.Name}\" началось!")
                        .AddButton(new ToastButton("Удалить", "delete=" + record.Id.ToString()))
                        .Show();
                        continue;
                    }
                    record.ViewItem.SubItems[3].Text = "Finished";
                    record.ViewItem.SubItems[4].Text = "Yes";
                    new ToastContentBuilder()
                    .AddText("Event started")
                    .AddText($"Event with name \"{record.Name}\" started!")
                    .AddButton(new ToastButton("Delete", "delete=" + record.Id.ToString()))
                    .Show();
                }
                record.ViewItem.SubItems[3].Text = (DateTimeOffset.FromUnixTimeSeconds(record.Date) - DateTimeOffset.UtcNow).ToString(@"d\.hh\:mm\:ss");
            }
        }

        private void autoRunButton_Click(object sender, EventArgs e)
        {
            if(!IsAdministrator())
            {
                ProcessStartInfo info = new ProcessStartInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                info.UseShellExecute = true;
                info.Verb = "runas";
                Process.Start(info);
                allowExit = true;
                Close();
                return;
            }
            autoRunButton.Checked = !autoRunButton.Checked;
            if (autoRunButton.Checked)
            {
                _run.SetValue("EventsSchedule", new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath + " -silent");
            }
            else
            {
                _run.DeleteValue("EventsSchedule");
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowExit)
            {
                e.Cancel = true;
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            allowExit = true;
            Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_args.Length > 0 && _args[0] == "-silent")
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                notifyIcon.Visible = true;
            }
        }

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void settingsButtons_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }
    }
}