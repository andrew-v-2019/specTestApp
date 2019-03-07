namespace specTestApp.Services
{
    public interface IConfigurationProvider
    {
        T GetConfig<T>(string name, T defaultValue);
        string GetConfig(string name);
    }
}
