using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPkk2.Objects
{
    internal class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees { get; set;} = new List<Employee>();

        public Employee(string id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
