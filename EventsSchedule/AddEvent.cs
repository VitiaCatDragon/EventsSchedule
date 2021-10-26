using System;
using System.Windows.Forms;

namespace EventsSchedule
{
    public partial class AddEvent : Form
    {
        private MainForm _form;
        public AddEvent(MainForm form)
        {
            InitializeComponent();
            _form = form;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var record = new Record(-1, eventName.Text, ((DateTimeOffset)eventDate.Value).ToUnixTimeSeconds(), false);
            record.Id = Database.AddRecord(record);
            _form.database.Records.Add(record);
            _form.AddItem(record);
        }
    }
}