using jobpostingboard_Dela_Cruz.Models;

namespace jobpostingboard_Dela_Cruz.Repositories
{
    public interface IJobRepository
    {
        List<Job> GetAll(string? search = null);
        Job? GetById(int id);
        void Add(Job job);
        void Update(Job job);
        void Delete(int id);
    }
}