namespace ScooterRental.Infrastructure.IoC.Setting
{
    using System.Collections.Specialized;
    using ScooterRental.Domain.Interfaces.IoC;
    
    public class Settings : ISettings
    {
        public NameValueCollection AppSettings { get; set; }

        public string GetAppSetting(string key)
        {
            return AppSettings[key];
        }
    }
}
