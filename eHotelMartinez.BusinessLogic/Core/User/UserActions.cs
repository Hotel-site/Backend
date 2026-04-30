using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Entities.User;
using eHotelMartinez.DataAccess.Context;
using System.Security.Cryptography;
using System.Text;

namespace eHotelMartinez.BusinessLogic.Core.User
{
    public class UserActions
    {   
        private string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.Default.GetBytes(password + "likeLinux");
                var encodedBytes = md5.ComputeHash(bytes);

                return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
            }
        }
        private bool PasswordCheck(string password, string hash)
        {
            var TmpHash = HashPassword(password);
            return TmpHash == hash;
        }

        protected List<UserDTO> ExecuteGetAllUsersAction()
        {
            using (var db = new UserContext())
            {
                return db.Users
                .Where(u => u.IsActive)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .ToList();
            }
        }
        protected UserDTO ExecuteGetUserByIdAction(int id)
        {
            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id && u.IsActive);

                if (user == null)
                {
                    return null;
                }
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    IsActive = user.IsActive
                };
            }
        }
        protected ResponseMsg ExecuteUserCreateAction(UserRegDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Username isn't be empty!"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Invalid Email format!"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "The password must be at least 8 characters long!"
                };
            }
            var email = user.Email.ToLower();

            using (var db = new UserContext())
            {
                var existUserByEmail = db.Users.FirstOrDefault(u => u.Email.ToLower() == email);

                if (existUserByEmail != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "User with this Email is already exist!"
                    };
                }
            }

            var User = new UserData
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = HashPassword(user.Password),
                RegisteredOn = DateTime.Now,
                IsActive = true
            };
            using (var db = new UserContext())
            {
                db.Users.Add(User);
                db.SaveChanges();
            }
            return new ResponseMsg
            {
                IsSuccess = true,
                Message = "User created successfully!"
            };
        }
        protected ResponseMsg ExecuteUserUpdateAction(UserDTO user)
        {
            using (var db = new UserContext())
            {
                var existUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The User doesn't exist!"
                    };
                }
                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Username isn't be empty!"
                    };
                }
                if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Invalid Email format!"
                    };
                }
                var email = user.Email.ToLower();

                var existUserByEmail = db.Users.FirstOrDefault(u => u.Email.ToLower() == email && u.Id != user.Id);
                if (existUserByEmail != null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "A User with that email is already exist!"
                    };
                }
                existUser.Username = user.Username;
                existUser.Email = user.Email;
                existUser.IsActive = user.IsActive;

                db.SaveChanges();
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User data updated successfully!"
                };
            }
        }
        protected ResponseMsg ExecuteUserActivateAction(UserActivateDTO user)
        {
            using (var db = new UserContext())
            {
                var existUser = db.Users.FirstOrDefault(u => u.Id == user.Id);

                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The User doesn't exist!"
                    };
                }
                existUser.IsActive = user.IsActive;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User activated successfully!"
                };
            }
        }
        protected ResponseMsg ExecuteUserDeleteAction(int id)
        {
            using (var db = new UserContext())
            {
                var existUser = db.Users.FirstOrDefault(u => u.Id == id);
                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The User doesn't exist!"
                    };
                }
                existUser.IsActive = false;
                db.SaveChanges();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User was deactivated!"
                };
            }
        }
        protected ResponseMsg ExecuteUserloginAction(UserAuthDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Please, enter the Email"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
            {
                return new ResponseMsg
                {
                    IsSuccess = false,
                    Message = "Please, enter the password"
                };
            }
            using (var db = new UserContext())
            {
                var email = user.Email.ToLower();
                var existUser = db.Users.FirstOrDefault(u => u.Email.ToLower() == email);
                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, try again!"
                    };
                }
                if (PasswordCheck(user.Password, existUser.PasswordHash) == false)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, try again!"
                    };
                }
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "Login Successfull!"
                };
            }
        }
    }
}