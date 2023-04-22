using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppPkk2.Objects
{
    [Obsolete("This is outedated.")]
    internal class Member
    {
        public string FileName { get; set; }
        public string Name { get; set; }

        public Member(string fileName, string name)
        {
            FileName = fileName;
            Name = name;
        }
    }
}
