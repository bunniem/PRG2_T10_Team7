using System;
using System.Collections.Generic;

namespace PRG2_ASSIGNMENT
{
    class Stay
    {
        /* Attributes */
        private List<HotelRoom> roomList = new List<HotelRoom>();
        private DateTime checkInDate;
        private DateTime checkOutDate;

        /* Properties */
        public List<HotelRoom> RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }
        public DateTime CheckInDate
        {
            get { return checkInDate; }
            set { checkInDate = value; }
        }
        public DateTime CheckOutDate
        {
            get { return checkOutDate; }
            set { checkOutDate = value; }
        }

        /* Constructors */
        public Stay() { }

        public Stay(DateTime i, DateTime o)
        {
            CheckInDate = i;
            CheckOutDate = o;
        }

        /* Methods */
        public void AddRoom(HotelRoom r)
        {
            RoomList.Add(r);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach(HotelRoom r in RoomList)
            {
                double days = (checkOutDate - checkInDate).TotalDays;
                total += (r.CalculateCharges() * days);
            }
            return total;
        }

        public override string ToString()
        {
            return "something"; //to be done
        }

    }
}
