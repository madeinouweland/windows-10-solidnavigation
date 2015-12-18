using System.Diagnostics;

namespace SolidNavigation.Tasks {
    [DebuggerDisplay("Id: {Id}")]
    public class TaskViewModel {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
