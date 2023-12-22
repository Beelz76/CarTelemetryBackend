using DatabaseAccessLayer;
using DatabaseAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApi.Services
{
    public class UserService
    {
        private readonly TelemetryDbContext _telemetryDbContext;

        public UserService(TelemetryDbContext telemetryDbContext)
        {
            _telemetryDbContext = telemetryDbContext;
        }

        public Guid Register(Contracts.UserRegisterCredentials credentials)
        {
            var user = new User
            {
                UserUid = Guid.NewGuid(),
                Email = credentials.Email,
                Password = GetHash(credentials.Password),
                IsAdmin = false,
            };

            _telemetryDbContext.Add(user);
            _telemetryDbContext.SaveChanges();

            return user.UserUid;
        }

        public Guid? Login(Contracts.UserLoginCredentials credentials)
        {
            var hashedPassword = GetHash(credentials.Password);

            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.Email == credentials.Email && x.Password == hashedPassword);

            return user?.UserUid;
        }

        public Contracts.UserInfo? GetUserInfo(Guid userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return null; }

            return new Contracts.UserInfo
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        public bool UpdateUser(Guid userUid, Contracts.UserUpdate userUpdate)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; }

            user.Password = GetHash(userUpdate.Password);
            user.Email = userUpdate.Email;

            return _telemetryDbContext.SaveChanges() > 0;
        }

        public bool UpdateUserAdminStatus(Guid userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; };

            user.IsAdmin = true;

            return _telemetryDbContext.SaveChanges() > 0;
        }

        public bool DeleteUser(Guid userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; }

            _telemetryDbContext.Remove(user);

            return _telemetryDbContext.SaveChanges() > 0;
        }

        private string GetHash(string password)
        {
            using var sha = SHA512.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToHexString(bytes);
        }

        public bool CheckEmail(string email)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.Email == email);

            if (user == null) { return false; }

            return true;
        }

        public string? GetEmail(Guid userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return null; }

            return user.Email;
        }

        public bool IsAdmin(Guid userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; }

            if (user.IsAdmin == true)
            {
                return true;
            }

            return false;
        }

        public bool IsUserExists(Guid? userUid)
        {
            var user = _telemetryDbContext.Set<User>().SingleOrDefault(x => x.UserUid == userUid);

            if (user == null) { return false; }

            return true;
        }

        public bool CheckEmailRegex(string email)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w -]+\.)+[\w-]{2,4}$");

            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;
        }
    }
}
