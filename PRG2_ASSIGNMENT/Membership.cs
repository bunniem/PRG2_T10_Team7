using System;

namespace PRG2_ASSIGNMENT
{
    class Membership
    {
        /* Attributes */
        private string status;
        private int points;
        private bool silverStatus = false;
        private bool goldStatus = false;

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
            int earn = Convert.ToInt32(p / 10);
            Points += earn;
            if (!goldStatus)
            {
                if (Points >= 200)
                {
                    Status = "Gold";
                    goldStatus = true;
                    silverStatus = true;
                }
            }
            if (!silverStatus)
            {
                if (Points >= 100)
                {
                    Status = "Silver";
                    silverStatus = true;
                }
            }
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
            return $"Member status: {Status}\nPoints available: {Points}";
        }
    }
}
