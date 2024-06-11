using Employee.Models;
using Employee.ViewModels;

namespace Employee.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel?> GetUserByLoginCredentials(Login input);
        Task<UserProfileViewModel?> GetUserPersonalDetails(string email);
        Task<bool> UserExists(string email);
        Task<bool> CreateNewUser(Register input);

        Task<bool> EditUserDetails(ProfileUser input, string email);

        Task<bool> EditUserPassword(PasswordUser input, string email);

        Task<bool> EditUserAddress(AddressUser input, string email);
        
    }
}
