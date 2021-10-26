using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EventsSchedule
{
    public partial class MainForm : Form
    {
        public Database database;

        private readonly RegistryKey _run = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Run", true);

        public MainForm()
        {
            InitializeComponent();
            database = new Database();
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
            ListViewItem item = new ListViewItem(new string[] { record.Id.ToString(), record.Name, DateTimeOffset.FromUnixTimeSeconds(record.Date).ToLocalTime().ToString("yyyy, dd MMMM HH:mm"), "Finished", record.Finished ? "Yes ": "No" })
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
                        if (MessageBox.Show("Are you sure want to delete this event?\n\nUse Shift + Delete to delete events without confirmation", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                    record.ViewItem.SubItems[3].Text = "Finished";
                    record.ViewItem.SubItems[4].Text = "Yes";
                    new ToastContentBuilder()
                    .AddText("Event started")
                    .AddText($"Event with name \"{record.Name}\" started!")
                    //.AddButton(new ToastButton("Postpone for tomorrow", "tommorow=" + record.Id.ToString() + '='))
                    .AddButton(new ToastButton("Delete", "delete=" + record.Id.ToString()))
                    .Show();
                }
                record.ViewItem.SubItems[3].Text = (DateTimeOffset.FromUnixTimeSeconds(record.Date) - DateTimeOffset.UtcNow).ToString(@"d\.hh\:mm\:ss");
            }
        }

        private void autoRunButton_Click(object sender, EventArgs e)
        {
            autoRunButton.Checked = !autoRunButton.Checked;
            if (autoRunButton.Checked)
            {
                _run.SetValue("EventsSchedule", new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            }
            else
            {
                _run.DeleteValue("EventsSchedule");
            }
        }
    }
}