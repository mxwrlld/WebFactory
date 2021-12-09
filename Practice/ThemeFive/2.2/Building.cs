using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    class Building : IInventory
    {
        public string Number { get; }

        public string Address { get; set; }

        public Building(string address)
        {
            Address = address;
            Number = InventoryAccounting.GetInventoryNumber(PropertyType.BuildingAndStructures);
        }
    }
}
