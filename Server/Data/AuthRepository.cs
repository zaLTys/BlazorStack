using BlazorStack.Shared.Entities;
using BlazorStack.Shared.Models;
using Microsoft.EntityFrameworkCore;
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
            var  response = new ServiceResponse<string>();
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(existingUser == null)
            {
                response.Success = false;
                response.Message = "User or email not found";
            }
            else if(!VerifyPasswordHash(password, existingUser.PasswordHash, existingUser.PasswordSalt)){
                response.Success = false;
                response.Message = "Incorrect credentials";
            }
            else
            {
                response.Data = existingUser.Id.ToString();
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "User exists" };
            }

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "User created" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (_context.Users.Any(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
