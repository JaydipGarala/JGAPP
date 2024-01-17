using Firebase.Auth;
using Firebase.Auth.Providers;
using JGAPP.Model;

namespace JGAPP.Services
{
    public class AuthService : IAuthService
    {


        private async Task<LoginResponse> RegisterLoginUser(string email, string password)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = Configuration.FAPI,
                AuthDomain = Configuration.FDomain,
                Providers = new FirebaseAuthProvider[]
             {
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
                  },
                    };

            var client = new FirebaseAuthClient(config);
            UserCredential user = await client.SignInWithEmailAndPasswordAsync(email,password);
            LoginResponse response = new LoginResponse { 
             Email=email, IDToken=user.User.Credential.IdToken, RefreshToken=user.User.Credential.RefreshToken,
              UId=user.User.Uid
            };
            return response;
        }

        private async Task<LoginResponse> RegisterUser(string email, string password, string name)
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = Configuration.FAPI,
                AuthDomain = Configuration.FDomain,
                Providers = new FirebaseAuthProvider[]
             {
                new GoogleProvider().AddScopes("email"),
                new EmailProvider()
                  },
            };

            var client = new FirebaseAuthClient(config);
            UserCredential user1 = await client.CreateUserWithEmailAndPasswordAsync(email, password,name);
            UserCredential user = await client.SignInWithEmailAndPasswordAsync(email, password);
            LoginResponse response = new LoginResponse
            {
                Email = email,
                IDToken = user.User.Credential.IdToken,
                RefreshToken = user.User.Credential.RefreshToken,
                UId = user.User.Uid
            };
            return response;
        }

        public  async Task<bool> RegisterLoginAsync(string email, string password)
        {
            LoginResponse response = new LoginResponse();
            DeviceInformation device = new DeviceInformation(response.RefreshToken);
            bool deviceresult = false;
            var taskmethodrun = new Task[]
            {
                Task.Run(async ()=> response = await RegisterLoginUser(email,password) ),
                Task.Run(async()=>  deviceresult = await device.CheckDevice())
            };
            try
            {
                await Task.WhenAll(taskmethodrun);
                if (response.RefreshToken != null && deviceresult==true )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ApplicationClose()
        {
            Application.Current?.Quit();
        }

        public async Task<bool> RegisterAsync(string email, string password, string name)
        {
            LoginResponse response = new LoginResponse();
                  response = await RegisterUser(email, password, name);
                DeviceInformation device = new DeviceInformation(response.IDToken);
            Configuration.DataUid = response.UId;
                bool  deviceresult = await device.SaveDevice(device);
            
            try
            {

                if (response.RefreshToken != null && deviceresult == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //ApplicationClose();
            }
            return false;
        }
    }
}
