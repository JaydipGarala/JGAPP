using JGAPP.Services;

namespace JGAPP.View;

public partial class RegisterView : ContentPage
{
	public RegisterView()
	{
		InitializeComponent();
	}

    private async void registerbtn_ClickedAsync(object sender, EventArgs e)
    {
        if (email.Text != null && password.Text != null && mobileorname.Text != null)
        {

            IAuthService _authService = new AuthService();
            skl.IsVisible = false;
            aiLayout.IsVisible = true;
            ai.IsVisible = true;
            ai.IsRunning = true;
            if (await _authService.RegisterAsync(email.Text, password.Text,mobileorname.Text))
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
                await DisplayAlert("Error", "E-Mail or password is wrong!", "Ok");

            }
        }
        else
        {
            await DisplayAlert("Error", "Please Enter E-Mail and Password and Mobile", "Ok");
        }

    }
}