using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1.ProgramModel
{
    class Manufacturer : Provider
    {
        public override HashSet<Product> Nomenclature { get; }

        public Manufacturer(string tin, string name, string address, List<Product> nomenclature) : base(tin, name, address)
        {
            Nomenclature = new HashSet<Product>();

            foreach (var unit in nomenclature)
            {
                Nomenclature.Add(unit);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Manufacturer manufacturer &&
                   TIN == manufacturer.TIN &&
                   Name == manufacturer.Name &&
                   Address == manufacturer.Address;
        }

        public override string ToString()
        {
            return String.Format($"{Name} - {TIN} - {Address}\n");
        }
    }
}
