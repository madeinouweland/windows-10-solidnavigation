using System.Collections.Generic;
using System.Linq;
using SolidNavigation.Entities;
using SolidNavigation.Navigation;

namespace SolidNavigation.Lists {
    public class ListsPageViewModel : ObservableObject {
        private ListViewModel _selectedList;
        public List<ListViewModel> Lists { get; set; }

        public ListsPageViewModel() {
            Lists = (from list in Workspace.Current.Lists
                     select new ListViewModel { Id = list.Id, Title = list.Title }).ToList();
        }

        public object SelectedList {
            get { return _selectedList; }
            set {
                var newValue = (ListViewModel)value;
                if (newValue != _selectedList) {
                    _selectedList = (ListViewModel)value;
                    NotifyOfPropertyChange(() => SelectedList);
                    NavigateService.Current.Navigate(new TaskListTarget(_selectedList.Id));
                }
            }
        }
    }
}
