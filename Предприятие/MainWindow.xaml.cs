using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Предприятие
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string box;
        public MainWindow()
        {
            InitializeComponent();
        }
        public static string name_base;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            DB dB = new DB();
            Login login = new Login();
            
            if (request.проверка_токена(Login.login_use) == null || user_name.Text == null)
            {
                login.Show();
                Hide();
            }
            else
            {   
                login.Close();
                Button button = (Button)sender;
                switch (button.Content.ToString())
                {
                    case "Добавить":
                        AddBaze baze = new AddBaze();
                        baze.Show();
                        Hide();
                        break;
                    case "Удалить":
                        int item = listBox1.SelectedIndex;
                        if (item != -1)
                        {


                            var a = listBox1.Items.GetItemAt(item);


                            request.УдалитьБазу(a.ToString());

                            listBox1.Items.RemoveAt(item);
                        }
                        else
                        {
                            MessageBox.Show("Вы ничего не выбрали");
                        }
                        break;
                    case "2С Предприятие":
                        int index = listBox1.SelectedIndex;
                        if (index != -1)
                        {
                            box = listBox1.SelectedItem.ToString();
                            var a = listBox1.Items.GetItemAt(index);
                            name_base = a.ToString();
                            request.ВыбратьБазу(a.ToString());
                            Приложение приложение = new Приложение();
                            приложение.Show();

                        }
                        else
                        {
                            MessageBox.Show("Вы ничего не выбрали");
                        }
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB dB = new DB();
            Request request = new Request();
            MySqlDataReader t = request.СписокПредприятий();
            while (t.Read())
            {
                string name = t.GetString(0);
                if(name == "information_schema" || name == "mysql" || name == "performance_schema" || name == "sys" || name == "users")
                {

                }
                else
                {
                    listBox1.Items.Add(t.GetString(0));
                }
                
            }
            dB.CloseConnection();
        }
    }
}
