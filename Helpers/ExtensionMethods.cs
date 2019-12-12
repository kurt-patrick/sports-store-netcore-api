using System.Collections.Generic;
using System.Linq;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user) {
            var newUser = new User();
            newUser.Email = user.Email;
            newUser.FirstName = user.FirstName;
            newUser.Id = user.Id;
            newUser.LastName = user.LastName;
            newUser.Token = user.Token;
            return newUser;
        }
    }
}