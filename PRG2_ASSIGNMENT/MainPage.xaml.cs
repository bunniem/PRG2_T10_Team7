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
        /* Global Lists */
        List<HotelRoom> hotelRoomList = new List<HotelRoom>();
        List<Guest> guestList = new List<Guest>();
        List<HotelRoom> availRms = new List<HotelRoom>();

        /* Global UIElement Lists */
        UIElementList frontPage = new UIElementList();
        UIElementList chkRmAvailPage = new UIElementList();
        UIElementList currentRmPage = new UIElementList();
        UIElementList chkInPage = new UIElementList();
        UIElementList hiddenchkInPage = new UIElementList();

        /* Global guest */
        bool guestexist = false;
        Guest guest = new Guest();

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

            // Add rooms to hotelRoomList
            HotelRoom[] rooms = { s101, s102, s201, s202, s203, s301, s302, d204, d205, d303, d304 };
            hotelRoomList.AddRange(rooms);


            /* Initialising existing Guests */
            // Amelia
            Stay st1 = new Stay(new DateTime(2019, 1, 26), new DateTime(2019, 02, 02));
            StandardRoom sr101 = (StandardRoom)s101;
            sr101.RequireBreakfast = true;
            sr101.RequireWifi = true;
            s101.IsAvail = false;
            st1.AddRoom(s101);
            Guest g1 = new Guest("Amelia", "S1234567A", st1, new Membership("Gold", 280), true);

            // Bob
            Stay st2 = new Stay(new DateTime(2019, 1, 25), new DateTime(2019, 1, 31));
            StandardRoom sr302 = (StandardRoom)s302;
            sr302.RequireBreakfast = true;
            s302.IsAvail = false;
            st2.AddRoom(s302);
            Guest g2 = new Guest("Bob", "G1234567A", st2, new Membership("Ordinary", 0), true);

            // Cody
            Stay st3 = new Stay(new DateTime(2019, 2, 1), new DateTime(2019, 2, 6));
            StandardRoom sr202 = (StandardRoom)s202;
            sr202.RequireBreakfast = true;
            s202.IsAvail = false;
            st3.AddRoom(s202);
            Guest g3 = new Guest("Cody", "G2345678A", st3, new Membership("Silver", 190), true);

            // Edda
            Stay st4 = new Stay(new DateTime(2019, 1, 28), new DateTime(2019, 2, 10));
            DeluxeRoom dr303 = (DeluxeRoom)d303;
            dr303.AdditionalBed = true;
            d303.IsAvail = false;
            st4.AddRoom(d303);
            Guest g4 = new Guest("Edda", "S3456789A", st4, new Membership("Gold", 10), true);

            // Add guests to guestList
            Guest[] guests = { g1, g2, g3, g4 };
            guestList.AddRange(guests);

            // Add available hotel rooms to availRms list
            foreach (HotelRoom r in hotelRoomList)
            {
                if (r.IsAvail)
                {
                    availRms.Add(r);
                }
            }

            /* UI Elements */
            // Front page
            frontPage.UIElements = new List<UIElement> { guestBlk, guestTxt, ppBlk, ppTxt, adultnoBlk, adultnoTxt, childrennoBlk, childrennoTxt, proceedBtn, searchBtn, };

            // Check rooms available page (proceed button is clicked)
            chkRmAvailPage.UIElements = new List<UIElement> { checkInDateTxt, checkOutDateTxt, chkinBlk, chkoutBlk, chkrmBtn, backBtn1 };

            // Show current rooms page (search button is clicked)           
            currentRmPage.UIElements = new List<UIElement> { guestBlk, guestTxt, ppBlk, ppTxt, currentrmBlk, currentrmLv, extendBtn, invoiceBlk, invoiceDetailBlk, memberstatusBlk, pointsBlk, pointsTxt, redeemBtn, chkoutBtn, backBtn3 };

            // Show available rooms and check in function (chkrm button is clicked)
            chkInPage.UIElements = new List<UIElement> { availrmBlk, availrmLv, selectrmBlk, selectrmLv, wifiCb, breakfastCb, bedCb, addrmBtn, removermBtn, chkinBtn, backBtn2 };

            // show available rooms and check in function (hidden elements until event happens)
            hiddenchkInPage.UIElements = new List<UIElement> { wifiCb, breakfastCb, bedCb, addrmBtn, removermBtn, chkinBtn };
        }

        public MainPage()
        {
            this.InitializeComponent();
            InitData(); // initialise all hotel rooms and existing guests

            /* UI Visibility */
            chkRmAvailPage.Hide();
            currentRmPage.Hide();
            chkInPage.Hide();
            frontPage.Show();

            statusBlk.Visibility = Visibility.Collapsed; // what to do with status block
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            guestexist = false;
            string name = guestTxt.Text;
            string ppnumber = ppTxt.Text;

            if (name == "" && ppnumber == "")
            {
                // do something if name not entered
            }
            else
            {
                if (!guestexist)
                {
                    foreach (Guest eg in guestList)
                    {
                        if (eg.Name == name || eg.PpNumber == ppnumber)
                        {
                            guest = eg;
                            guestexist = true;

                            // refresh current room listview 
                            currentrmLv.ItemsSource = null;
                            currentrmLv.ItemsSource = guest.HotelStay.RoomList;

                            // Display member status & points
                            memberstatusBlk.Text = $"Member status: {guest.Membership.Status}";
                            pointsBlk.Text = $"Points available: {guest.Membership.Points.ToString()}";

                            // Display invoice
                            double chargesPerDay = 0;
                            double noOfNights = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
                            invoiceDetailBlk.Text = "Invoice:\n\nRoom Type\tRoom No.\tBed Config.\tDaily Rate\tWi-Fi\tBreakfast\tAdd. bed\tCharges\n";                            
                            foreach(HotelRoom r in guest.HotelStay.RoomList)
                            {
                                invoiceDetailBlk.Text += r.ToString() + "\n";
                                chargesPerDay += r.CalculateCharges();
                            }
                            invoiceDetailBlk.Text += $"\nCharges per day: ${chargesPerDay}\nDuration of stay: {noOfNights} days\n\nTotal charges: ${guest.HotelStay.CalculateTotal()}";

                            /* UI visibility */
                            frontPage.Hide();
                            currentRmPage.Show();

                            break;
                        }
                    }
                }
                if (!guestexist)
                {
                    // do something if guest does not exist
                }
            }
        }

        private void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            guestexist = false;
            string name = guestTxt.Text;
            string ppnumber = ppTxt.Text;

            if (name == "" && ppnumber == "")
            {
                //do something if no name or ppnumber entered
            }
            else
            {
                // search guest by name or passport number
                foreach (Guest eg in guestList)
                {
                    if (eg.Name == name || eg.PpNumber == ppnumber)
                    {
                        guest = eg;
                        guestexist = true;
                        break;
                    }
                }
                if (guestexist)
                {
                    if (guest.IsCheckedIn)
                    {
                        // error: guest still checked into hotel, need to check out to check in more rooms
                    }
                    else // existing guest, but not checked into hotel
                    {
                        /* UI Visibility */
                        frontPage.Hide();
                        chkRmAvailPage.Show();
                    }
                }
                else
                {
                    if (name != "" && ppnumber != "")
                    {
                        Guest ng = new Guest(name, ppnumber, new Stay(), new Membership(), false); // create new guest if guest not found in database
                        guest = ng;

                        /* UI Visibility */
                        frontPage.Hide();
                        chkRmAvailPage.Show();
                    }
                    else
                    {
                        // error: not all fields filled in
                    }
                }
            }
        }

        private void ChkrmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!checkInDateTxt.Date.HasValue || !checkOutDateTxt.Date.HasValue)
            {
                // error: either field not filled in
            }
            else if (checkInDateTxt.Date > checkOutDateTxt.Date)
            {
                // error: checkoutdate earlier than checkindate
            }
            else
            {
                // Refresh availrm listview
                availrmLv.ItemsSource = null;
                availrmLv.ItemsSource = availRms;

                // Refresh selectrm listview
                selectrmLv.ItemsSource = null;
                selectrmLv.ItemsSource = guest.HotelStay.RoomList;

                /* UI Visibility */
                chkRmAvailPage.Hide();
                chkInPage.Show();
                hiddenchkInPage.Hide();
            }
        }

        private void AvailrmLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addrmBtn.Visibility = Visibility.Visible;

            // hide all checkboxes
            wifiCb.Visibility = Visibility.Collapsed;
            breakfastCb.Visibility = Visibility.Collapsed;
            bedCb.Visibility = Visibility.Collapsed;

            // show checkBoxes that apply for selected room
            if (availrmLv.SelectedItem is DeluxeRoom)
            {
                bedCb.Visibility = Visibility.Visible;
            }
            else
            {
                wifiCb.Visibility = Visibility.Visible;
                breakfastCb.Visibility = Visibility.Visible;
            }
        }

        private void AddrmBtn_Click(object sender, RoutedEventArgs e)
        {
            HotelRoom r = (HotelRoom)availrmLv.SelectedItem;
            if (r is null)
            {
                // error: no room selected
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
                else if (r is StandardRoom sr)
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

                /* Add selected room to guest's roomList and refresh selectedrm listview  */
                guest.HotelStay.AddRoom(r);
                selectrmLv.ItemsSource = null;
                selectrmLv.ItemsSource = guest.HotelStay.RoomList;

                /* Remove selected room from avail room list and refresh */
                availRms.Remove(r);
                availrmLv.ItemsSource = null;
                availrmLv.ItemsSource = availRms;

                /* Uncheck checkboxes */
                wifiCb.IsChecked = false;
                breakfastCb.IsChecked = false;
                bedCb.IsChecked = false;

                /* UI Visibility */
                hiddenchkInPage.Hide();
                chkinBtn.Visibility = Visibility.Visible;
            }
        }

        private void SelectrmLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removermBtn.Visibility = Visibility.Visible;
        }

        private void RemovermBtn_Click(object sender, RoutedEventArgs e)
        {
            HotelRoom r = (HotelRoom)selectrmLv.SelectedItem;
            if (r is null)
            {
                // error: no room selected
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

                /* Remove selected room from guest's roomList and refresh */
                guest.HotelStay.RoomList.Remove(r);
                selectrmLv.ItemsSource = null;
                selectrmLv.ItemsSource = guest.HotelStay.RoomList;

                /* Add selected room to available room list and refresh */
                r.IsAvail = true; // room made available
                availRms.Add(r);
                availRms.Sort(); // sort roomList by room number
                availrmLv.ItemsSource = null;
                availrmLv.ItemsSource = availRms;

                /* Uncheck checkboxes */
                wifiCb.IsChecked = false;
                breakfastCb.IsChecked = false;
                bedCb.IsChecked = false;

                /* UI Visibility */
                hiddenchkInPage.Hide();
                if (guest.HotelStay.RoomList.Any()) //guest's roomList contains at least one room
                {
                    chkinBtn.Visibility = Visibility.Visible;
                }
            }
        }

        private void ChkinBtn_Click(object sender, RoutedEventArgs e)
        {
            /* Set checkindate & checkoutdate of stay */
            guest.HotelStay.CheckInDate = checkInDateTxt.Date.Value.DateTime;
            guest.HotelStay.CheckOutDate = checkOutDateTxt.Date.Value.DateTime;
            guest.Membership.Status = "Ordinary";
            guest.Membership.Points = 0;

            guest.HotelStay.RoomList.Sort(); // sort roomList by room number

            guest.IsCheckedIn = true; // guest is checked in
            if (!guestexist)
            {
                guestList.Add(guest);
            }

            /* Reset all fields to blank */
            guestTxt.Text = "";
            ppTxt.Text = "";
            checkInDateTxt.Date = null;
            checkOutDateTxt.Date = null;

            /* UI Visibilty */
            chkInPage.Hide();
            frontPage.Show();
        }

        /* Back buttons for navigation */
        private void BackBtn1_Click(object sender, RoutedEventArgs e)
        {
            chkRmAvailPage.Hide();
            frontPage.Show();
            
            /* Reset values of date to null */
            checkInDateTxt.Date = null;
            checkOutDateTxt.Date = null;
        }

        private void BackBtn2_Click(object sender, RoutedEventArgs e)
        {
            chkInPage.Hide();
            chkRmAvailPage.Show();

            /* Add all selected rooms back to available room list */
            foreach(HotelRoom r in guest.HotelStay.RoomList.ToList())
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
                /* Remove selected room from guest's roomList */
                guest.HotelStay.RoomList.Remove(r);

                /* Add selected room to available room list */
                r.IsAvail = true; // room made available
                availRms.Add(r);
            }
            availRms.Sort(); // sort available room list
        }

        private void BackBtn3_Click(object sender, RoutedEventArgs e)
        {
            currentRmPage.Hide();
            frontPage.Show();
        }
    }

}
