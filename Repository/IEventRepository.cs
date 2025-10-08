namespace EventManagement.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Models.Event>> GetAllWithAttendeesAsync();
        Task<Models.Event?> GetByIdWithAttendeesAsync(int id);
    }
}
