namespace Services
{
    public interface IService
    {
        Task StartAsync();
        Task StopAsync();
        string Name { get; }
    }
} 