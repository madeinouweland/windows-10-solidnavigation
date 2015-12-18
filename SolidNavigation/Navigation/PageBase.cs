using SolidNavigation.Sdk;
using SolidNavigator.Main;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation.Navigation {
    public class PageBase : Page {
        public NavigationTarget Target { get; private set; }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            var url = e.Parameter + "";
            Target = Router.Current.CreateTarget(url);

            if (((Frame)Window.Current.Content).CanGoBack) {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            } else {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            ServiceLocator.GetNavigationPath().Navigate(Target);
        }
    }
}
