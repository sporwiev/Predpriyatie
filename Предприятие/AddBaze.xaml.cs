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
using System.Windows.Shapes;

namespace Предприятие
{
    /// <summary>
    /// Логика взаимодействия для AddBaze.xaml
    /// </summary>
    public partial class AddBaze : Window
    {
        public AddBaze()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "Добавить")
            {
                Request request = new Request();
                var text = request.ДобавитьБазу(textbox.Text);
                if (text.Length > 100)
                {
                    MessageBox.Show(text);
                }
                else
                {
                    res.Text = text;
                }

            }
            MainWindow main = new MainWindow();
            main.Show();
            Close();

        }

    }
}
