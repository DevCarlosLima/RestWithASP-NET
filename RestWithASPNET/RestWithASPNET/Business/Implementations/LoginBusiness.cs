using RestWithASPNET.Data.Converters;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Models;
using RestWithASPNET.Repository;
using RestWithASPNET.Security.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace RestWithASPNET.Business.Implementations
{
    public class LoginBusiness : ILoginBusiness
    {
        private ILoginRepository _repository;
        private SignInConfigurations _signInConfigurations;
        private TokenConfiguration _tokenConfigurtions;
        private readonly UserConverter _converter;

        public LoginBusiness(ILoginRepository repository, SignInConfigurations signInConfigurations, TokenConfiguration tokenConfigurtions)
        {
            _repository = repository;
            _signInConfigurations = signInConfigurations;
            _tokenConfigurtions = tokenConfigurtions;
        }

        object ILoginBusiness.FindByLogin(UserVO user)
        {
            bool credentialsIsValid = false;
            
            if(user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }

            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.Login, "Login"),
                            new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                            }
                        );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurtions.Seconds);
                var handler = new JwtSecurityTokenHandler();
                string token = CreateToekn(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToekn(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurtions.Issuer,
                Audience = _tokenConfigurtions.Audience,
                SigningCredentials = _signInConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                authenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
