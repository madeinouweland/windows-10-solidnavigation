using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using SolidNavigator.Main;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation.Main
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            listsregion.ViewModel = ServiceLocator.GetListsRegionViewModel();
            tasksregion.ViewModel = ServiceLocator.GetTasksRegionViewModel();
            detailsregion.ViewModel = ServiceLocator.GetDetailsRegionViewModel();
            detailsregion.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            ServiceLocator.GetNavigationPath().OnSelectionChanged.OfType<SelectionChanged<WTask>>().Subscribe(x => OnTaskSelectionChanged(x.NewValue));
        }

        private void OnTaskSelectionChanged(WTask task)
        {
            if (task == null)
            {
                detailsregion.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                detailsregion.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavInfo.Text = e.Parameter + "\n" + ServiceLocator.GetNavigationPath().ToString();
        }
    }
}
