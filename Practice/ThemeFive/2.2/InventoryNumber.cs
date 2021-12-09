using System;
using System.Collections.Generic;
using System.Text;

namespace _2._2
{
    class InventoryNumber
    {
        // 14 цифр, разделенных дефисами: XX-XXXX-XX-XXXXXX
        private PropertyType PropertyType { get; set; }
        private int SerialNumberOfCategory { get; set; }

        private int yearOfRegistration;
        private int YearOfRegistration
        {
            get => yearOfRegistration;
            set
            {
                yearOfRegistration = int.Parse(value.ToString().Substring(2, 2));
            }
        }

        private int UniversalSerialNumber { get; set; }

        public InventoryNumber(PropertyType propertyType, int serialNumberOfCategory, int yearOfRegistration, int universalSerialNumber)
        {
            PropertyType = propertyType;
            SerialNumberOfCategory = serialNumberOfCategory;
            YearOfRegistration = yearOfRegistration;
            UniversalSerialNumber = universalSerialNumber;
        }

        public override string ToString()
        {
            return (String.Format("{0:00}", ((int)PropertyType)) + "-"
                + String.Format("{0:0000}", SerialNumberOfCategory) + "-"
                + String.Format("{0:00}", YearOfRegistration) + "-"
                + String.Format("{0:000000}", UniversalSerialNumber)
                );
        }
    }
}
