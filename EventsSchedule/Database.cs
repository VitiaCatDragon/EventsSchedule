using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace EventsSchedule
{
    public class Database
    {

        private static SQLiteConnection _connection = new SQLiteConnection(@"Data Source=events.db; Version=3;");

        public List<Record> Records = new List<Record>();
        public Dictionary<string, string> Settings = new Dictionary<string, string>();

        public Database()
        {
            CheckDatabase();
        }

        private void CheckDatabase()
        {
            if (!File.Exists(@"events.db"))
            {
                SQLiteConnection.CreateFile(@"events.db");
                CreateDatabase();
            }
            else
            {
                Records = GetRecords();
            }
        }

        public static long AddRecord(Record record)
        {
            string commandText = "INSERT INTO `events`(name, date) VALUES (@name, @date)";
            var command = new SQLiteCommand(commandText, _connection);
            command.Parameters.AddWithValue("@name", record.Name);
            command.Parameters.AddWithValue("@date", record.Date);
            _connection.Open();
            command.ExecuteNonQuery();
            var id = _connection.LastInsertRowId;
            _connection.Close();
            return id;
        }

        public static void AddSettings(Record record)
        {
            string commandText = "INSERT INTO `settings`(key, value) VALUES (\"notification_audio\", NULL, \"version\", \"2\")";
            var command = new SQLiteCommand(commandText, _connection);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public static void EditRecord(Record record)
        {
            string commandText = "UPDATE `events` SET `name` = @name, `date` = @date, `finished` = @finished WHERE `id` = @id";
            var command = new SQLiteCommand(commandText, _connection);
            command.Parameters.AddWithValue("@name", record.Name);
            command.Parameters.AddWithValue("@date", record.Date);
            command.Parameters.AddWithValue("@finished", record.Finished);
            command.Parameters.AddWithValue("@id", record.Id);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public static void DeleteRecord(long id)
        {
            string commandText = "DELETE FROM `events` WHERE `id` = @id";
            var command = new SQLiteCommand(commandText, _connection);
            command.Parameters.AddWithValue("@id", id);
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public static List<Record> GetRecords()
        {
            var records = new List<Record>();
            string commandText = "SELECT * FROM `events`";
            var command = new SQLiteCommand(commandText, _connection);
            _connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                records.Add(new Record(reader.GetInt32(0), reader.GetString(1), reader.GetInt64(2), reader.GetBoolean(3)));
            }
            reader.Close();
            _connection.Close();
            return records;
        }

        public static Dictionary<string, string> GetSettings()
        {
            var settings = new Dictionary<string, string>();
            string commandText = "SELECT * FROM `settings`";
            var command = new SQLiteCommand(commandText, _connection);
            _connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                settings.Add(reader.GetString(0), reader.GetString(1));
            }
            reader.Close();
            _connection.Close();
            return settings;
        }

        private void CreateDatabase()
        {
            string commandText = "CREATE TABLE \"events\"(\"id\"   INTEGER, \"name\"  TEXT NOT NULL, \"date\"  INTEGER NOT NULL, \"finished\"  INTEGER NOT NULL DEFAULT 0, PRIMARY KEY(\"id\" AUTOINCREMENT));";
            var command = new SQLiteCommand(commandText, _connection);
            _connection.Open();
            command.ExecuteNonQuery();
            commandText = "CREATE TABLE \"settings\" (\"key\"   TEXT NOT NULL, \"value\" TEXT, PRIMARY KEY(\"key\"));";
            command.CommandText = commandText;
            command.ExecuteNonQuery();
            _connection.Close();
        }

    }

    public class Record
    {
        public long Id;
        public string Name;
        public long Date;
        public bool Finished;

        public ListViewItem ViewItem;

        public Record(int id, string name, long date, bool finished)
        {
            Id = id;
            Name = name;
            Date = date;
            Finished = finished;
        }
    }
}
