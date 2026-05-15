using System;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ISessionActions
    {
        string CreateOrUpdateSession(int userId);
        int? GetUserIdFromSession(string sessionKey);
        void DeleteSession(string sessionKey);
    }
}