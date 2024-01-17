using JGAPP.View;

namespace JGAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("settings", typeof(SettingsPage));
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            if (await isAuthenticated())
            {
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await Shell.Current.GoToAsync("login");
            }
            base.OnNavigatedTo(args);
        }

        async Task<bool> isAuthenticated()
        {
            await Task.Delay(2000);
            var hasAuth = await SecureStorage.GetAsync("email");
            return !(hasAuth == null);
        }

        protected override bool OnBackButtonPressed()
        {
            AppClose();
            return true;
        }

        private void AppClose()
        {
            Application.Current!.Quit();
        }

    }
}
