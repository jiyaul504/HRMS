using Employee.Data;
using Employee.Interfaces;
using Employee.Models;
using Employee.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Employee.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserViewModel?> GetUserByLoginCredentials(Login input)
        {
            if (await dbContext.Contacts.AnyAsync(user => user.EmailAddress == input.Email))
            {
                var attemptedUser = await dbContext.Contacts.FirstAsync(user => user.EmailAddress == input.Email);
                var attemptedUserPasswordSalt = attemptedUser.PasswordSalt;
                var attemptedPasswordWithSalt = input.Password + attemptedUserPasswordSalt;

                if (HashPassword(attemptedPasswordWithSalt) != attemptedUser.PasswordHash)
                {
                    return null;
                }

                var userViewModel = new UserViewModel()
                {
                    Id = attemptedUser.ContactId,
                    EmailAddress = attemptedUser.EmailAddress,
                    FirstName = attemptedUser.FirstName,
                    LastName = attemptedUser.LastName,
                    Phone = attemptedUser.Phone
                };

                return userViewModel;
            }

            return null;
        }

        public async Task<UserProfileViewModel?> GetUserPersonalDetails(string email)
        {
            if (await dbContext.Contacts.AnyAsync(user => user.EmailAddress == email))
            {
                var currentUser = await dbContext.Contacts.FirstAsync(user => user.EmailAddress == email);
                var userViewModel = new UserProfileViewModel()
                {
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Phone = currentUser.Phone,
                    EmailAddress = currentUser.EmailAddress,
                    City = currentUser.City,
                    Zipcode = currentUser.ZipCode,
                    Street = currentUser.Street,
                    State = currentUser.State,
                    Country = currentUser.Country
                };
                return userViewModel;
            }
            return null;
        }

        public async Task<bool> UserExists(string email)
        {
            return await dbContext.Contacts.AnyAsync(c => c.EmailAddress == email);
        }

        public async Task<bool> CreateNewUser(Register input)
        {
            var firstName = input.FirstName;
            var lastName = input.LastName;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }
            if (input.Password.Length < 8)
            {
                return false;
            }

            var passwordSalt = GenerateSalt(8);
            var passwordWithSalt = input.Password + passwordSalt;
            var hashedPassword = HashPassword(passwordWithSalt);

            var userForDb = new Employee.Entities.Contact
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = input.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = passwordSalt
            };

            await dbContext.Contacts.AddAsync(userForDb);
            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }


        public async Task<bool> EditUserDetails(ProfileUser input, string email)
        {
            if (!await UserExists(email))
            {
                return false;
            }

            if (string.IsNullOrEmpty(input.FirstName) || string.IsNullOrEmpty(input.LastName))
            {
                return false;
            }

            var userDetailsFromDb = await dbContext.Contacts.FirstAsync(sci => sci.EmailAddress == email);
            userDetailsFromDb.FirstName = input.FirstName;
            userDetailsFromDb.LastName = input.LastName;
            userDetailsFromDb.Phone = input.Phone;

            dbContext.Update(userDetailsFromDb);

            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> EditUserPassword(PasswordUser input, string email)
        {
            var passwordSalt = GenerateSalt(8);
            var passwordWithSalt = input.Password + passwordSalt;
            var hashedPassword = HashPassword(passwordWithSalt);

            var userDetailsFromDb = await dbContext.Contacts.FirstAsync(sci => sci.EmailAddress == email);
            userDetailsFromDb.PasswordHash = hashedPassword;
            userDetailsFromDb.PasswordSalt = passwordSalt;

            dbContext.Update(userDetailsFromDb);

            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> EditUserAddress(AddressUser input, string email)
        {
            var userAddressFromDb = await dbContext.Contacts.FirstAsync(sci => sci.EmailAddress == email);
            userAddressFromDb.Street = input.Street;
            userAddressFromDb.City = input.City;
            userAddressFromDb.ZipCode = input.Zipcode;
            userAddressFromDb.Country = input.Country;
            userAddressFromDb.State = input.State;

            dbContext.Contacts.Update(userAddressFromDb);

            return await dbContext.SaveChangesAsync() > 0 ? true : false;
        }
        private string HashPassword(string password)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        private string GenerateSalt(int length)
        {
            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789/+";

            Random random = new Random();

            return new string(Enumerable.Repeat(allowedCharacters, length - 1).Select(s => s[random.Next(s.Length)]).ToArray()) + '=';
        }
    }
}