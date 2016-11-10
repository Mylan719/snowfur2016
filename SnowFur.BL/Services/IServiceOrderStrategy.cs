namespace SnowFur.BL.Services
{
    public interface IServiceOrderStrategy
    {
        bool CanBeOrdered(int userId, int serviceId);
    }
}