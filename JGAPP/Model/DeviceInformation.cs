using Firebase.Database;
using Firebase.Database.Query;

namespace JGAPP.Model
{
    public class DeviceData
    {

        private string _plateform = DeviceInfo.Current.Platform.ToString();
        public string PlateForm
        { get { return _plateform; } set { PlateForm = value; } }
        private string _devicetype = DeviceInfo.Current.DeviceType.ToString();
        public string DeviceType { get { return _devicetype; } set { _devicetype = value; } }
        private string _idom = DeviceInfo.Current.Idiom.ToString();
        public string IDom { get { return _idom; } set { _idom = value; } }
        private string _manufacturer = DeviceInfo.Current.Manufacturer.ToString();
        public string Manufacturer { get { return _manufacturer; } set { _manufacturer = value; } }
        private string _model = DeviceInfo.Current.Model.ToString();
        public string Model { get { return _model; } set { _model = value; } }
        private string _name = DeviceInfo.Current.Name.ToString();
        public string Name { get { return _name; } set { _name = value; } }
        private string _buildver = DeviceInfo.Current.Version.Build.ToString();
        public string BuildVer { get { return _buildver; } set { _buildver = value; } }
        private string _majorver = DeviceInfo.Current.Version.Major.ToString();
        public string Majorver { get { return _majorver; } set { _majorver = value; } }
        private string _majorrevver = DeviceInfo.Current.Version.MajorRevision.ToString();
        public string MajorervVer { get { return _majorrevver; } set { _majorrevver = value; } }
        private string _minorver = DeviceInfo.Current.Version.Minor.ToString();
        public string MinorVer { get { return _minorver; } set { _minorver = value; } }
        private string _minorrevver = DeviceInfo.Current.Version.MinorRevision.ToString();
        public string MinorrevVer { get { return _minorrevver; } set { _minorrevver = value; } }
        private string _revisionver = DeviceInfo.Current.Version.Revision.ToString();
        public string RevisionVer { get { return _revisionver; } set { _revisionver = value; } }
        private string _versionstrg = DeviceInfo.Current.VersionString.ToString();
        public string Versionstrg {  get { return _versionstrg; } set { _versionstrg = value; } }
        private bool _IsDefault = false;
        public bool IsDefault
        {
            get
            {
                return _IsDefault;

            }
            set { _IsDefault = value; }
        }
        private DateTime _LoginUpdate =DateTime.Now;
        public DateTime LoginUpdate {  get { return _LoginUpdate; } set {
                    _LoginUpdate = value;
                } }   

    }
    public class DeviceInformation(DeviceData dd)
    {
        public async Task<bool>  CheckDevice()
        {
            var data = await GetDeviceIDAsync();
            var deviceinfo=  data.Where(x => x.Object.IsDefault == true).ToList();
            foreach (var d in deviceinfo)
            {
                if (d.Object.IsDefault == true && d.Object.Manufacturer == dd.Manufacturer && d.Object.IDom == dd.IDom &&
                    d.Object.Model == dd.Model)
                {
                    return true;
                }
                else
                {
                    await SaveDevice(dd);
                    return false; 
                }
            }
            return false;
        }
        private async Task< IReadOnlyCollection<FirebaseObject<DeviceData>>> GetDeviceIDAsync()
        {
            try
            {
                var _Client = new FirebaseClient(
                 Configuration.DataFileUrl,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(Configuration.IdToken)
                 });
                            var devices = await _Client
                  .Child(Configuration.DataFile)
                  .Child(Configuration.DataUid)
                  .OnceAsync<DeviceData>();
                return devices;
            }
            catch (Exception)
            {

                throw;
            }
                         
        }

        public async Task<bool> SaveDevice(DeviceData d1)
        {
            try
            {
                var _Client = new FirebaseClient(
                   Configuration.DataFileUrl,
                   new FirebaseOptions
                   {
                       AuthTokenAsyncFactory = () => Task.FromResult(Configuration.IdToken)
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
