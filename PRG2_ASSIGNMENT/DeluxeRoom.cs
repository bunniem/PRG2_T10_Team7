using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    class DeluxeRoom:HotelRoom
    {
        /* Attributes */
        private bool additionalBed;

        /* Properties */
        public bool AdditionalBed
        {
            get { return additionalBed; }
            set { additionalBed = value; }
        }

        /* Constructors */
        public DeluxeRoom() : base() { }

        /* Methods */
        public override double CalculateCharges()
        {
            return 1.0;
        }
    }
}
