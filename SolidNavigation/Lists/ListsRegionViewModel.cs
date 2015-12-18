using System.Collections.Generic;
using System.Linq;
using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using System;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

namespace SolidNavigation.Lists
{
    public class ListsRegionViewModel : ObservableObject
    {
        private ListViewModel _selectedList;
        private NavigationPath _navigationPath;
        private Workspace _workspace;
        private NavigateService _navigateService;

        public ObservableCollection<ListViewModel> Lists { get; set; }

        public ListsRegionViewModel(Workspace workspace, NavigateService navigateService, NavigationPath navigationPath)
        {
            _workspace = workspace;
            _navigateService = navigateService;
            _navigationPath = navigationPath;

            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WList>>().Subscribe(x => LoadLists());
            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WList>>().
                Delay(TimeSpan.FromMilliseconds(200)).
                ObserveOnDispatcher().
                Subscribe(x => SelectList(x.NewValue));
        }

        private void SelectList(WList list)
        {
            var should_be_selected = Lists.FirstOrDefault(x => x.Id == _navigationPath.SelectedList.Id);
            if (should_be_selected != null && _selectedList != should_be_selected)
            {
                SelectedList = should_be_selected;
            }
        }

        private void LoadLists()
        {
            if (Lists == null)
            {
                Lists = new ObservableCollection<ListViewModel>();
                foreach (var list in _workspace.Lists)
                {
                    Lists.Add(new ListViewModel { Id = list.Id, Title = list.Title });
                }
            }
        }

        public ListViewModel SelectedList
        {
            get { return _selectedList; }
            set
            {
                if (value != _selectedList)
                {
                    _selectedList = value;
                    NotifyOfPropertyChange(nameof(SelectedList));
                    if (value != null && value.Id != _navigationPath.SelectedList?.Id)
                    {
                        _navigateService.Navigate(new TaskListTarget(_selectedList.Id));
                    }
                }
            }
        }
    }
}
