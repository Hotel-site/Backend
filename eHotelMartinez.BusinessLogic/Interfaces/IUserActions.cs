using eHotelMartinez.Domain.Models.User;
using eHotelMartinez.Domain.Models.Base;


namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface IUserActions
    {
        Task<List<UserDTO>> GetAllUsersAction();
        Task<UserDTO?> GetUserByIdAction(int id);
        Task<ResponseAction> ResponseUserCreateAction(UserRegDTO user);
        Task<ResponseMsg> ResponseUserUpdateAction(UserDTO user);
        Task<ResponseMsg> ResponseUserActivateAction(UserActivateDTO user);
        Task<ResponseMsg> ResponseUserDeleteAction(int id);
        Task<ResponseAction> ResponseUserLoginAction(UserAuthDTO userAuth);
    }
}