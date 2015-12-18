using SolidNavigation.Entities;
using SolidNavigation.Sdk;
using System;
using System.Linq;
using System.Reactive.Subjects;

namespace SolidNavigation.Navigation
{
    public interface ISelectable { }

    public class SelectionChanged<T> : ISelectable
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
        public SelectionChanged(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class NavigationPath
    {
        private Workspace _workspace;
        public NavigationPath(Workspace workspace)
        {
            _workspace = workspace;
        }
        private string _log;

        private Subject<ISelectable> _onSelectionChanged = new Subject<ISelectable>();
        public Subject<ISelectable> OnSelectionChanged => _onSelectionChanged;

        private WList _selectedList;
        public WList SelectedList
        {
            get { return _selectedList; }
            set
            {
                if (_selectedList != value)
                {
                    _onSelectionChanged.OnNext(new SelectionChanged<WList>(_selectedList, value));
                    _selectedList = value;
                }
            }
        }

        private WTask _selectedTask;
        public WTask SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (_selectedTask != value)
                {
                    _onSelectionChanged.OnNext(new SelectionChanged<WTask>(_selectedTask, value));
                    _selectedTask = value;
                }
            }
        }

        private WComment _selectedComment;
        public WComment SelectedComment
        {
            get { return _selectedComment; }
            set
            {
                if (_selectedComment != value)
                {
                    _onSelectionChanged.OnNext(new SelectionChanged<WComment>(_selectedComment, value));
                    _selectedComment = value;
                }
            }
        }

        public void Navigate(NavigationTarget target)
        {
            target
                .IfType<HomeTarget>(x =>
                {
                    var inbox = _workspace.Lists.First(); // simulate inbox
                    SelectedList = inbox;
                    SelectedTask = null;
                    SelectedComment = null;
                })
                .IfType<TaskListTarget>(x =>
                {
                    SelectedList = _workspace.Lists.First(l => l.Id == x.ListId);
                    SelectedTask = null;
                    SelectedComment = null;
                })
                .IfType<TaskDetailsTarget>(x =>
                {
                    var task = _workspace.Tasks.First(t => t.Id == x.TaskId);
                    var list = _workspace.Lists.First(l => l.Id == task.ListId);
                    SelectedList = list;
                    SelectedTask = task;
                    SelectedComment = null;
                })
                .IfType<CommentTarget>(x =>
                 {
                     var task = _workspace.Tasks.First(t => t.Id == x.TaskId);
                     var list = _workspace.Lists.First(l => l.Id == task.ListId);
                     var comment = _workspace.Comments.First(c => c.Id == x.CommentId); 
                     SelectedList = list;
                     SelectedTask = task;
                     SelectedComment = comment;
                 });

            Log(target);
        }

        private void Log(NavigationTarget target)
        {
            _log = String.Format("Target: {0}\nNavPath: {1}/{2}/{3}", target.ToString(), _selectedList?.Id, _selectedTask?.Id, _selectedComment?.Id);
        }

        public override string ToString()
        {
            return _log;
        }
    }
}
