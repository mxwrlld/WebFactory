using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    class Book: IInventory
    {
        public string Number { get; }
        public string Name { get; set; }
        public string[] Authors { get; set; }

        public int YearOfPublishing { get; set; }

        
        public Book(string name, string[] authors, int yearOfPublishing)
        {
            Name = name;
            Authors = authors;
            YearOfPublishing = yearOfPublishing;
            Number = InventoryAccounting.GetInventoryNumber(PropertyType.LibraryFunds);
        }
    }
}
