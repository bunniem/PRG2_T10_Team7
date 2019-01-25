namespace PRG2_ASSIGNMENT
{
    class DeluxeRoom : HotelRoom
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
        public DeluxeRoom(string rn, string bc, double r, bool a, int no) : base("Deluxe", rn, bc, r, a, no) { }

        public override double CalculateCharges()
        {
            double rate = DailyRate;
            /* Addons */
            if (AdditionalBed)
            {
                rate += 25;
            }

            return rate;
        }

        /* Methods */
        public override string ToString()
        {
            return base.ToString() + $"\tTrue\tTrue\t{AdditionalBed}\t${CalculateCharges()}";
        }
    }
}