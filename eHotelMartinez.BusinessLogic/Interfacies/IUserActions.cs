using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;


namespace eHotelMartinez.BusinessLogic.Interfacies
{
    public interface IUserActions
    {
        List<UserDTO> GetAllUsersAction();
        UserDTO GetUserByIdAction(int id);
        ResponseMsg ResponceUserCreateAction(UserRegDTO user);
        ResponseMsg ResponceUserUpdateAction(UserDTO user);
        ResponseMsg ResponceUserDeleteAction(int id);
        ResponseMsg ResponceUserLoginAction(UserAuthDTO userAuth);
    }
}
