using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolidNavigation.Details;
using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using SolidNavigation.Sdk;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolidNavigation.Tasks {
    public class TasksPageViewModel : ObservableObject {
        public List<TaskViewModel> Tasks { get; set; }
        private TaskViewModel _selectedTask;
        private ListModel _list;

        public string ListTitle { get { return _list.Title; } }


        public TasksPageViewModel(long listId) {
            Tasks = (from task in Workspace.Current.Tasks.Where(x => x.ListId == listId)
                     select new TaskViewModel { Id = task.Id, Title = task.Title }).ToList();

            _list = Workspace.Current.Lists.FirstOrDefault(x => x.Id == listId);
        }

        public async Task PinToStart() {
            var logo = new Uri("ms-appx:///Assets/Logo.png");

            var target = new TaskListTarget(_list.Id);
            var url = Router.Current.CreateUrl(target);

            var secondaryTile = new SecondaryTile(Guid.NewGuid().ToString(), _list.Title, url, logo, TileSize.Square150x150);
            secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
            await secondaryTile.RequestCreateAsync();
        }

        public object SelectedTask {
            get { return _selectedTask; }
            set {
                var newValue = (TaskViewModel)value;
                if (newValue != _selectedTask) {
                    _selectedTask = (TaskViewModel)value;
                    NotifyOfPropertyChange(() => SelectedTask);
                    NavigateService.Current.Navigate(new TaskDetailsTarget(_selectedTask.Id));
                }
            }
        }
    }
}
