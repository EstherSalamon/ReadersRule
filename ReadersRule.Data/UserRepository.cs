using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReadersRule.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            //check if email exists again? or not necessary?

            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            LibraryDataContext context = new LibraryDataContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();

        }

        public bool IfEmailExists(string email)
        {
            LibraryDataContext context = new LibraryDataContext(_connectionString);
            return context.Users.Any(u => u.Email == email);
        }

        public User VerifyUser(string email, string password)
        {
            LibraryDataContext context = new LibraryDataContext(_connectionString);
            User user = context.Users.FirstOrDefault(u => u.Email == email);

            if(user == null || !BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                return null;
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            LibraryDataContext context = new LibraryDataContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
