using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    static class InventoryAccounting
    {
        private static int amountOfInventoryNumbers = 1;
        private static Dictionary<PropertyType, int> ordinalByType = new Dictionary<PropertyType, int>()
        {
            [PropertyType.BuildingAndStructures] = 1,    
            [PropertyType.Furniture] = 1,    
            [PropertyType.Appliances] = 1,    
            [PropertyType.LibraryFunds] = 1    
        };


        public static string GetInventoryNumber(PropertyType propertyType)
        {

            InventoryNumber inventoryNumber = new InventoryNumber(propertyType, ordinalByType[propertyType],
                DateTime.Now.Year, amountOfInventoryNumbers);
            amountOfInventoryNumbers += 1;
            ordinalByType[propertyType] += 1;
            return inventoryNumber.ToString(); 
        }

    }
}
