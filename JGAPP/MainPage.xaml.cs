using JGAPP.Services;

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
            if (email.Text != null && password.Text != null)
            {

                IAuthService _authService = new AuthService();
                skl.IsVisible = false;
                aiLayout.IsVisible = true;
                ai.IsVisible = true;
                ai.IsRunning = true;
                if (await _authService.RegisterLoginAsync(email.Text, password.Text))
                {
                    await SecureStorage.SetAsync("email", email.Text);
                    skl.IsVisible = true;
                    aiLayout.IsVisible = false;
                    ai.IsVisible = false;
                    ai.IsRunning = false;

                    await Shell.Current.GoToAsync("///home");
                }
                else
                {
                    skl.IsVisible = true;
                    aiLayout.IsVisible = false;
                    ai.IsVisible = false;
                    ai.IsRunning = false;
                    await DisplayAlert( "Error", "E-Mail or password is wrong!", "Ok");
                   
                }
            }
            else
            {
                await DisplayAlert("Error", "Please Enter E-Mail and Password", "Ok");
            }
        }

      
        private async void registerbtn_Clicked(object sender, EventArgs e)
        {
           
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
