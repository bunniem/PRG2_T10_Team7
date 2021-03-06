﻿using System;

namespace PRG2_ASSIGNMENT
{
    abstract class HotelRoom : IComparable<HotelRoom>
    {
        /* Attributes */
        private string roomType;
        private string roomNumber;
        private string bedConfiguration;
        private double dailyRate;
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
        public double DailyRate
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

        public HotelRoom(string rt, string rn, string bc, double r, bool a, int no)
        {
            RoomType = rt;
            RoomNumber = rn;
            BedConfiguration = bc;
            DailyRate = r;
            IsAvail = a;
            NoOfOccupants = no;
        }

        /* Methods */
        public abstract double CalculateCharges();

        public override string ToString()
        {
            return $"{RoomType}\t\t{RoomNumber}\t{BedConfiguration}\t\t${DailyRate}";
        }

        public int CompareTo (HotelRoom r) // used for sorting
        {
            return RoomNumber.CompareTo(r.RoomNumber);
        }
    }
}
