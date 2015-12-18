using SolidNavigation.Details;
using SolidNavigation.Entities;
using SolidNavigation.Lists;
using SolidNavigation.Navigation;
using SolidNavigation.Tasks;

namespace SolidNavigator.Main
{
    public static class ServiceLocator
    {
        private static ListsRegionViewModel _listsRegionViewModel;
        private static TasksRegionViewModel _tasksRegionViewModel;
        private static DetailsRegionViewModel _detailsRegionViewModel;
        private static NavigateService _navigateService;
        private static NavigationPath _navigationPath;
        private static Workspace _workspace;

        public static ListsRegionViewModel GetListsRegionViewModel()
        {
            if (_listsRegionViewModel == null)
            {
                _listsRegionViewModel = new ListsRegionViewModel(GetWorkspace(), GetNavigateService(), GetNavigationPath());
            }
            return _listsRegionViewModel;
        }

        public static TasksRegionViewModel GetTasksRegionViewModel()
        {
            if (_tasksRegionViewModel == null)
            {
                _tasksRegionViewModel = new TasksRegionViewModel(GetWorkspace(), GetNavigateService(), GetNavigationPath());
            }
            return _tasksRegionViewModel;
        }

        public static DetailsRegionViewModel GetDetailsRegionViewModel()
        {
            if (_detailsRegionViewModel == null)
            {
                _detailsRegionViewModel = new DetailsRegionViewModel(GetWorkspace(), GetNavigateService(), GetNavigationPath());
            }
            return _detailsRegionViewModel;
        }


        public static NavigateService GetNavigateService()
        {
            if (_navigateService == null)
            {
                _navigateService = new NavigateService();
            }
            return _navigateService;
        }

        public static NavigationPath GetNavigationPath()
        {
            if (_navigationPath == null)
            {
                _navigationPath = new NavigationPath(GetWorkspace());
            }
            return _navigationPath;
        }

        public static Workspace GetWorkspace()
        {
            if (_workspace == null)
            {
                _workspace = new Workspace();
            }
            return _workspace;

        }
    }
}
