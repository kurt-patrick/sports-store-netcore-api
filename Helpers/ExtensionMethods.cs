using System.Collections.Generic;
using System.Linq;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<AuthenticatedUserResponse> WithoutPassword(this IEnumerable<User> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static AuthenticatedUserResponse WithoutPassword(this User user) {
            var newUser = new AuthenticatedUserResponse();
            newUser.Id = user.Id;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Email = user.Email;
            newUser.Token = user.Token;
            return newUser;
        }

    }
}