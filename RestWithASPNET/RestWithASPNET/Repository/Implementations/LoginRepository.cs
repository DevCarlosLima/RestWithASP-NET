using RestWithASPNET.Models;
using RestWithASPNET.Models.Context;
using System.Linq;

namespace RestWithASPNET.Repository.Implementations
{
    public class LoginRepository : ILoginRepository
    {
        private MySQLContext _context;

        public LoginRepository(MySQLContext context)
        {
            _context = context;
        }
        
        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(p => p.Login.Equals(login));
        }
    }
}
