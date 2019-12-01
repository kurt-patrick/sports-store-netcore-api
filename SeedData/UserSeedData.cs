using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.SeedData
{
    public static class UserSeedData
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Dev", LastName = "Dev", Email = "dev@email.com", Password = "password" },
            new User { Id = 2, FirstName = "Test", LastName = "Test", Email = "test@email.com", Password = "password" }
        };

        public static User[] Users => _users.ToArray();

    }
}
