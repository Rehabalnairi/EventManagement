using EventManagement.Models;
using System.Threading.Tasks;

namespace EventManagement.Repository
{
    public interface IEventRepository
    {
        Task<IEnumerable<Models.Event>> GetAllWithAttendeesAsync();
        Task<Models.Event?> GetByIdAsync(int id);
        //Task<Event> GetByIdAsync(int id);
        Task<Event> AddAsync(Event newEvent);
        Task<Models.Event?> GetByIdWithAttendeesAsync(int id);
        

    }
}
