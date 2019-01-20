using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

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
