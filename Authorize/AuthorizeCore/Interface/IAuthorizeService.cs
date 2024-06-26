using FZFarm.AuthorizeCore.Model;

namespace FZFarm.AuthorizeCore.Interface;

public interface IAuthorizeService
{
    UserInfo? GetUserInfo();
    void ClearUserInfo();
    UserInfo? AuthenticateUser(string domain, string userName, string password);
    UserInfo? AuthenticateUser(string domain, string userName);
}