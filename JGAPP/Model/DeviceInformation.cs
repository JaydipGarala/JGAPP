using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;

namespace JGAPP.Model
{
    public class DeviceInformation(string? token)
    {
        private string? _token = token;
        private string _plateform = DeviceInfo.Current.Platform.ToString();
        private string _devicetype=DeviceInfo.Current.DeviceType.ToString();
        private string _idom = DeviceInfo.Current.Idiom.ToString();
        private string _manufacturer = DeviceInfo.Current.Manufacturer.ToString();
        private string _model = DeviceInfo.Current.Model.ToString();
        private string _name = DeviceInfo.Current.Name.ToString();
        private string _buildver = DeviceInfo.Current.Version.Build.ToString();
        private string _majorver = DeviceInfo.Current.Version.Major.ToString();
        private string _majorrevver = DeviceInfo.Current.Version.MajorRevision.ToString();
        private string _minorver = DeviceInfo.Current.Version.Minor.ToString();
        private string _minorrevver = DeviceInfo.Current.Version.MinorRevision.ToString();
        private string _revisionver = DeviceInfo.Current.Version.Revision.ToString();
        private string _versionstrg = DeviceInfo.Current.VersionString.ToString();
        private bool _IsDefault = false;

        public async Task<bool>  CheckDevice()
        {
            var data = await GetDeviceIDAsync();
            var deviceinfo=  data.Where(x => x.Object._IsDefault == true).ToList();
            foreach (var d in deviceinfo)
            {
                if (d.Object._IsDefault == true && d.Object._manufacturer == _manufacturer && d.Object._idom == _idom &&
                    d.Object._model == _model)
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            return false;
        }
        private async Task< IReadOnlyCollection<FirebaseObject<DeviceInformation>>> GetDeviceIDAsync()
        {
                            var _Client = new FirebaseClient(
                    Configuration.DataFileUrl,
                    new FirebaseOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(_token)
                    });
                            var devices = await _Client
                  .Child(Configuration.DataFile)
                  .Child(Configuration.DataUid)
                  .OnceAsync<DeviceInformation>();
            return devices;
        }

        public async Task<bool> SaveDevice(DeviceInformation d1)
        {
            try
            {
                var _Client = new FirebaseClient(
                   "https://educationdata-bbc71-default-rtdb.firebaseio.com/",
                   new FirebaseOptions
                   {
                       AuthTokenAsyncFactory = () => Task.FromResult(d1._token)
                   });
                await _Client
               .Child(Configuration.DataFile)
               .Child(Configuration.DataUid)
               .PostAsync(d1);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
