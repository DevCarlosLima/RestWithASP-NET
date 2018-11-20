using RestWithASPNET.Models;
using RestWithASPNET.Models.Context;
using RestWithASPNET.Repository.Generic;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNET.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context)
            : base(context) { }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName) &&
                    p.LastName.Contains(lastName)
                ).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(
                    p => p.FirstName.Contains(firstName)
                ).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}
