using System.Diagnostics;

namespace SolidNavigation.Lists {
    [DebuggerDisplay("Id: {Id}")]
    public class ListViewModel {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
