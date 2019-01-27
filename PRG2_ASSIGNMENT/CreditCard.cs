using System;

namespace PRG2_ASSIGNMENT
{
    class CreditCard
    {
        /* Attributes */
        private string cardNum;
        private DateTime expDate;
        private string cvv;

        /* Properties */
        public string CardNum
        {
            get { return cardNum; }
            set { cardNum = value; }
        }

        public DateTime ExpDate
        {
            get { return expDate; }
            set { expDate = value; }
        }

        public string Cvv
        {
            get { return cvv; }
            set { cvv = value; }
        }

        /* Constructors */
        public CreditCard() { }

        public CreditCard(string cn, DateTime ed, string c)
        {
            CardNum = cn;
            ExpDate = ed;
            Cvv = c;
        }

        /* Methods */
        public string getExpiryDate()
        {
            return ExpDate.ToString("MM/y");
        }
        
        public override string ToString()
        {
            return $"{CardNum}\t{ExpDate}\t{Cvv}";
        }
    }
}
