using eHotelMartinez.BusinessLogic.Core.User;
<<<<<<< Updated upstream
using eHotelMartinez.BusinessLogic.Interfacies;
=======
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;
>>>>>>> Stashed changes
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
        public List<UserDTO> GetAllUsersAction()
        {
            return ExecuteGetAllUsersAction();
        }
        public UserDTO GetUserByIdAction(int id)
        {
            return ExecuteGetUserByIdAction(id);
        }
        public ResponseMsg ResponseUserCreateAction(UserRegDTO user)
        {
            return ExecuteUserCreateAction(user);
        }
        public ResponseMsg ResponseUserUpdateAction(UserDTO user)
        {
            return ExecuteUserUpdateAction(user);
        } 
        public ResponseMsg ResponseUserDeleteAction(int id)
        {
            return ExecuteUserDeleteAction(id);
        }
        public ResponseMsg ResponseUserLoginAction(UserAuthDTO user)
        {
            return ExecuteUserloginAction(user);
        }
    }
}
