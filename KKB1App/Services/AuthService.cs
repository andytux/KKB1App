using KKB1App.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace KKB1App.Services
{
    public class AuthService
    {
        /// <summary>
        /// Holt sich aus den ersten 8 zeichen das salz aus der Guid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>string salz</returns>
        private string GetSaltOfGuid(Guid userId)
        {
            return userId.ToString().Substring(0, 8);
        }

        /// <summary>
        /// erstellt ein bytearry zum verhashen
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns>byte[]</returns>
        private byte[] GetHashableBytes(string password, Guid userId)
        {
            var salt = GetSaltOfGuid(userId);
            var combined = password + salt;

            return Encoding.UTF8.GetBytes(combined);
        }

        /// <summary>
        /// Verhascht das Password und gibt diesen zurück
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>string - passworthash</returns>
        public string GetPasswordHash(string password, User user)
        {
            var hashableBytes = GetHashableBytes(password, user.UserId);

            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(hashableBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }


        /// <summary>
        /// Passwortüberprüfung
        /// </summary>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns>true = richtig; false = falsch</returns>
        public bool VerifyPassword(string password, User user)
        {
            var hashOfInput = GetPasswordHash(password, user);

            if (hashOfInput != user.PasswordHash)
            {
                return false;
            }

            return true;
        }
    }
}
