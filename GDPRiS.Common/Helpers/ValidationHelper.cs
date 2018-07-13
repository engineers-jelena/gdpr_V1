using System.Net.Mail;
using GDPRiS.Common.Exceptions;
using GDPRiS.Common.Extensions;

namespace GDPRiS.Common.Helpers
{
    public static class ValidationHelper
    {
        public static void ValidateNotNull<T>(T entity) where T : class
        {
            if (entity == null)
            {
                string entityName = typeof(T).Name.ToSentenceCase();

                throw new ValidationException($"{entityName} does not exist!");
            }
        }

        public static void ValidateEntityExist<T>(T entity) where T : class
        {
            if (entity != null)
            {
                string entityName = typeof(T).Name.ToSentenceCase();

                throw new ValidationException($"{entityName} already exists!");
            }
        }
        public static bool IsMail(string email)
        {
            // TODO Refactor this to regex
            try
            {
                new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
            //return Regex.Match(email, @"[A-Z0-9._%+-]+@[A - Z0 - 9._ % +-]+.[A - Z0 - 9_ % +-]", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(200)).Success; //TODO fix regex expression
        }

        public static void ValidateNewMember()
        {
            
        }
    }
}
