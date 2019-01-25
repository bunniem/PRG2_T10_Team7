﻿namespace PRG2_ASSIGNMENT
{
    class Guest
    {
        /* Attributes */
        private string name;
        private string ppNumber;
        private Stay hotelStay;
        private Membership membership;
        private bool isCheckedIn;

        /* Properties */
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PpNumber
        {
            get { return ppNumber; }
            set { ppNumber = value; }
        }
        public Stay HotelStay
        {
            get { return hotelStay; }
            set { hotelStay = value; }
        }
        public Membership Membership
        {
            get { return membership; }
            set { membership = value; }
        }
        public bool IsCheckedIn
        {
            get { return isCheckedIn; }
            set { isCheckedIn = value; }
        }

        /* Constructors */
        public Guest() { }
        
        public Guest(string n, string p, Stay s, Membership m, bool i)
        {
            Name = n;
            PpNumber = p;
            HotelStay = s;
            Membership = m;
            IsCheckedIn = i;
        }

        /* Methods */
        public override string ToString()
        {
            return name + ppNumber; //to be done
        }
    }
}
