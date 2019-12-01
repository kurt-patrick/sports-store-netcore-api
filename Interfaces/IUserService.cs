using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IUserService
    {
        User GetById(int id);
        User Authenticate(string email, string password);
        IEnumerable<User> GetAll();
    }
}