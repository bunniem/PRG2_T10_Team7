using System;

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
            int earn = Convert.ToInt32(p / 10);
            Points += earn;
            if (Status == "Ordinary" || Status == "Silver")
            {
                if (Points >= 100 && Points < 200)
                {
                    Status = "Silver";
                }
                else if (Points >= 200)
                {
                    Status = "Gold";
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
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Member status: {Status}\nPoints available: {Points}";
        }
    }
}
