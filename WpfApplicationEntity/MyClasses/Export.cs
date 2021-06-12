using System;
using System.Text;
using System.Windows;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WpfApplicationEntity.MyClasses
{
    static class ExportImport
    {
        private static readonly string ConnectionString =  ConfigurationManager.ConnectionStrings["DbConnectString"].ConnectionString;
        private static StreamWriter File;
        public static void ExportDataBase()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    File = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\DataBase.csv", false, Encoding.Unicode);
                    SelectTable("SELECT * FROM Employees", connection);
                    File.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                    connection.Dispose();
                    MessageBox.Show("База данных успешно сохранена", "Congratulations!");
                }
            }
        }
        private static void SelectTable(string query, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader SQLreader = command.ExecuteReader();
            if (SQLreader.HasRows)
            {
                File.WriteLine("#");
                while (SQLreader.Read())
                {
                    for (int i = 1; i < SQLreader.FieldCount; i++)
                        File.Write(SQLreader.GetValue(i).ToString() + ";");
                    File.WriteLine("$");
                }
            }
            connection.Close();
        }
        private static void InsertTable(string query, string[] values, int count, SqlConnection connection)
        {
            query += $" values (";
            for (int i = 0; i < count; i++)
            {
                query += $"'{values[i]}', ";
            }
            query = query.Substring(0, query.Length - 2);
            query += ")";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public static void ImportDataBase() 
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (StreamReader streamReader = new StreamReader("DataBase.csv"))
                {
                    string[] tables = streamReader.ReadToEnd().Replace(Environment.NewLine, "").Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tables.Length == 9)
                    {
                        for (int i = 0; i < tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries).Length; i++)
                        {
                            string[] values = tables[0].Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                            InsertTable("insert into Employees(Surname, Name, MiddelName, adress, Birthday, Position, Login, Password, Pass, NumberTelephone)", values[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries), 11, connection);
                        }
                    }
                    else MessageBox.Show("Количество таблиц не соответсвует таблицам базы данных", "Ошибка");
                    streamReader.Close();
                }
                connection.Close();
            }
        }
    }
}
