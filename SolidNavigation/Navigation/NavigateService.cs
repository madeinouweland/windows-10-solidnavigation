using SolidNavigation.Sdk;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolidNavigation.Navigation
{
    public class NavigateService : NavigationServiceBase
    {
        public Frame ContentFrame
        {
            get { return (Frame)Window.Current.Content; }
        }

        public void GoBack()
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        protected override void Navigate(Route route, NavigationTarget target, string uri)
        {
            ContentFrame.Navigate(route.PageType, uri);
        }
    }
}
