using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPkk2Controls.Objects
{
    public class Customer
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; }

        public Customer() { }

        public Customer(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public Customer(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - {Phone}";
        }
    }
}
