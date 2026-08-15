using jobpostingboard_Dela_Cruz.Models;

namespace jobpostingboard_Dela_Cruz.Data
{
    public static class FakeDbContext
    {
        public static List<User> Users { get; set; } = new List<User>();
    }
}