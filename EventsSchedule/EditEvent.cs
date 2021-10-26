using System;
using System.Windows.Forms;

namespace EventsSchedule
{
    public partial class EditEvent : Form
    {
        private MainForm _form;
        private ListViewItem _item;

        public EditEvent(MainForm form, ListViewItem item)
        {
            InitializeComponent();
            _form = form;
            _item = item;
            eventName.Text = item.SubItems[1].Text;
            eventDate.Value = DateTimeOffset.FromUnixTimeSeconds(((Record)item.Tag).Date).UtcDateTime.ToLocalTime();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var id = _item.Index;
            var record = new Record(id + 1, eventName.Text, ((DateTimeOffset)eventDate.Value).ToUnixTimeSeconds(), false);
            Database.EditRecord(record);
            _form.database.Records[id] = record;
            _form.EditItem(id, record);
        }
    }
}