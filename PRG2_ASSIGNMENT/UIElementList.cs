﻿using System.Collections.Generic;
using Windows.UI.Xaml;

/* This class is used to simplify control of the visibility of UIElements in mainpage.xaml */
namespace PRG2_ASSIGNMENT
{
    class UIElementList
    {
        private List<UIElement> uIElements;

        public List<UIElement> UIElements
        {
            get { return uIElements; }
            set { uIElements = value; }
        }

        /* Methods */
        public void Show()
        {
            foreach (UIElement uI in UIElements)
            {
                uI.Visibility = Visibility.Visible;
            }
        }

        public void Hide()
        {
            foreach(UIElement uI in UIElements)
            {
                uI.Visibility = Visibility.Collapsed;
            }
        }
    }
}
