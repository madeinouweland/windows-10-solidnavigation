using System.Diagnostics;

namespace SolidNavigation.Details
{
    [DebuggerDisplay("Id: {Id}")]
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
