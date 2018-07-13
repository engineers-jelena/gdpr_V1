using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using GDPRiS.Api.Helpers;
using System.Web.Http;
using GDPRiS.Data.Model;
using System.Net.Mail;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http.Results;
using AutoMapper;
using GDPRiS.Api.Models.User;
using GDPRiS.Common.Enums;
using GDPRiS.Common.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace GDPRiS.Api.Controllers
{
    [TokenAuthorize]
    public class UserController : BaseController
    {
        /// <summary>
        /// Logins user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        public object Login(UserLoginModel model)
        {
            User user = UserManager.Login(model.Email, model.Password);
            UserModel userModel = Mapper.Map<UserModel>(user);
            if (user.Role == Role.Admin)
            {
                userModel.IsAdmin = true;
            }
            else
            {
                userModel.IsAdmin = false;
            }

            return new { User = userModel, Token = CreateLoginToken(user) };
        }

        #region Helena

        #endregion

        /// <summary>
        /// Temperery method/during development.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        public object Register(UserRegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
                throw new Common.Exceptions.ValidationException("Password confirmation not valid!");
          
            User user = new Data.Model.User { Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Password = model.ConfirmPassword, Username = model.Username };
            User registeredUser = UserManager.Register(user);

            UserModel userModel = new UserModel { Email = registeredUser.Email, FirstName = registeredUser.FirstName, Id = registeredUser.Id, IsAdmin = true, LastName =  registeredUser.LastName, RegistrationDate = registeredUser.DateCreated };
            return new { User = userModel, Token = CreateLoginToken(user) };
        }

        [NonAction]
        private string CreateLoginToken(User user)
        {
            UserJwtModel userModel = Mapper.Map<UserJwtModel>(user);
            userModel.ExpirationDate = DateTime.UtcNow.AddDays(1);

            string secretKey = "Helena123431286SecretCode";
            string token = JWT.JsonWebToken.Encode(userModel, secretKey, JWT.JwtHashAlgorithm.HS256);
            return token;
        }


        /// <summary>
        /// Method that checks if email is already registrated for existing user.
        /// </summary>
        /// <param name="email">Email we need to check.</param>
        /// <param name="userId">Optional User Id parametar that will be excluded from check. Used when editing existing account.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public object CheckEmail(string email, int userId = 0)
        {
            if (string.IsNullOrWhiteSpace(email)) return new { success = true };
            var emailExists = UserManager.CheckEmailExists(email);
            return new { success = emailExists };
        }

        /// <summary>
        /// Method that sends reset password email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public bool ForgotPassword(string email)
        {
            User user = UserManager.GetByEmail(email);
            Guid confirmHash = Guid.NewGuid();
            user = UserManager.ForgotPasswordUpdate(user.Id, confirmHash.ToString());

            string fromAddress = "noreply@enginee.rs";//UserManager.GetFromAddressForPasswordResetMail();

            SmtpClient smtpClient = new SmtpClient();

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromAddress);
            mail.Subject = "NightKey - Reset password";

            string mailBody = "<h3>Forgot your password, " + user.FirstName +
                              "?</h3><hr/><p>Click [HERE] to reset your password.You will be redirected to the site to set a new password for your account.</p><p> You will be redirected to login to the app.</p> ";
            string link = "<a href=\"" + Request.GetRequestContext().Url.Request.RequestUri.Scheme + "://" +
                          Request.GetRequestContext().Url.Request.RequestUri.Authority +
                          "/api/user/resetpasswordconfirmation?hash=" + confirmHash.ToString() + "\">here</a>";
            mail.Body = mailBody.Replace("[HERE]", link);
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.To.Add(email);

            smtpClient.Send(mail);

            return true;
        }

        /// <summary>
        /// Method that is called from reset password email link.
        /// </summary>
        /// <param name="hash">Password reset code.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public RedirectResult ResetPasswordConfirmation(string hash)
        {
            User user = UserManager.GetByHash(hash);
            return Redirect($"{ConfigurationManager.AppSettings["SiteUrl"]}/ConfirmPassword/{hash}");
        }

        /// <summary>
        /// Method that is called to reset password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPut]
        public bool SetNewPassword(UserResetPasswordModel model)
        {
            User user = UserManager.GetByHash(model.ConfirmHash);
            UserManager.NewPasswordUpdate(user.Id, model.NewPassword);

            return true;
        }

        [AllowAnonymous]
        [HttpGet]
        public List<UserSearchModel> UserSearch(string firstName)
        {
            List<User> foundUsers =  UserManager.SearchUser(firstName);

           return foundUsers.Select(u => new UserSearchModel { Email = u.Email, FullName = u.FirstName + " " + u.LastName, Username = u.Username , Phones = u.Phones.Select(p => p.PhoneNumber).ToList()}).ToList();
        }

        [AllowAnonymous]
        [HttpPost]
        public UserSearchModel UserAddPhones(UsersPhoneModel model)
        {
            User userDb = UserManager.UserAddPhones(model.UserId, model.PhoneNumbers);
            UserSearchModel viewModel = Mapper.Map<UserSearchModel>(userDb);
            viewModel.Phones = userDb.Phones.Select(u => u.PhoneNumber).ToList();

            return viewModel;
        }

        [AllowAnonymous]
        [HttpGet]
        public bool DeleteUser(int userId)
        {
             UserManager.DeleteUserById(userId);
            return true;
        }

        [AllowAnonymous]
        [HttpPost]

        public User UpdateUser(UserModel model)
        {
            User editedUser = UserManager.UpdateUser(Mapper.Map<User>(model));
            return editedUser;
        }

        //[AllowAnonymous]
        //[HttpGet]

        //public List<User> showAllUsers()
        //{
        //    List<User> allUsers = UserManager.listOfUsers();
        //    return allUsers;
        // }
       
    }
}