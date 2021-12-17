using System;
using System.Collections.Generic;

#nullable disable

namespace _1._2.Model
{
    public class Passenger
    {
        public Passenger()
        {
            PassInTrips = new HashSet<PassInTrip>();
        }

        public int IdPsg { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PassInTrip> PassInTrips { get; set; }
    }
}
