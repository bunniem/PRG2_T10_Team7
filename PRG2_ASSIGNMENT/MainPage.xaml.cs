using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        UIElementList statusMsg = new UIElementList();

        /* Global guest */
        bool guestexist = false;
        Guest guest = new Guest();
        int redeempoints = 0;

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
            frontPage.UIElements = new List<UIElement> { guestBlk, guestTxt, ppBlk, ppTxt, adultnoBlk, adultnoTxt, childrennoBlk, childrennoTxt, proceedBtn, searchBtn };

            // Check rooms available page (proceed button is clicked)
            chkRmAvailPage.UIElements = new List<UIElement> { checkInDateTxt, checkOutDateTxt, chkinBlk, chkoutBlk, chkrmBtn, backBtn1 };

            // Show current rooms page (search button is clicked)           
            currentRmPage.UIElements = new List<UIElement> { guestBlk, guestTxt, ppBlk, ppTxt, currentrmBlk, currentrmLv, extendBtn, invoiceBlk, invoiceDetailBlk, invoiceDetailScroll, statuspointsBlk, pointsTxt, redeemBtn, chkoutBtn, backBtn3 };

            // Show available rooms and check in function (chkrm button is clicked)
            chkInPage.UIElements = new List<UIElement> { availrmBlk, availrmLv, selectrmBlk, selectrmLv, wifiCb, breakfastCb, bedCb, addrmBtn, removermBtn, chkinBtn, backBtn2 };

            // Show available rooms and check in function (hidden elements until event happens)
            hiddenchkInPage.UIElements = new List<UIElement> { wifiCb, breakfastCb, bedCb, addrmBtn, removermBtn, chkinBtn };

            // Show Status messages (for error messages or informational messages)
            statusMsg.UIElements = new List<UIElement> { statusBlk, hideBtn };
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
        }

        public void PrintInvoice() // print invoice
        {
            double chargesPerDay = 0;
            double noOfNights = (guest.HotelStay.CheckOutDate - guest.HotelStay.CheckInDate).TotalDays;
            invoiceDetailBlk.Text = "\nRoom Type\tNo.\tBed Config.\tRate\tWi-Fi\tBreakfast\tAdd. bed\tCharges\n";
            foreach (HotelRoom r in guest.HotelStay.RoomList)
            {
                invoiceDetailBlk.Text += r.ToString() + "\n";
                chargesPerDay += r.CalculateCharges();
            }
            invoiceDetailBlk.Text += $"\nCharges per day: ${chargesPerDay}\nDuration of stay: {noOfNights} days\n\nTotal charges: ${guest.HotelStay.CalculateTotal()}";
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            statusMsg.Hide(); // hide status message
            guestexist = false;
            bool namematch = false;
            bool ppmatch = false;
            string name = guestTxt.Text;
            string ppnumber = ppTxt.Text;

            if (name == "" && ppnumber == "")
            {
                // error: name and ppnumber not entered
                statusBlk.Text = "Error: To view an existing guest's checked in rooms, enter either their name or passport number!";
                statusMsg.Show();
            }
            else
            {
                // disable input for textboxes
                guestTxt.IsReadOnly = true;
                ppTxt.IsReadOnly = true;

                // match name with existing users, and set bool to true if match
                foreach (Guest g in guestList)
                {
                    if (g.Name == name)
                    {
                        namematch = true;
                    }
                }

                // match pp number with existing users, and set bool to true if match
                foreach (Guest g in guestList)
                {
                    if (g.PpNumber == ppnumber)
                    {
                        ppmatch = true;
                    }
                }

                // check booleans namematch and ppmatch to determine the result
                if (!namematch && !ppmatch)
                {
                    // Error: No existing guest with matching Name or Passport No.
                    statusBlk.Text = "Error: No existing guest with matching Name or Passport Number!";
                    statusMsg.Show();

                    /* Set fields to allow input */
                    guestTxt.IsReadOnly = false;
                    ppTxt.IsReadOnly = false;
                }
                else if (namematch && !ppmatch)
                {
                    // use name to match with existing guest
                    foreach (Guest eg in guestList)
                    {
                        if (eg.Name == name)
                        {
                            guest = eg;
                        }
                    }
                    guestexist = true;

                    statusBlk.Text = $"Guest matched via name: {name}";
                    statusMsg.Show();
                }
                else if (!namematch && ppmatch)
                {
                    // use passport number to match with existing guest
                    foreach (Guest eg in guestList)
                    {
                        if (eg.PpNumber == ppnumber)
                        {
                            guest = eg;
                        }
                    }
                    guestexist = true;

                    statusBlk.Text = $"Guest matched via Passport number: {ppnumber}";
                    statusMsg.Show();
                }
                else if (namematch && ppmatch)
                {

                    // name and pp number matches the same guest
                    foreach (Guest eg in guestList)
                    {
                        if (eg.Name == name && eg.PpNumber == ppnumber)
                        {
                            guest = eg;
                            guestexist = true;
                            break;
                        }
                    }
                    // name and pp number does not match the same guest
                    if (!guestexist)
                    {
                        // use passport number to match existing guest
                        foreach (Guest eg in guestList)
                        {
                            if (eg.PpNumber == ppnumber)
                            {
                                guest = eg;
                                guestexist = true;

                                statusBlk.Text = $"Guest matched via Passport number: {ppnumber}";
                                statusMsg.Show();
                                break;
                            }
                        }
                    }
                }
                if (guestexist) // Only runs if guest exists
                {
                    // Refresh current room listview 
                    currentrmLv.ItemsSource = null;
                    currentrmLv.ItemsSource = guest.HotelStay.RoomList;

                    // Display member status & points
                    statuspointsBlk.Text = guest.Membership.ToString();

                    // Display invoice
                    if (guest.IsCheckedIn)
                    {
                        PrintInvoice();
                    }
                    else
                    {
                        invoiceDetailBlk.Text = "Guest is currently not checked in.";
                    }

                    /* UI visibility */
                    frontPage.Hide();
                    currentRmPage.Show();
                    if (guest.Membership.Status == "Ordinary") // Hide redeem button from ordinary members
                    {
                        pointsTxt.Visibility = Visibility.Collapsed;
                        redeemBtn.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void ProceedBtn_Click(object sender, RoutedEventArgs e)
        {
            statusMsg.Hide();
            guestexist = false;
            string name = guestTxt.Text;
            string ppnumber = ppTxt.Text;

            if (name == "" || ppnumber == "")
            {
                // error: no name or ppnumber entered
                statusBlk.Text = "Error: Guests need to enter both name and passport number fields!";
                statusMsg.Show();
            }
            else if (childrennoTxt.Text == "" || adultnoTxt.Text == "")
            {
                // error: no number of occupants entered
                statusBlk.Text = "Error: Number of guests not entered for adult / children!\nIf there is no children, please put '0'.";
                statusMsg.Show();
            }
            else if (adultnoTxt.Text == "0")
            {
                // error: no adults entered
                statusBlk.Text = "Error: There must be at least 1 adult!";
                statusMsg.Show();
            }
            else if (!int.TryParse(adultnoTxt.Text, out int adultNo) || !int.TryParse(childrennoTxt.Text, out int childrenNo))
            {
                statusBlk.Text = "Error: Non-numerical characters entered for pax!";
                statusMsg.Show();
            }
            else
            {
                // search guest by name and passport number
                foreach (Guest eg in guestList)
                {
                    if (eg.Name == name && eg.PpNumber == ppnumber)
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
                        statusBlk.Text = "Error: Guest is already checked in the hotel. Check out to check in more rooms!";
                        statusMsg.Show();
                    }
                    else // existing guest, not checked into hotel
                    {
                        /* UI Visibility */
                        frontPage.Hide();
                        chkRmAvailPage.Show();
                        statusMsg.Hide();
                    }
                }
                else
                {
                    bool namematch = false;
                    bool ppmatch = false;

                    // check if name or passport number match any existing users
                    foreach (Guest g in guestList)
                    {
                        if(g.Name == name)
                        {
                            namematch = true;
                        }
                    }

                    foreach (Guest g in guestList)
                    {
                        if (g.PpNumber == ppnumber)
                        {
                            ppmatch = true;
                        }
                    }
                    if(!namematch && !ppmatch) // new guest
                    {
                        // create new guest
                        Guest ng = new Guest(name, ppnumber, new Stay(), new Membership(), false);
                        guest = ng;

                        /* UI Visibility */
                        frontPage.Hide();
                        chkRmAvailPage.Show();
                        statusMsg.Hide();
                    }
                    else
                    {
                        statusBlk.Text = "Error: Name or Passport number is incorrect / does not match any user";
                        statusMsg.Show();
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
            else if (checkInDateTxt.Date >= checkOutDateTxt.Date)
            {
                // error: checkoutdate earlier or equal to than checkindate
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

            /* Auto membership for new guests */
            if (!guestexist)
            {
                guest.Membership.Status = "Ordinary";
                guest.Membership.Points = 0;
            }

            guest.HotelStay.RoomList.Sort(); // sort roomList by room number

            double totalno = Convert.ToDouble(adultnoTxt.Text) + Convert.ToDouble(childrennoTxt.Text) / 2;
            int totalcap = 0;
            foreach (HotelRoom r in guest.HotelStay.RoomList)
            {
                totalcap += r.NoOfOccupants;
            }
            if (totalno > totalcap)
            {
                // error: not enough rooms to fit everybody
            }
            else
            {
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
            foreach (HotelRoom r in guest.HotelStay.RoomList.ToList())
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
            // unfreeze textboxes
            guestTxt.IsReadOnly = false;
            ppTxt.IsReadOnly = false;
            pointsTxt.Text = ""; // reset points textbox

            currentRmPage.Hide();
            frontPage.Show();
        }

        private void ChkoutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!guest.IsCheckedIn)
            {
                statusBlk.Text = "Error: Guest is not checked in.";
                statusMsg.Show();
            }
            else
            {
                int oldpoints = guest.Membership.Points;
                string oldstatus = guest.Membership.Status;
                guest.Membership.EarnPoints(guest.HotelStay.CalculateTotal() - redeempoints); // add points to guest           
                guest.Membership.RedeemPoints(redeempoints); // redeem points from guest
                redeempoints = 0; // reset redeempoints
                int newpoints = guest.Membership.Points;
                string newstatus = guest.Membership.Status;

                /* Add all selected rooms back to available room list */
                foreach (HotelRoom r in guest.HotelStay.RoomList.ToList())
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
                guest.IsCheckedIn = false; // guest not checked in

                // unfreeze textboxes                          
                guestTxt.IsReadOnly = false;
                ppTxt.IsReadOnly = false;

                /* Reset all fields to blank */
                guestTxt.Text = "";
                ppTxt.Text = "";
                pointsTxt.Text = "";

                currentRmPage.Hide();
                frontPage.Show();

                // display message
                statusBlk.Text = $"Check-Out successful!\n";
                if (oldstatus != newstatus)
                {
                    statusBlk.Text += $"New Member status: {newstatus}";
                }
                else
                {
                    statusBlk.Text += $"Member status: {newstatus}";
                }
                if (newpoints > oldpoints)
                {
                    statusBlk.Text += $"\nPoints earned: {newpoints - oldpoints}";
                }
                else if (oldpoints > newpoints)
                {
                    statusBlk.Text += $"\nPoints deducted: {oldpoints - newpoints}!";
                }
                else
                {
                    statusBlk.Text += "Points have not changed";
                }
                statusBlk.Text += $"\nThank you for your stay, {guest.Name}!";
                statusMsg.Show();
            }

            //await Task.Delay(5000);

        }

        private void RedeemBtn_Click(object sender, RoutedEventArgs e)
        {
            statusMsg.Show();
            PrintInvoice();
            // add deducted amount to invoice
            if (pointsTxt.Text == "")
            {
                // error: points field blank
            }
            else if (Convert.ToInt32(pointsTxt.Text) > guest.Membership.Points)
            {
                // error: points more than membership
            }
            else
            {
                redeempoints = Convert.ToInt32(pointsTxt.Text);
                if (guest.Membership.Status == "Silver")
                {
                    bool hasSr = false;
                    // check for standard rooms
                    foreach (HotelRoom r in guest.HotelStay.RoomList)
                    {
                        if (r is StandardRoom sr)
                        {
                            hasSr = true;
                            break;
                        }
                    }
                    if (hasSr)
                    {
                        invoiceDetailBlk.Text += $"\nDiscount (Converted points): ${redeempoints}\nTotal Payable: ${guest.HotelStay.CalculateTotal() - redeempoints}";
                    }
                    else
                    {
                        statusBlk.Text = "Silver members can only offset their bills for standard rooms";
                    }
                }
                else
                {
                    invoiceDetailBlk.Text += $"\nDiscount (Converted points): ${redeempoints}\nTotal Payable: ${guest.HotelStay.CalculateTotal() - redeempoints}";
                }
            }
        }

        private void ExtendBtn_Click(object sender, RoutedEventArgs e)
        {
            guest.HotelStay.CheckOutDate = guest.HotelStay.CheckOutDate.AddDays(1);

            // print invoice
            PrintInvoice();
        }

        private void HideBtn_Click(object sender, RoutedEventArgs e)
        {
            statusBlk.Text = "";
            statusMsg.Hide();
        }
    }
}
