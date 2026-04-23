using System;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ISessionAction
    {
        string CreateSession(int userId);
        int? GetUserIdFromSession(string sessionKey);
        void DeleteSession(string sessionKey);
    }
}
