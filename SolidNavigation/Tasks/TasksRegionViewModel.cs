using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SolidNavigation.Tasks
{
    public class TasksRegionViewModel : ObservableObject
    {
        private TaskViewModel _selectedTask;
        private NavigateService _navigateService;
        private NavigationPath _navigationPath;
        private Workspace _workspace;
        private string listTitle;

        public ObservableCollection<TaskViewModel> Tasks { get; set; } = new ObservableCollection<TaskViewModel>();

        public TasksRegionViewModel(Workspace workspace, NavigateService navigateService, NavigationPath navigationPath)
        {
            _workspace = workspace;
            _navigateService = navigateService;
            _navigationPath = navigationPath;

            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WList>>().Subscribe(x => LoadData(x.NewValue));
            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WTask>>()
                .Where(x => x.NewValue != null)
                .Delay(TimeSpan.FromMilliseconds(200))
                .ObserveOnDispatcher()
                .Subscribe(x => SelectTask(x.NewValue));
        }

        private void SelectTask(WTask task)
        {
            var should_be_selected = Tasks.FirstOrDefault(x => x.Id == _navigationPath.SelectedTask.Id);
            if (should_be_selected != null && _selectedTask != should_be_selected)
            {
                SelectedTask = should_be_selected;
            }
        }

        private void LoadData(WList list)
        {
            ListTitle = _workspace.Lists.FirstOrDefault(x => x.Id == list.Id).Title;

            Tasks.Clear();
            foreach (var task in _workspace.Tasks.Where(x => x.ListId == list.Id))
            {
                Tasks.Add(new TaskViewModel { Id = task.Id, Title = task.Title });
            }
        }

        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (value != _selectedTask)
                {
                    _selectedTask = value;
                    NotifyOfPropertyChange(nameof(SelectedTask));
                    if (value != null && value.Id != _navigationPath.SelectedTask?.Id)
                    {
                        _navigateService.Navigate(new TaskDetailsTarget(_selectedTask.Id));
                    }
                }
            }
        }

        public string ListTitle
        {
            get { return listTitle; }
            set
            {
                if (listTitle != value)
                {
                    listTitle = value;
                    NotifyOfPropertyChange(nameof(ListTitle));
                }
            }
        }

    }
}
