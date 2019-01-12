using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    class Stay
    {
        private List<HotelRoom> roomList = new List<HotelRoom>();

        public List<HotelRoom> RoomList
        {
            get { return roomList; }
            set { roomList = value; }
        }

        private DateTime checkInDate;

        public DateTime CheckInDate
        {
            get { return checkInDate; }
            set { checkInDate = value; }
        }
        private DateTime checkOutDate;

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

        }

        public double CalculateTotal()
        {
            return 1.0; //to be done
        }

        public override string ToString()
        {
            return "something"; //to be done;
        }

    }
}
