namespace ScooterRental.Domain.Interfaces.IoC
{
    public interface ISettings
    {
        string GetAppSetting(string key);
    }
}
