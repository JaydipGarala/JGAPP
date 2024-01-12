
namespace JGAPP
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void loginbtn_ClickedAsync(object sender, EventArgs e)
        {
            if (IsCredentialCorrect(email.Text, password.Text))
            {
                await SecureStorage.SetAsync("hasAuth", "true");
                await Shell.Current.GoToAsync("///home");
            }
            else
            {
                await DisplayAlert("Login failed", "Uusername or password if invalid", "Try again");
            }
        }

        private bool IsCredentialCorrect(object text1, object text2)
        {
            return true;
        }

        private async void registerbtn_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
            {
                SecureStorage.RemoveAll();
                await Shell.Current.GoToAsync("///login");
            }
            skl.IsVisible = false;
            aiLayout.IsVisible = true;
            ai.IsVisible = true;
            ai.IsRunning = true;
            await Navigation.PushAsync(new View.RegisterView());
        }



        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //    count++;

        //    if (count == 1)
        //        CounterBtn.Text = $"Clicked {count} time";
        //    else
        //        CounterBtn.Text = $"Clicked {count} times";

        //    SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }

}
