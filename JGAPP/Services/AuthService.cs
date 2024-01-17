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
            Configuration.DataUid = response.UId;
            Configuration.IdToken = response.IDToken;
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
            Configuration.DataUid = response.UId;
            return response;
        }

        public  async Task<bool> RegisterLoginAsync(string email, string password)
        {
            LoginResponse response = new LoginResponse();
            DeviceData d1 = new DeviceData();
            
            bool deviceresult = false;
                response = await RegisterLoginUser(email,password);
            DeviceInformation device = new DeviceInformation(d1);
            deviceresult = await device.CheckDevice();
            try
            {
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
            DeviceData d1 = new DeviceData();
                DeviceInformation device = new DeviceInformation(d1);
            Configuration.DataUid = response.UId;
                bool  deviceresult = await device.SaveDevice(d1);
            
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
