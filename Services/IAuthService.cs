using RestauranteAPI.DTOs;

namespace RestauranteAPI.Services
{
    public interface IAuthService
    {
        string Login(LoginDtoIn loginDtoIn);
        string Register(UserDtoIn userDtoIn);
    }
}
