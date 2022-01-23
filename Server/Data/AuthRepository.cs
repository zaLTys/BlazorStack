using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;
using System.Security.Cryptography;

namespace BlazorStack.Server.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {

            return new ServiceResponse<string> { Success = false, Message = "Login failed" };
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if(await UserExists(user.Email)){
                return new ServiceResponse<int> { Success = false, Message = "User exists"};
            }

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data= user.Id, Message = "User created" };
        }

        public async Task<bool> UserExists(string email)
        {
            if(_context.Users.Any(x=> x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
