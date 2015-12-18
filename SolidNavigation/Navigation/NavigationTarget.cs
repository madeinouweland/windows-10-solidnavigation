using SolidNavigation.Sdk;

namespace SolidNavigation.Navigation
{
    public class HomeTarget : NavigationTarget
    {
    }

    public class TaskListTarget : NavigationTarget
    {
        public long ListId { get; private set; }

        public TaskListTarget(long listId)
        {
            ListId = listId;
        }

        public override string ToString()
        {
            return base.ToString() + "(ListId=" + ListId + ")";
        }
    }

    public class TaskDetailsTarget : NavigationTarget
    {
        public long TaskId { get; set; }

        public TaskDetailsTarget(long taskId)
        {
            TaskId = taskId;
        }
        public override string ToString()
        {
            return base.ToString() + "(TaskId=" + TaskId + ")";
        }
    }

    public class CommentTarget : NavigationTarget
    {
        public long TaskId { get; set; }
        public long CommentId { get; set; }

        public CommentTarget(long taskId, long commentId)
        {
            TaskId = taskId;
            CommentId = commentId;
        }

        public override string ToString()
        {
            return base.ToString() + "TaskId=" + TaskId + ", CommentId=" + CommentId;
        }
    }
}