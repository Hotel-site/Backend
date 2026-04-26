using System;

namespace eHotelMartinez.BusinessLogic.Interfaces
{
    public interface ISessionAction
    {
        string CreateOrUpdateSession(int userId);
        int? GetUserIdFromSession(string sessionKey);
        void DeleteSession(string sessionKey);
    }
}