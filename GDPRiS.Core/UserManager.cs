﻿using GDPRiS.Common.Exceptions;
using GDPRiS.Common.Helpers;
using GDPRiS.Data;
using GDPRiS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDPRiS.Common.Enums;
using System.Net.Http;
using System.Net.Http.Headers;

//using System.Net.Http.;

namespace GDPRiS.Core
{
    public class UserManager
    {
        #region Login and Register

        public Users Login(string email, string password)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var users = uow.UserRepository.Find(u => u.Email.ToLower().Trim() == email.ToLower().Trim());

                if (users.Count == 0)
                    throw new ValidationException("Wrong email or password!");

                var user = users.FirstOrDefault();

                if (!user.IsActive)
                    throw new ValidationException("User is not active!");

                if (!PasswordHelper.ValidatePassword(password, user.Password))
                    throw new ValidationException("Wrong email or password!");

                return user;

            }
        }

        public Users Register(Users user)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users existingUser = (uow.UserRepository.Find(u => u.Email == user.Email)).FirstOrDefault();
                if (existingUser != null)
                {
                    throw new ValidationException("Account email is already taken!");
                }

              //  user.DateCreated = DateTime.UtcNow;
                user.Role = Role.Admin;
                user.IsActive = true;

                user.Password = PasswordHelper.CreateHash(user.Password);
                user.DateModified = DateTime.UtcNow;
                user.DateCreated = DateTime.UtcNow;
                uow.UserRepository.Insert(user);
                uow.Save();

                Users newUser = uow.UserRepository.GetById(user.Id);

                return newUser;
            }
        }
    

        public bool CheckEmailExists(string email, int? userId = null)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.Find(u => u.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();

                if (userId.HasValue) return user != null && user.Id != userId.Value;

                return user != null;
            }
        }

        #endregion

        public Users GetByEmail(string email)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.Find(u => u.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
                ValidationHelper.ValidateNotNull(user);

                return user;
            }
        }

        public Users GetByHash(string confirmHash)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.Find(a => a.Password == confirmHash).FirstOrDefault();
                ValidationHelper.ValidateNotNull(user);

                return user;
            }
        }

        //public string GetFromAddressForPasswordResetMail()
        //{
        //    using (UnitOfWork uow = new UnitOfWork())
        //    {
        //        return uow.GlobalSettingsRepository.First(s => s.Key == "FromAddress").Value;
        //    }
        //}

        public Users ForgotPasswordUpdate(int userId, string hashCode)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users userFromDb = uow.UserRepository.GetById(userId);
                ValidationHelper.ValidateNotNull(userFromDb);

                userFromDb.Password = hashCode;

                uow.UserRepository.Update(userFromDb);

                uow.Save();

                return userFromDb;
            }
        }

        public Users NewPasswordUpdate(int userId, string password)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users userFromDb = uow.UserRepository.GetById(userId);
                ValidationHelper.ValidateNotNull(userFromDb);

                if (!string.IsNullOrWhiteSpace(password))
                {
                    userFromDb.Password = PasswordHelper.CreateHash(password);
                    uow.UserRepository.Update(userFromDb);

                    uow.Save();
                }

                return userFromDb;
            }
        }

        public void ChangePassword(int memberId, string oldPassword, string newPassword)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.GetById(memberId);
                ValidationHelper.ValidateNotNull(user);

                if (!PasswordHelper.ValidatePassword(oldPassword, user.Password))
                    throw new ValidationException("Wrong email or password!");

                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    user.Password = PasswordHelper.CreateHash(newPassword);
                    uow.UserRepository.Update(user);
                    uow.Save();
                }
            }
        }

        public List<Users> SearchUser (string firstName)
        { 
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<Users> users = uow.UserRepository.Find(u => !u.DateDeleted.HasValue && (u.FirstName.IndexOf(firstName) != -1 || string.IsNullOrEmpty(firstName)), includeProperties: "Phones");
  
                return users;
            }

        }

     

        public Users GetById(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.GetById(userId);
                ValidationHelper.ValidateNotNull(user);

                return user;
            }
        }

        public void DeleteUserById(int userId)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users user = uow.UserRepository.GetById(userId);
                ValidationHelper.ValidateNotNull(user);

                user.DateDeleted = DateTime.UtcNow;
                uow.UserRepository.Update(user);

                uow.Save();
            }
        }

        public Users UpdateUser(Users modifiedUser)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                Users userFromDb = uow.UserRepository.GetById(modifiedUser.Id);
                ValidationHelper.ValidateNotNull(userFromDb);

                userFromDb.FirstName = modifiedUser.FirstName;
                userFromDb.LastName = modifiedUser.LastName;
                userFromDb.Password = modifiedUser.Password != null ? PasswordHelper.CreateHash(modifiedUser.Password) : userFromDb.Password; 
                userFromDb.Email = modifiedUser.Email;
                userFromDb.Username = modifiedUser.Username;
                userFromDb.DateModified = DateTime.UtcNow;

                uow.UserRepository.Update(userFromDb);
                uow.Save();
                

                return userFromDb;
            }
        }

        public Users UserAddPhones(int userId, List<string> phones)
        {

            using (UnitOfWork uow = new UnitOfWork())
            {
                Users userDb = uow.UserRepository.Find(u => u.Id == userId).FirstOrDefault();
                ValidationHelper.ValidateNotNull(userDb);

                DateTime now = DateTime.UtcNow;

                foreach (string phone in phones)
                {
                    Phones newPhone = new Phones { DateCreated = now, DateModified = now, PhoneNumber = phone, UserId = userId };
                    uow.PhoneRepository.Insert(newPhone);
                }

                uow.Save();

                return userDb;
            }
        }

        public List<Users> listOfUsers()
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                List<Users> allUsers = uow.UserRepository.GetAll();

            return allUsers;
            }
        }


    }
}
