using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    class Device : IInventory
    {
        public string Number { get; }

        public Device()
        {
            Number = InventoryAccounting.GetInventoryNumber(PropertyType.Appliances);
        }
    }
}
