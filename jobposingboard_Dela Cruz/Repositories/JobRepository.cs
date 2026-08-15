using jobpostingboard_Dela_Cruz.Models;

namespace jobpostingboard_Dela_Cruz.Repositories
{
    public class JobRepository : IJobRepository
    {
        private static readonly List<Job> Jobs = new();
        private static int nextId = 1;

        public List<Job> GetAll(string? search = null)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return Jobs.OrderByDescending(x => x.DatePosted).ToList();
            }

            search = search.ToLower();

            return Jobs
                .Where(x =>
                    x.Title.ToLower().Contains(search) ||
                    x.Company.ToLower().Contains(search) ||
                    x.Location.ToLower().Contains(search))
                .OrderByDescending(x => x.DatePosted)
                .ToList();
        }

        public Job? GetById(int id)
        {
            return Jobs.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Job job)
        {
            job.Id = nextId++;
            job.DatePosted = DateTime.Now;
            Jobs.Add(job);
        }

        public void Update(Job job)
        {
            var existing = GetById(job.Id);

            if (existing == null)
                return;

            existing.Title = job.Title;
            existing.Company = job.Company;
            existing.Location = job.Location;
            existing.Description = job.Description;
        }

        public void Delete(int id)
        {
            var job = GetById(id);

            if (job != null)
            {
                Jobs.Remove(job);
            }
        }
    }
}