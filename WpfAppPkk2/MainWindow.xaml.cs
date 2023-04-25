using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using WpfAppPkk2Controls.Objects;

namespace WpfAppPkk2Controls
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<Customer> _customers = new List<Customer>();
        // データの変更を画面に通知する
        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();
        private ObservableCollection<Member> _members = new ObservableCollection<Member>();
        private ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();

        private int _index = 0;

        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
            //PrepareListView();
            PrepareCombo();
            PrepareListbox();
            PrepareTreeView();
        }

        private void PrepareListView()
        {
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });
            _customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });

            CustomerListView.ItemsSource = _customers;
        }

        private void PrepareCombo()
        {
            MyComboBox.Items.Add("111");
            MyComboBox.Items.Add("222");
            MyComboBox.Items.Add("333");

            _persons.Add(new Person() { Id = 1, Name = "aaa", Phone = "010" });
            _persons.Add(new Person() { Id = 2, Name = "bbb", Phone = "020" });
            _persons.Add(new Person() { Id = 3, Name = "ccc", Phone = "030" });

            MyComboBox2.ItemsSource = _persons;
            MyComboBox3.ItemsSource = _persons;
        }

        private void PrepareListbox()
        {
            string currentPath = Directory.GetCurrentDirectory();
            _members.Add(new Member(Path.Combine(currentPath, @"images\RidingHood.jpg"), "fse geoi"));
            _members.Add(new Member(Path.Combine(currentPath, @"images\Ninja.jpg"), "weij fije"));
            _members.Add(new Member(Path.Combine(currentPath, @"images\PoliceOfficer.jpg"), "ofke iekjsai"));

            MyListBox.ItemsSource = _members;
            MyListBox.SelectionMode = SelectionMode.Extended;
        }

        private void PrepareTreeView()
        {
            var section1 = new Employee("100", "Section1");
            section1.Employees.Add(new Employee("101", "社員A"));
            section1.Employees.Add(new Employee("102", "社員B"));
            var section2 = new Employee("100", "Section2");
            section2.Employees.Add(new Employee("101", "社員C"));
            section2.Employees.Add(new Employee("102", "社員S"));

            _employees.Add(section1);
            _employees.Add(section2);

            MyTreeViewControl2.ItemsSource = _employees;

        }

        private void ReadDatabase()
        {
            using(var conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Customer>();

                _customers.Clear();
                foreach (var customer in conn.Table<Customer>().ToList())
                {
                    _customers.Add(customer);
                }
                CustomerListView.ItemsSource = _customers;

                //CustomerListView.ItemsSource = conn.Table<Customer>().ToList();
            }
        }

        private void SaveSqliteButton_Click(object sender, RoutedEventArgs e)
        {
            var customer = new Customer()
            {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
            };
            
            using (var conn = new SQLiteConnection(App.databasePath))
            {
                // テーブルがなかったら作る
                conn.CreateTable<Customer>();
                conn.Insert(customer);
            }
        }

        private void ReadSqliteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Customer>();
                //var customers = conn.Table<Customer>().ToList();
                var customers = conn.Table<Customer>().Select(x => x.Name).ToList();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filterList = _customers.Where(x => x.Name.Contains(SearchTextBox.Text)).ToList();
            //var filterList = from x in _customers where x.Name.Contains(SearchTextBox.Text) select x;
            CustomerListView.ItemsSource = filterList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //_customers.Add(new Customer() { Id = ++_index, Name = "name" + _index, Phone = "090-123-00" + _index });

            var f = new SaveWindow(null);
            f.ShowDialog();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var item = CustomerListView.SelectedItem as Customer;
            if (item is null)
            {
                MessageBox.Show("行を選択してください");
                return;
            }

            var f = new SaveWindow(item);
            f.ShowDialog();
            int ret = f.ret;
            ReadDatabase();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = CustomerListView.SelectedItem as Customer;
            if (item == null)
            {
                MessageBox.Show("行を選択してください");
                return;
            }

            using (var conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Customer>();
                conn.Delete(item);
                ReadDatabase();
            }
        }

        private void NormalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MyRepeatButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "RepeatButton");
        }

        private void MyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ToggleButton is " + MyToggleButton.IsChecked);
        }

        private void MyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("MyCheckBox_Checked " + MyCheckBox.IsChecked);
        }

        private void MyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("MyCheckBox_Unchecked " + MyCheckBox.IsChecked);
        }

        private void MyCheckBox_Indeterminate(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("MyCheckBox_Indeterminate " + MyCheckBox.IsChecked);
        }

        private void ABRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            if (radio == ARadioButton)
            {
                Console.WriteLine("ARadioButton");
            } 
            else if (radio == BRadioButton)
            {
                Console.WriteLine("BRadioButton");
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueTextBox.Text = e.NewValue.ToString();
        }

        private void AProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ProgressTextBlock.Text = e.NewValue.ToString() + "%";
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            
            Task.Run(() =>
            {
                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(100);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AProgressBar.Value = i;
                    });
                }
            });
        }

        private void ProgressIndeterminate_Click(object sender, RoutedEventArgs e)
        {
            AProgressBar.IsIndeterminate = true;
            //Thread.Sleep(3000);
            //AProgressBar.IsIndeterminate = false;
        }

        private void ComboButton_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Index:" + MyComboBox.SelectedIndex);
            sb.AppendLine("Value:" + MyComboBox.SelectedValue);
            sb.AppendLine("Text:" + MyComboBox.Text);
            MessageBox.Show(sb.ToString());
        }

        private void ComboButton2_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Index:" + MyComboBox2.SelectedIndex);
            sb.AppendLine("Value:" + MyComboBox2.SelectedValue);
            sb.AppendLine("Text:" + MyComboBox2.Text);
            MessageBox.Show(sb.ToString());
        }

        private void ComboButton3_Click(object sender, RoutedEventArgs e)
        {
            var item = MyComboBox3.SelectedItem as Person;
            if (item != null)
            {
                MessageBox.Show($"{item.Id} {item.Name} {item.Phone}");
            }

        }

        private void ListBoxButton_Click(object sender, RoutedEventArgs e)
        {
            var item = MyListBox.SelectedItem as Member;
        }

        private void OpenTabButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Window1();
            dlg.ShowDialog();
        }

        private void MyTreeViewControl1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void MyTreeViewControl2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = MyTreeViewControl2.SelectedItem;
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 dlg = new Window1();
            dlg.ShowDialog();
        }

        private void MenuItemDock_Click(object sender, RoutedEventArgs e)
        {
            Window2 dlg = new Window2();
            dlg.ShowDialog();
        }

        private void MenuItemCanvas_Click(object sender, RoutedEventArgs e)
        {
            Window3 dlg = new Window3();
            dlg.ShowDialog();
        }
    }
}
