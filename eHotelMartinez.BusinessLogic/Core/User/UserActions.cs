using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.User;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.BusinessLogic.Structure;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

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

        protected async Task<List<UserDTO>> ExecuteGetAllUsersAction()
        {
            await using (var db = new UserContext())
            {
                return await db.Users
                .Where(u => u.IsActive)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    IsActive = u.IsActive
                })
                .ToListAsync();
            }
        }
        protected async Task<UserDTO?> ExecuteGetUserByIdAction(int id)
        {
            await using (var db = new UserContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

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
        protected async Task<ResponseAction> ExecuteUserCreateAction(UserRegDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Username can't be empty!"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Invalid Email format!"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "The password must be at least 8 characters long!"
                };
            }
            var email = user.Email.ToLower();

            await using (var db = new UserContext())
            {
                var existUserByEmail = await db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);

                if (existUserByEmail != null)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "User with this Email is already exist!",
                        Id = existUserByEmail.Id
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
            await using (var db = new UserContext())
            {
                db.Users.Add(User);
                await db.SaveChangesAsync();
            }
            return new ResponseAction
            {
                IsSuccess = true,
                Message = "User created successfully!",
                Id = User.Id
            };
        }
        protected async Task<ResponseMsg> ExecuteUserUpdateAction(UserDTO user)
        {
            await using (var db = new UserContext())
            {
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

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
                        Message = "Username can't be empty!"
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

                var existUserByEmail = await db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email && u.Id != user.Id);
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

                await db.SaveChangesAsync();
                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User data updated successfully!"
                };
            }
        }
        protected async Task<ResponseMsg> ExecuteUserActivateAction(UserActivateDTO user)
        {
            await using (var db = new UserContext())
            {
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The User doesn't exist!"
                    };
                }
                existUser.IsActive = user.IsActive;
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User activated successfully!"
                };
            }
        }
        protected async Task<ResponseMsg> ExecuteUserDeleteAction(int id)
        {
            await using (var db = new UserContext())
            {
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (existUser == null)
                {
                    return new ResponseMsg
                    {
                        IsSuccess = false,
                        Message = "The User doesn't exist!"
                    };
                }
                existUser.IsActive = false;
                await db.SaveChangesAsync();

                return new ResponseMsg
                {
                    IsSuccess = true,
                    Message = "User was deactivated!"
                };
            }
        }
        protected async Task<ResponseAction> ExecuteUserLoginAction(UserAuthDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Please, enter the Email"
                };
            }
            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 8)
            {
                return new ResponseAction
                {
                    IsSuccess = false,
                    Message = "Please, enter the password"
                };
            }
            await using (var db = new UserContext())
            {
                var email = user.Email.ToLower();
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
                if (existUser == null)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, please try again!"
                    };
                }
                if (PasswordCheck(user.Password, existUser.PasswordHash) == false)
                {
                    return new ResponseAction
                    {
                        IsSuccess = false,
                        Message = "Incorrect data, try again!"
                    };
                }

                var token = GenerateUserToken(existUser);

                return new ResponseAction
                {
                    IsSuccess = true,
                    Message = token,
                    Id = existUser.Id
                };
            }
        }
        internal string GenerateUserToken(UserData user)
        {
            var token = new TokenService();
            return token.GenerateToken(user.Id, user.Username, user.Role.ToString());
        }
    }
}