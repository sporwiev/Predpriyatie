using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Предприятие
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    
    public partial class Login : Window
    {
        
        public Login()
        {
            InitializeComponent();
        }
        public static string login_use = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            
            if(button.Content.ToString() == "Войти")
            {
                Request request = new Request();
                request.ВыбратьБазу("users");
                List<string> resp = request.Вход(login_user.Text, password_user.Text);

          
                if(resp[1] != null)
                {

                    MessageBox.Show(button.Content.ToString());
                    MainWindow mainWindow = new MainWindow();

                    login_use = login_user.Text;
                    res.Text = "Ожидание...";
                    Thread.Sleep(200);
                    mainWindow.Show();
                    Hide();
                }
                else
                {
                    res.Text = "Неправильный пароль или логин";
                }

            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                Hide();
            }
        }
    }
}
