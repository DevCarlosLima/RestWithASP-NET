using RestWithASPNET.Models;
using System.Collections.Generic;

namespace RestWithASPNET.Repository.Generic
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        List<Person> FindByName(string firstName, string lastName);
    }
}
