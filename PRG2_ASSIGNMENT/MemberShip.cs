using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    class Membership
    {
        /* Attributes */
        private string status;
        private int points;

        /* Properties */
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        /* Constructors */
        public Membership() { }

        public Membership(string s, int p)
        {
            Status = s;
            Points = p;
        }

        /* Methods */
        public void EarnPoints(double p)
        {
            Points += Convert.ToInt32(p); //earn points is double?
        }

        public bool RedeemPoints(int p)
        {
            if (p <= Points)
            {
                Points -= p;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
