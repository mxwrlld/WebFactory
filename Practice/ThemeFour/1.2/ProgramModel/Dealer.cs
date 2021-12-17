using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1.ProgramModel
{
    class Dealer : Provider
    {
        public Manufacturer Manufacturer { get; }

        public override HashSet<Product> Nomenclature => Manufacturer.Nomenclature;


        public double ExtraCharge { get; set; }

        public Dealer(string tin, string name, string address, Manufacturer manufacturer, double extraCharge) : base(tin, name, address)
        {
            Manufacturer = manufacturer;
            ExtraCharge = extraCharge;
        }

        public decimal GetPriceWithExtraCharge(Product product)
        {
            decimal price = product.Price;
            return price + (price * (decimal)ExtraCharge);
        }

        public override string ToString()
        {
            return String.Format($"{Name} - {TIN} - {Address} ({Manufacturer.Name})\n");
        }
    }
}
