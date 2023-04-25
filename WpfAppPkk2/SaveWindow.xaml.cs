using SQLite;
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
using WpfAppPkk2Controls.Objects;

namespace WpfAppPkk2Controls
{
    /// <summary>
    /// SaveWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SaveWindow : Window
    {
        private Customer _customer;
        public int ret;
        public SaveWindow(Customer customer)
        {
            InitializeComponent();

            _customer = customer;

            if (customer != null )
            {
                NameTextBox.Text = customer.Name;
                PhoneTextBox.Text = customer.Phone;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Customer>();
                if (_customer == null)
                {
                    conn.Insert(new Customer(NameTextBox.Text, PhoneTextBox.Text));
                }
                else
                {
                    conn.Update(new Customer(_customer.Id, NameTextBox.Text, PhoneTextBox.Text));
                }
            }

            ret = 3;
            Close();
        }
    }
}
