using System;
using System.Collections.Generic;
using System.Text;

namespace _1._1.DAL.Model
{
    class PersonalCard
    {
        public long Id { get; set; }
        public float? Discount { get; set; }

        public List<Purchase> Purchases { get; set; } = new List<Purchase>();

        public UserProfile User { get; set; }

        public override string ToString()
        {
            return String.Format($"{Discount:P2}");
        }
    }
}



