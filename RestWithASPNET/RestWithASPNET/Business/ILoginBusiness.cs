using RestWithASPNET.Models;

namespace RestWithASPNET.Business
{
    public interface ILoginBusiness
    {
        object FindByLogin(User login);
    }
}
