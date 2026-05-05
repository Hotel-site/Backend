using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;


namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IUserActions
    {
        List<UserDTO> GetAllUsersAction();
        UserDTO GetUserByIdAction(int id);
        ResponseAction ResponseUserCreateAction(UserRegDTO user);
        ResponseMsg ResponseUserUpdateAction(UserDTO user);
        ResponseMsg ResponseUserActivateAction(UserActivateDTO user);
        ResponseMsg ResponseUserDeleteAction(int id);
        ResponseMsg ResponseUserLoginAction(UserAuthDTO userAuth);
    }
}