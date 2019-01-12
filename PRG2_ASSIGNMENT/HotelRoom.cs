using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_ASSIGNMENT
{
    abstract class HotelRoom
    {
        /* Attributes */
        private string roomType;
        private string roomNumber;
        private string bedConfiguration;
        private int dailyRate;
        private bool isAvail;
        private int noOfOccupants;

        /* Properties */
        public string RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }
        public string RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
        public string BedConfiguration
        {
            get { return bedConfiguration; }
            set { bedConfiguration = value; }
        }
        public int DailyRate
        {
            get { return dailyRate; }
            set { dailyRate = value; }
        }
        public bool IsAvail
        {
            get { return isAvail; }
            set { isAvail = value; }
        }
        public int NoOfOccupants
        {
            get { return noOfOccupants; }
            set { noOfOccupants = value; }
        }

        /* Constructors */
        public HotelRoom() { }

        public HotelRoom(string rt, string rn, string bc, int r, bool a)
        {
            RoomType = rt;
            RoomNumber = rn;
            BedConfiguration = bc;
            DailyRate = r;
            IsAvail = a;
        }

        /* Methods */
        public abstract double CalculateCharges();

        public override string ToString()
        {
            return "to be completed";
        }
    }
}
