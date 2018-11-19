using RestWithASPNET.Models;
using System.Collections.Generic;

namespace RestWithASPNET.Repository
{
    public interface ILoginRepository
    {
        User FindByLogin(string login);
    }
}
