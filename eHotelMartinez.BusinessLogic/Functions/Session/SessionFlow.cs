using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.BusinessLogic.Core.Session;

namespace eHotelMartinez.BusinessLogic.Functions.Session
{
    public class SessionFlow : SessionAction, ISessionAction
    {
        public string CreateSession(int userId)
        {
            return ExecuteCreateOrUpdateSession(userId);
        }
        public int? GetUserIdFromSession(string sessionKey)
        {
            return ExecuteGetUserIdFromSession(sessionKey);
        }
        public void DeleteSession(string sessionKey)
        {
            ExecuteDeleteSession(sessionKey);
        }


    }
}
