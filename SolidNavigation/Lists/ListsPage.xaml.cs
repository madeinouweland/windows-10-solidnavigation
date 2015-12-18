using Windows.UI.Xaml.Navigation;

namespace SolidNavigation.Lists {
    public sealed partial class ListsPage {
        public ListsPageViewModel ViewModel { get; set; }

        public ListsPage() {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            ViewModel = new ListsPageViewModel();

            NavInfo.Text = "parameter: none";
        }
    }
}
