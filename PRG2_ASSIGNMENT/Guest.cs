using System;
using System.Collections.Generic;

namespace PRG2_ASSIGNMENT
{
    class Guest
    {
        /* Attributes */
        private string name;
        private string ppNumber;
        private Stay hotelStay;
        private Membership membership;
        private bool isCheckedIn;
        private List<CreditCard> creditcardList = new List<CreditCard>();

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
        public List<CreditCard> CreditcardList
        {
            get { return creditcardList; }
            set { creditcardList = value; }
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
        public void addCard(string cn, DateTime ed, string c)
        {
            CreditCard cc = new CreditCard(cn, ed, c);
            CreditcardList.Add(cc);
        }
        public override string ToString()
        {
            return $"{Name}\t{PpNumber}";
        }
    }
}
