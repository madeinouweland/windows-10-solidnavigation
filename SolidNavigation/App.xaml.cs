using SolidNavigation.Main;
using SolidNavigation.Navigation;
using SolidNavigation.Sdk;
using SolidNavigator.Main;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolidNavigation
{
    sealed partial class App : Application {
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;

        public App() {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Router.Current.Scheme = "solidnavigation://";
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(MainPage), typeof(CommentTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(MainPage), typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(MainPage), typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(MainPage), typeof(HomeTarget));
        }

        private void InitFrame() {
            var frame = Window.Current.Content as Frame;
            if (frame == null) {
                frame = new Frame();
                Window.Current.Content = frame;
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) => {
                if (frame.CanGoBack) {
                    frame.GoBack();
                }
            };
        }

        protected override void OnActivated(IActivatedEventArgs args) {
            base.OnActivated(args);

            InitFrame();

            if (args.Kind == ActivationKind.Protocol) {
                var protocolArgs = (ProtocolActivatedEventArgs)args;
                ServiceLocator.GetNavigateService().Navigate(protocolArgs.Uri + "");
            }

            Window.Current.Activate();
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e) {
            InitFrame();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                await SuspensionManager.RestoreAsync();
            } else {
                ServiceLocator.GetNavigateService().Navigate(e.Arguments);
            }

            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();

            await SuspensionManager.SaveAsync();

            deferral.Complete();
        }
    }
}