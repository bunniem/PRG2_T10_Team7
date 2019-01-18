using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PRG2_ASSIGNMENT
{
    /* Creating Standard Rooms */
    public sealed partial class MainPage : Page
    {
        /* Global List of HotelRoom and Guest */
        List<HotelRoom> hotelRoomList = new List<HotelRoom>();
        List<Guest> guestList = new List<Guest>();
        List<HotelRoom> selectedRoomList = new List<HotelRoom>();
        List<HotelRoom> availRms = new List<HotelRoom>();

        public void InitData()
        {
            /* Initialising Rooms */
            HotelRoom s101 = new StandardRoom("101", "Single", 90.0, true, 1);
            HotelRoom s102 = new StandardRoom("102", "Single", 90.0, true, 1);
            HotelRoom s201 = new StandardRoom("201", "Twin", 110.0, true, 2);
            HotelRoom s202 = new StandardRoom("202", "Twin", 110.0, true, 2);
            HotelRoom s203 = new StandardRoom("203", "Twin", 110.0, true, 2);
            HotelRoom d204 = new DeluxeRoom("204", "Twin", 140.0, true, 3);
            HotelRoom d205 = new DeluxeRoom("205", "Twin", 140.0, true, 3);
            HotelRoom s301 = new StandardRoom("301", "Triple", 120.0, true, 3);
            HotelRoom s302 = new StandardRoom("302", "Triple", 120.0, true, 3);
            HotelRoom d303 = new DeluxeRoom("303", "Triple", 210.0, true, 4);
            HotelRoom d304 = new DeluxeRoom("304", "Triple", 210.0, true, 4);

            // add rooms to hotelRoomList
            HotelRoom[] rooms = { s101, s102, s201, s202, s203, s301, s302, d204, d205, d303, d304 };
            hotelRoomList.AddRange(rooms);

            /* Initialising Guests */
            // Amelia
            Stay st1 = new Stay(new DateTime(2019 - 01 - 26), new DateTime(2019 - 02 - 02));
            StandardRoom sr101 = (StandardRoom)s101;
            sr101.RequireBreakfast = true;
            sr101.RequireWifi = true;
            s101.IsAvail = false;
            st1.AddRoom(s101);
            Guest g1 = new Guest("Amelia", "S1234567A", st1, new Membership("Gold", 280), true);

            // Bob
            Stay st2 = new Stay(new DateTime(2019 - 01 - 25), new DateTime(2019 - 01 - 31));
            StandardRoom sr302 = (StandardRoom)s302;
            sr302.RequireBreakfast = true;
            s302.IsAvail = false;
            st2.AddRoom(s302);
            Guest g2 = new Guest("Bob", "G1234567A", st2, new Membership("Ordinary", 0), true);

            // Cody
            Stay st3 = new Stay(new DateTime(2019 - 02 - 01), new DateTime(2019 - 02 - 06));
            StandardRoom sr202 = (StandardRoom)s202;
            sr202.RequireBreakfast = true;
            s202.IsAvail = false;
            st3.AddRoom(s202);
            Guest g3 = new Guest("Cody", "G2345678A", st3, new Membership("Silver", 190), true);

            // Edda
            Stay st4 = new Stay(new DateTime(2019 - 01 - 28), new DateTime(2019 - 02 - 10));
            DeluxeRoom dr303 = (DeluxeRoom)d303;
            dr303.AdditionalBed = true;
            d303.IsAvail = false;
            st4.AddRoom(d303);
            Guest g4 = new Guest("Edda", "S3456789A", st4, new Membership("Gold", 10), true);

            // add guests to guestList
            Guest[] guests = { g1, g2, g3, g4 };
            guestList.AddRange(guests);
        }

        public MainPage()
        {
            this.InitializeComponent();
            InitData(); // initialise all hotel rooms and existing guests
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            bool guestmatch = false;
            string name = guestTxt.Text;
            string ppnumber = ppTxt.Text;

            foreach (Guest m in guestList)
            {
                if (m.Name == name && m.PpNumber == ppnumber)
                {
                    memberBlk.Text = $"Member status: {m.Membership.Status}";
                    pointsBlk.Text = $"Points available: {m.Membership.Points}";
                    guestmatch = true;
                    break;
                }
            }

            if (!guestmatch)
            {
                Guest g = new Guest()
                {
                    Name = name, PpNumber = ppnumber
                };
            }
        }

        private void ChkrmBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (HotelRoom r in hotelRoomList)
            {
                if (r.IsAvail)
                {
                    availRms.Add(r);
                }
            }
            availrmLv.ItemsSource = availRms;
        }

        private void AvailrmLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // reset all checkBoxes to visible
            List<CheckBox> checkBoxes = new List<CheckBox> { wifiCb, breakfastCb, bedCb };
            foreach(CheckBox cb in checkBoxes)
            {
                cb.Visibility = Visibility.Visible;
            }

            // hide checkBoxes that do not apply to selected room
            if (availrmLv.SelectedItem is DeluxeRoom)
            {
                wifiCb.Visibility = Visibility.Collapsed;
                breakfastCb.Visibility = Visibility.Collapsed;
            }
            else
            {
                bedCb.Visibility = Visibility.Collapsed;
            }
        }

        private void AddrmBtn_Click(object sender, RoutedEventArgs e)
        {
            HotelRoom r = (HotelRoom)availrmLv.SelectedItem;
            if (r == null)
            {
                // error (no room selected)
            }
            else
            {
                r.IsAvail = false; // room not available
                /* set addon booleans for rooms based on checkboxes */
                if (r is DeluxeRoom dr)
                {
                    if (bedCb.IsChecked == true)
                    {
                        dr.AdditionalBed = true;
                    }
                }
                else if(r is StandardRoom sr)
                {
                    if (breakfastCb.IsChecked == true)
                    {
                        sr.RequireBreakfast = true;
                    }
                    if (wifiCb.IsChecked == true)
                    {
                        sr.RequireWifi = true;
                    }
                }

                /* Add selected room to selected list and refresh */
                selectedRoomList.Add(r);
                selectrmLv.ItemsSource = null;
                selectrmLv.ItemsSource = selectedRoomList;

                /* Remove selected room from avail room list and refresh */
                availRms.Remove(r);
                availrmLv.ItemsSource = null;
                availrmLv.ItemsSource = availRms;

                /* Reset checkboxes to unchecked */
                wifiCb.IsChecked = false;
                breakfastCb.IsChecked = false;
                bedCb.IsChecked = false;
            }
        }

        private void RemovermBtn_Click(object sender, RoutedEventArgs e)
        {
            HotelRoom r = (HotelRoom)selectrmLv.SelectedItem;
            if (r == null)
            {
                // error (no room selected)
            }
            else
            {
                /* Set addon booleans for rooms to false */
                if (r is DeluxeRoom dr)
                {
                    dr.AdditionalBed = false;
                }
                else if (r is StandardRoom sr)
                {
                    sr.RequireBreakfast = false;
                    sr.RequireWifi = false;
                }
                /* Remove selected room from list and refresh */
                selectedRoomList.Remove(r);
                selectrmLv.ItemsSource = null;
                selectrmLv.ItemsSource = selectedRoomList;

                /* Add selected room to available room list and refresh */
                availRms.Add(r);
                r.IsAvail = true; // room made available
                availRms.Sort();
                availrmLv.ItemsSource = null;
                availrmLv.ItemsSource = availRms;

                /* Reset checkboxes to unchecked */
                wifiCb.IsChecked = false;
                breakfastCb.IsChecked = false;
                bedCb.IsChecked = false;
            }
        }

        private void ChkinBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: get check in & check out date and add to stay, create a new guest (?) and add to guestList
            //Stay s = new Stay(selectedRoomList, );
        }
    }

}
