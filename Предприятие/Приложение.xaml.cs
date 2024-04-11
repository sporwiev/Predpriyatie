using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using static Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Предприятие
{
    /// <summary>
    /// Логика взаимодействия для Приложение.xaml
    /// </summary>
    public partial class Приложение : Window
    {
        public Приложение()
        {
            InitializeComponent();
        }
        public void getData(string table,string name_db)
        {
            Request request = new Request();
            dataGridCustomers.ItemsSource = request.ОбновитьТаблицу(table,name_db);
            dataGridCustomers.Columns.RemoveAt(0);
            

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            user.Text = Login.login_use;
            
            Request request = new Request();
            string name = request.СуществованиеТаблицы(MainWindow.name_base, "продажи");
            if (name == null)
            {
                addTable table = new addTable();
                table.Show();
                Hide();
            }
            else
            {
                getData(name,MainWindow.name_base);
                
            }
        }
        public void btndeleted()
        {
            Button button = new Button();
            button.Margin = new Thickness(0,0,0,2);
            button.BorderThickness = new Thickness(0);
            button.Click += Button_Click_2;
            button.Background = Brushes.Transparent;
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\reject.png"));
            image.Width = 15;
            image.Height = 15;
            button.Content = image;
            regect.Children.Add(button);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Request request = new Request();
            Button button = (Button)sender;
            switch (button.Content)
            {
                case "продажи":
                    string Продажи = request.СуществованиеТаблицы(MainWindow.name_base, "продажи");
                    if (Продажи == null)
                    {

                        addTable table = new addTable();
                        table.Show();
                        Hide();
                    }
                    else
                    {
                        for (int i = 1; i < request.КолличетсвоСтрочек(MainWindow.name_base, "продажи"); i++)
                        {
                            btndeleted();
                        }
                        addpole("продажи");
                        nameTable.Text = "продажи";
                        getData(Продажи, MainWindow.name_base);
                    }
                    break;
                case "склад":
                    string Склад = request.СуществованиеТаблицы(MainWindow.name_base, "склад");
                    if (Склад == null)
                    {

                        addTable table = new addTable();
                        table.Show();
                        Hide();
                    }
                    else
                    {
                        for (int i = 1; i < request.КолличетсвоСтрочек(MainWindow.name_base, "склад"); i++)
                        {
                            btndeleted();
                        }
                        addpole("склад");
                        nameTable.Text = "склад";
                        getData(Склад, MainWindow.name_base);
                    }

                    break;
                case "зарплата":
                    string Зарплата = request.СуществованиеТаблицы(MainWindow.name_base, "зарплата");
                    if (Зарплата == null)
                    {
                        addTable table = new addTable();
                        table.Show();
                        Hide();
                    }
                    else
                    {
                        for (int i = 1; i < request.КолличетсвоСтрочек(MainWindow.name_base, "зарплата"); i++)
                        {
                            btndeleted();
                        }
                        addpole("зарплата");
                        nameTable.Text = "зарплата";
                        getData(Зарплата, MainWindow.name_base);
                    }
                    break;
                case "Создать":
                    listparametres.Items.Clear();
                    Button button1 = new Button();
                    button1.Content = "Добавить";
                    button1.Click += inserted;
                    List<string> list = request.КоличествоКолонн(MainWindow.name_base,nameTable.Text);
                    for(int i = 0;i < list.Count;i++)
                    {

                        StackPanel panel = new StackPanel();
                        TextBlock textBlock = new TextBlock();
                        TextBox textBox1 = new TextBox();
                        textBox1.BorderThickness = new Thickness(0,0,0,2);
                        textBox1.Width = 100;
                        textBox1.Background = Brushes.Transparent;
                        panel.Orientation = Orientation.Vertical;
                        panel.Margin = new Thickness(0,0,0,3);
                        panel.HorizontalAlignment = HorizontalAlignment.Center;
                        textBlock.Text = list[i];
                        textBlock.Margin = new Thickness(0,0,0,5);

                        if (list[i] != "ID")
                        {
                            panel.Children.Add(textBlock);
                            panel.Children.Add(textBox1);
                        }
                        listparametres.Items.Add(panel);
                        
                    }
                    listparametres.Items.Add(button1);

                    break;
            }
        }
        public void addpole(string table)
        {
            Button btnprod = new Button();
            Color color = new Color();
            color.R = 169;
            color.G = 169;
            color.B = 169;
            btnprod.Background = new SolidColorBrush(color);
            btnprod.Width = 100;
            btnprod.Foreground = Brushes.Black;
            btnprod.MouseDoubleClick += Btnprod_MouseDoubleClickActive;
            btnprod.Click += Btnprod_ClickDel;
            btnprod.VerticalContentAlignment = VerticalAlignment.Center;
            btnprod.HorizontalContentAlignment = HorizontalAlignment.Center;
            btnprod.BorderThickness = new Thickness(0);
            StackPanel stacksr = new StackPanel();
            StackPanel stat = new StackPanel();
            stacksr.Orientation = Orientation.Horizontal;
            TextBlock text = new TextBlock();
            text.Margin = new Thickness(0);
            text.Text = table;
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\reject.png"));
            img.Width = 15;
            img.Height = 15;
            stat.Orientation = Orientation.Horizontal;
            stat.Margin = new Thickness(1, 0, 0, 0);
            stacksr.Children.Add(text);
            stacksr.Children.Add(img);
            btnprod.Content = stacksr;
            stat.Children.Add(btnprod);
            nametableslist.Children.Add(stat);
        }

        private void Btnprod_ClickDel(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            nametableslist.Children.Remove(button);
            getData(nameTable.Text,MainWindow.name_base);
        }

        private void Btnprod_MouseDoubleClickActive(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            StackPanel stackPanel = new StackPanel();
            TextBlock text = stackPanel.Children[1] as TextBlock;
            getData(text.Text,MainWindow.name_base);
        }

        List<string> value = new List<string>();
        List<string> paramentres = new List<string>();
        private void inserted(object sender, RoutedEventArgs e)
        {
           
            foreach(object item in listparametres.Items)
            {
                if (item is StackPanel)
                {
                    StackPanel stack = item as StackPanel;

                    foreach (object item2 in stack.Children)
                    {
                        if (item2 is TextBox)
                        {
                            TextBox textBox = (TextBox)item2;
                            paramentres.Add(textBox.Text);
                        }
                        else
                        {
                            TextBlock textBlock = (TextBlock)item2;
                            value.Add(textBlock.Text);
                        }
                    }
                }
                else
                {

                }
            }
            string res_par = "";
            string res_val = "";
            for(int i = 0; i < paramentres.Count; i++)
            {
                res_par += "'" + paramentres[i] + "', ";
                res_val += "`" + value[i] + "`,";
            }
            Request request = new Request();
            //MessageBox.Show($"INSERT INTO `gggg` ({res_val.Substring(0, res_val.Length - 1)}) VALUES ({res_par.Substring(0, res_par.Length - 2)})");
            res.Text = request.ДобавитьСтрочку(MainWindow.name_base,nameTable.Text, res_val.Substring(0, res_val.Length - 1), res_par.Substring(0, res_par.Length - 2));
            getData(nameTable.Text,MainWindow.name_base);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данная функция доступна только в премиум версии");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int a = regect.Children.IndexOf(button);
            Request request = new Request();
            request.УдалитьСтрочку(MainWindow.name_base,nameTable.Text,a.ToString());
            getData(nameTable.Text, MainWindow.name_base);
            regect.Children.Remove(button);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Thread.Sleep(200);

        }
    }
}
