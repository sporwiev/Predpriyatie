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
using System.Xml.Linq;

namespace Предприятие
{
    /// <summary>
    /// Логика взаимодействия для addTable.xaml
    /// </summary>
    public partial class addTable : Window
    {

        public addTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string paramenres = "";
            for(int i = 0;i<column.Items.Count;i++)
            {

                Button button = column.Items[i] as Button;

                StackPanel stackPanel = button.Content as StackPanel;
                TextBlock text = stackPanel.Children[1] as TextBlock;
                paramenres += text.Text + " VARCHAR(50),";
            
            }

            List<string> list = new List<string> { paramenres.Substring(0,paramenres.Length - 1) };
            Request request = new Request();
            ComboBoxItem box = name_table.SelectedItem as ComboBoxItem;
            //MessageBox.Show($"USE {MainWindow.box}; CREATE TABLE {box.Content} ({list[0]});");
            MessageBox.Show(request.ДобавитьТаблицу(MainWindow.box, box.Content.ToString(),list));
            Приложение приложение = new Приложение();
            приложение.Show();
            Hide();
        }

        private void deleteitems(object sender, RoutedEventArgs e)
        {
            column.Items.Remove(sender);
        }
        List<string> zarplata = new List<string>() {"Дата","Номер","Месяц","Место_выплаты","Способ_расчетов","Сумма","Сотрудники","Организация","директБанк","Вид","Комментарий"};
        List<string> sklad = new List<string>() { "Подписан", "Комментарий", "Состояние_ЭДО", "Дата", "Номер", "Сумма", "Контрагент", "Счет_фактура", "Н_СФ_УПД", "Склад", "Вид_операции" };
        List<string> prod = new List<string>() { "Дата", "Номер", "Вид_операции", "Склад", "Цуль_расхода", "Комментарий"};

        public ComboBox comboBox = new ComboBox();

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            column.Items.Clear();
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem combos = combo.SelectedItem as ComboBoxItem;
            table_name.Text = combos.Content.ToString();

            if (combos.Content.ToString() == "продажи")
            {
                foreach (var item in zarplata)
                {
                    comboBox.Items.Add(item);

                }
            }
            if (combos.Content.ToString() == "склад")
            {
                foreach (var item in sklad)
                {
                    comboBox.Items.Add(item);
                }
            }
            if (combos.Content.ToString() == "зарплата")
            {

                foreach (var item in prod)
                {
                    comboBox.Items.Add(item);
                }
            }
        }
        private void name_column_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button button = new Button();
            StackPanel stack = new StackPanel();
            Image image = new Image();
            TextBlock textBlock = new TextBlock();
            ComboBox comboBoxed = comboBox;
            button.BorderBrush = Brushes.Transparent;
            button.Click += deleteitems;
            button.BorderThickness = new Thickness(0);
            button.Margin = new Thickness(0, 0, 10, 0);
            textBlock.Text = comboBoxed.SelectedItem.ToString();
            stack.Orientation = Orientation.Horizontal;
            stack.VerticalAlignment = VerticalAlignment.Center;
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\reject.png"));
            image.Width = 15;
            image.Height = 15;
            image.Margin = new Thickness(0, 0, 15, 0);
            stack.Children.Add(image);
            stack.Children.Add(textBlock);
            button.Content = stack;
            column.Items.Add(button);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox combo = new ComboBox();
            combo.Width = 100;
            combo.Margin = new Thickness(0,0,10,0);
            combo.SelectionChanged += name_column_SelectionChanged;
            combo.Name = "name_column";
            comboBox = combo;
            Button button = new Button();
            button.Content = "Добавить";
            button.Click += Button_Click;
            button.BorderThickness = new Thickness(0);
            button.Width = 100;
            stact.Children.Add(combo);
            stact.Children.Add(button);
        }
    }
}
