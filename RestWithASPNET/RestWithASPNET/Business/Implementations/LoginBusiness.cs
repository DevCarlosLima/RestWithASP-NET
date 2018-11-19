using RestWithASPNET.Data.Converters;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private ILoginRepository _repository;
        private readonly PersonConverter _converter;

        public LoginBusiness(ILoginRepository repository)
        {
            _repository = repository;
        }

        object ILoginBusiness.FindByLogin(User login)
        {
            throw new System.NotImplementedException();
        }
    }
}
