using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _2._1.ProgramModel
{
    abstract class Provider
    {
        private string tin;
        public string TIN
        {
            get => tin;
            set
            {
                Regex regex = new Regex(@"^\d{10}|\d{12}$");
                if (regex.IsMatch(value))
                    tin = value;
                else
                    throw new ArgumentException("ИНН должен состоять из 10 цифр");
            }
        }
        public string Name { get; set; }
        public string Address { get; set; }

        public abstract HashSet<Product> Nomenclature { get; }

        public Provider(string tIN, string name, string address)
        {
            TIN = tIN;
            Name = name;
            Address = address;
        }


    }
}
