using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _2._1.ProgramModel
{
    class Product
    {
        private string vendorCode;
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                Regex regex = new Regex(@"^\d{10,15}$");
                if (regex.IsMatch(value))
                    vendorCode = value;
                else
                    throw new ArgumentOutOfRangeException("Артикул - набор из 10-15 цифр");
            }
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string vendorCode, string name, decimal price)
        {
            VendorCode = vendorCode;
            Name = name;
            Price = price;
        }

        public Product(Product product)
        {
            VendorCode = product.VendorCode;
            Name = product.Name;
            Price = product.Price;
        }

        public override string ToString()
        {
            return String.Format($"{Name} - {Price:C2}");
        }
    }
}
