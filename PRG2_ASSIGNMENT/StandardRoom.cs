using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    class StandardRoom : HotelRoom
    {
        /* Attributes */
        private bool requireWifi;
        private bool requireBreakfast;

        /* Properties */
        public bool RequireWifi
        {
            get { return requireWifi; }
            set { requireWifi = value; }
        }
        public bool RequireBreakfast
        {
            get { return requireBreakfast; }
            set { requireBreakfast = value; }
        }

        /* Constructors */
        public StandardRoom() : base() { }

        public StandardRoom(string rn, string bc, double r, bool a, int no) : base("Standard", rn, bc, r, a, no) { }

        /* Methods */
        public override double CalculateCharges()
        {
            double rate = DailyRate;
            /* Addons */
            if(RequireWifi)
            {
                rate += 10;
            }
            if(RequireBreakfast)
            {
                rate += 20;
            }

            return rate;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
