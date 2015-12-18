using System.Collections.Generic;

namespace SolidNavigation.Entities {
    public class Workspace {
        public List<WList> Lists { get; set; }
        public List<WTask> Tasks { get; set; }
        public List<WComment> Comments { get; set; }

        public Workspace() {
            Lists = new List<WList>
            {
                new WList {Id = 1, Title = "Inbox"},
                new WList {Id = 2, Title = "Music"},
                new WList {Id = 3, Title = "Restaurants"},
                new WList {Id = 4, Title = "Movies"}
            };

            Tasks = new List<WTask>
            {
                new WTask {Id = 1, ListId = 1, Title = "Inbox task 1"},
                new WTask {Id = 2, ListId = 1, Title = "Inbox task 2"},

                new WTask {Id = 3, ListId = 2, Title = "The Beatles"},
                new WTask {Id = 4, ListId = 2, Title = "Steely Dan"},
                new WTask {Id = 5, ListId = 2, Title = "R.E.M."},
                new WTask {Id = 6, ListId = 2, Title = "Fleetwood Mac"},
                new WTask {Id = 7, ListId = 2, Title = "Dota und die Stadtpiraten"},

                new WTask {Id = 8, ListId = 3, Title = "Chai Village"},
                new WTask {Id = 9, ListId = 3, Title = "The Bird"},
                new WTask {Id = 10, ListId = 3, Title = "Lemongrass"},

                new WTask {Id = 11, ListId = 4, Title = "The godfather"},
                new WTask {Id = 12, ListId = 4, Title = "Back to the future"},
                new WTask {Id = 13, ListId = 4, Title = "Casino"}
            };

            Comments = new List<WComment>();
            foreach (var task in Tasks) {
                for (int i = 0; i < 10; i++) {
                    Comments.Add(new WComment { Id = i * task.Id, TaskId = task.Id, Text = "Comment " + i * task.Id });
                }
            }
        }
    }
}
