using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace Предприятие
{
    
    internal class Request
    {
        
        public MySqlDataReader СписокПредприятий()
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("SHOW DATABASES", db.getConnection());
            db.OpenConnection();
            MySqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                
                return reader;
            }
            else
            {
                
                return null;
            }

            
        }
        public string УдалитьСтрочку(string dbname, string table, string arg)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"USE {dbname}; DELETE FROM {table} WHERE `id` = {arg}", db.getConnection());
            db.OpenConnection();
            command.ExecuteNonQuery();
            return null;
            

        }
        public string ДобавитьСтрочку(string dbname,string table,string parametr,string arg)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"USE {dbname};INSERT INTO `{table}`({parametr}) VALUES ({arg})", db.getConnection());
            db.OpenConnection();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                db.CloseConnection();
                return "у вас ошибка в данных";
            }
            else
            {
                db.CloseConnection();
                return null;
            }

            }
        public List<string> КоличествоКолонн(string db_name,string table)
        {
            DB db = new DB();
            List<string> list = new List<string>();
            MySqlCommand command = new MySqlCommand($"SELECT `COLUMN_NAME` FROM `INFORMATION_SCHEMA`.`COLUMNS` WHERE `TABLE_SCHEMA`='{db_name}' AND `TABLE_NAME`='{table}';", db.getConnection());
            db.OpenConnection();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            return list;
           

        }
        public string СоздатьТокен()
        {
            Random random = new Random();
            char[] letters = Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (char)c).ToArray();
            string a = " ";
            for(int i = 0; i < letters.Length; i++)
            {
                a += letters[i];
                if(i % 2 == 0)
                {
                    a += '$';
                }
                if(i % 3 == 0)
                {
                    a += '@';
                }
                if(i % 4 == 0)
                {
                    a += random.Next(0,10).ToString();
                }
            }
            return a;
        }
        public string УдалитьБазу(string name)
        {
            DB db = new DB();
            try
            {
                MySqlCommand command = new MySqlCommand($"DROP DATABASE {name};", db.getConnection());
                db.OpenConnection();
                Thread.Sleep(100);
                command.ExecuteNonQuery();
                db.CloseConnection();
                return $"База данных {name} успешно удалена";
            }
            catch (Exception ex)
            {
                db.CloseConnection();
                return $"Ошибка, {ex.Message}";
            }
        }
        public string ВыбратьБазу(string name)
        {
            DB db = new DB();
            try
            {
                MySqlCommand command = new MySqlCommand($"USE {name};", db.getConnection());
                db.OpenConnection();
                Thread.Sleep(100);
                command.ExecuteNonQuery();
                db.CloseConnection();
                return null;
            }
            catch (Exception ex)
            {
                db.CloseConnection();
                return $"Ошибка, {ex.Message}";
            }
        }
        public DataView ОбновитьТаблицу(string table,string db_name)
        {
            DB dB = new DB();
            // Таблица в которую помещаем данные
            DataTable dt = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();//Адаптер 
            MySqlCommand mySqlCommand = new MySqlCommand($"USE {db_name};SELECT * FROM {table}", dB.getConnection()); //Запрос + Открытие подключения
            mySqlDataAdapter.SelectCommand = mySqlCommand;
            // Присваивание Дефолтных значение вDataGridCustomers
            mySqlDataAdapter.Fill(dt);//Присваеваем данные в таблицу'
            dB.CloseConnection();
            return dt.DefaultView;

        }
        public int КолличетсвоСтрочек(string db_name, string table_name)
        {
            DB dB = new DB();
            MySqlCommand command = new MySqlCommand($"USE `{db_name}`; SELECT COUNT(*) FROM {table_name};",dB.getConnection());
            dB.OpenConnection();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                int a = 0;
                while (reader.Read()) 
                {
                    a = Convert.ToInt32(reader.GetString(0));
                }
                
                dB.CloseConnection();
                
                return a;
            }
            return 0;
        }
        public string СуществованиеТаблицы(string db_name,string table_name)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"USE `{db_name}`; SHOW TABLES LIKE '{table_name}'", db.getConnection());
            db.OpenConnection();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                db.CloseConnection();
                return table_name;
            }
            else
            {
                db.CloseConnection();
                return null;
            }
        }
        public string проверка_токена(string login)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand($"USE users; SELECT token FROM `user` WHERE `login` = '{login}'", db.getConnection());
            db.OpenConnection();
            Thread.Sleep(100);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                return reader.GetString(0);
            }
            else
            {
                return null;
            }
        }

        public List<string> Вход(string login,string password)
        {
            DB db = new DB();
            try
            {
                MySqlCommand command = new MySqlCommand($"USE users; SELECT token FROM `user` WHERE `login` = '{login}' AND `password` = '{password}'", db.getConnection());
                db.OpenConnection();
                Thread.Sleep(100);
                MySqlDataReader reader = command.ExecuteReader();
                
                
                

                if (reader.Read())
                {
                    List<string> list = new List<string> {login, reader.GetString(0) };
                    return list;
                }
                
                else
                {
                    db.CloseConnection();
                    return ОбновитьТокен("");
                }
                
            }
            catch
            {
                db.CloseConnection();
                return null;
            }
        }
        public List<string> ОбновитьТокен(string token)
        {
            DB dB = new DB();


            string newtoken = СоздатьТокен();
            MySqlCommand command = new MySqlCommand($"UPDATE `user` SET `token` = '{newtoken}' WHERE `token`='{token}';",dB.getConnection());
            dB.OpenConnection();
            command.ExecuteNonQuery();
            dB.CloseConnection();
            List<string> list = new List<string> {newtoken};
            return list;

        }
        public string ДобавитьТаблицу(string db_name,string name, List<string> arg)
        {
            DB db = new DB();
            try
            {
                MySqlCommand command = new MySqlCommand($"USE {db_name}; CREATE TABLE {name} (ID INT NOT NULL AUTO_INCREMENT,{arg[0]},PRIMARY KEY (ID));", db.getConnection());
                db.OpenConnection();
                Thread.Sleep(100);
                command.ExecuteNonQuery();
                db.CloseConnection();
                return $"Таблица {name} успешно создана";
            }
            catch (Exception ex)
            {
                db.CloseConnection();
                return $"Ошибка, {ex.Message}";
            }
        }
        public string ДобавитьБазу(string name) 
        {
            DB db = new DB();
            try
            {
                MySqlCommand command = new MySqlCommand($"CREATE DATABASE {name};", db.getConnection());
                db.OpenConnection();
                Thread.Sleep(100);
                command.ExecuteNonQuery();
                db.CloseConnection();
                return $"База {name} успешно создана";
            } catch (Exception ex) 
            {
                db.CloseConnection();
                return $"Ошибка {ex.Message}";
            }
        }
        



    }
}
