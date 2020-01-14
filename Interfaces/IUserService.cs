using System.Collections.Generic;
using SportsStoreApi.Entities;

namespace SportsStoreApi.Interfaces
{
    public interface IUserService
    {
        AuthenticatedUserResponse GetById(int id);
        AuthenticatedUserResponse Authenticate(string email, string password);
        IEnumerable<AuthenticatedUserResponse> GetAll();
    }
}