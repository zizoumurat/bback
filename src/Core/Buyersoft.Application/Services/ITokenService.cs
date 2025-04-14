using Buyersoft.Application.Dtos;

namespace Buyersoft.Application.Services;
public interface ITokenService
{
    int GetUserIdByToken();
    int GetCompanyIdByToken();
    int GetSupplierIdByToken();
    string GetUserNameByToken();
}
