using SolidNavigation.Entities;
using SolidNavigation.Navigation;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace SolidNavigation.Details
{
    public class DetailsRegionViewModel : ObservableObject
    {
        private CommentViewModel _selectedComment;
        private NavigateService _navigateService;
        private NavigationPath _navigationPath;
        private Workspace _workspace;
        private string _taskTitle;

        public ObservableCollection<CommentViewModel> Comments { get; set; } = new ObservableCollection<CommentViewModel>();

        public DetailsRegionViewModel(Workspace workspace, NavigateService navigateService, NavigationPath navigationPath)
        {
            _navigationPath = navigationPath;
            _workspace = workspace;
            _navigateService = navigateService;

            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WTask>>().Where(x => x.NewValue != null).Subscribe(x => LoadData(x.NewValue));
            navigationPath.OnSelectionChanged.OfType<SelectionChanged<WComment>>()
                .Where(x => x.NewValue != null)
                .Delay(TimeSpan.FromMilliseconds(200))
                .ObserveOnDispatcher()
                .Subscribe(x => SelectComment(x.NewValue));
        }

        private void SelectComment(WComment comment)
        {
            var should_be_selected = Comments.FirstOrDefault(x => x.Id == _navigationPath.SelectedComment.Id);
            if (should_be_selected != null && _selectedComment != should_be_selected)
            {
                SelectedComment = should_be_selected;
            }
        }

        private void LoadData(WTask task)
        {
            TaskTitle = _workspace.Tasks.FirstOrDefault(x => x.Id == task.Id).Title;

            Comments.Clear();
            foreach (var comment in _workspace.Comments.Where(x => x.TaskId == task.Id))
            {
                Comments.Add(new CommentViewModel { Text = comment.Text, Id = comment.Id, });
            }
        }

        public CommentViewModel SelectedComment
        {
            get { return _selectedComment; }
            set
            {
                if (value != _selectedComment)
                {
                    _selectedComment = value;
                    NotifyOfPropertyChange(nameof(SelectedComment));
                    if (value!=null && value.Id != _navigationPath.SelectedComment?.Id)
                    {
                        _navigateService.Navigate(new CommentTarget(_navigationPath.SelectedTask.Id, _selectedComment.Id));
                    }
                }
            }
        }

        public string TaskTitle
        {
            get { return _taskTitle; }
            set
            {
                if (_taskTitle != value)
                {
                    _taskTitle = value;
                    NotifyOfPropertyChange(nameof(TaskTitle));
                }
            }
        }
    }
}
