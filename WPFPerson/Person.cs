using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPerson
{
    class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsMale { get; set; }
        public bool HasDriversLicence { get; set; }

        public Person()
        {

        }

        override
        public String ToString()
        {            
            return $"{Firstname} {Lastname} - {Birthdate.ToString("dd.MM.yyyy")} [{IsMale}, {HasDriversLicence}]";
        }
    }
}
