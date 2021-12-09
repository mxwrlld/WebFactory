using System;
using System.Collections.Generic;
using System.Text;

namespace _2._1
{
    class Person : IComparable
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime BirthDate { get; set; }

        public Person(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }


        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Person otherPerson = obj as Person;
            if(otherPerson != null)
            {
                int namesCompare = this.Name.CompareTo(otherPerson.Name);
                if (namesCompare != 0)
                    return namesCompare;

                int surnamesCompare = this.Surname.CompareTo(otherPerson.Surname);
                if (surnamesCompare != 0)
                    return surnamesCompare;

                int birthDatesCompare = this.BirthDate.CompareTo(otherPerson.BirthDate);
                if (birthDatesCompare != 0)
                    return birthDatesCompare;

                return 0;

            }
            else
            {
                throw new ArgumentException("Object is not a Person");
            }
        }
    }
}
