using eHotelMartinez.BusinessLogic.Core.User;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace eHotelMartinez.BusinessLogic.Functions.Users
{
    public class UserFlow : UserActions, IUserActions
    {
        public async Task<List<UserDTO>> GetAllUsersAction()
        {
            return await ExecuteGetAllUsersAction();
        }
        public async Task<UserDTO?> GetUserByIdAction(int id)
        {
            return await ExecuteGetUserByIdAction(id);
        }
        public async Task<ResponseAction> ResponseUserCreateAction(UserRegDTO user)
        {
            return await ExecuteUserCreateAction(user);
        }
        public async Task<ResponseMsg> ResponseUserUpdateAction(UserDTO user)
        {
            return await ExecuteUserUpdateAction(user);
        }
        public async Task<ResponseMsg> ResponseUserUpdatePasswordAction(UserChangePasswordDTO user)
        {
            return await ExecuteUserUpdatePasswordAction(user);
        }
        public async Task<ResponseMsg> ResponseUserActivateAction(UserActivateDTO user)
        {
            return await ExecuteUserActivateAction(user);
        }
        public async Task<ResponseMsg> ResponseUserDeleteAction(int id)
        {
            return await ExecuteUserDeleteAction(id);
        }
        public async Task<ResponseAction> ResponseUserLoginAction(UserAuthDTO user)
        {
            return await ExecuteUserLoginAction(user);
        }
    }
}
