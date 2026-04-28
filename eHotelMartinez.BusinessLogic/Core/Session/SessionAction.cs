using eHotelMartinez.BusinessLogic.Helpers;
using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Entities.Session;

namespace eHotelMartinez.BusinessLogic.Core.Session
{
    public class SessionAction
    {
        protected string ExecuteCreateOrUpdateSession(int userId)
        {
            var sessionKey = CookieGenerator.GenerateSessionKey();
            var expiresAt = DateTime.Now.AddHours(1);

            using (var context = new SessionContext())
            {
                var currentSession = context.Sessions.FirstOrDefault(s => s.UserId == userId);

                if (currentSession != null)
                {
                    currentSession.SessionKey = sessionKey;
                    currentSession.ExpiresAt = expiresAt;
                    context.SaveChanges();
                }
                else
                {
                    context.Sessions.Add(new SessionData
                    {
                        UserId = userId,
                        SessionKey = sessionKey,
                        ExpiresAt = expiresAt,
                        CreatedAt = DateTime.Now
                    });
                    context.SaveChanges();
                }
            }

            return sessionKey;
        }

        protected int? ExecuteGetUserIdFromSession(string sessionKey)
        {

            if (string.IsNullOrEmpty(sessionKey))
                return null;

            using (var context = new SessionContext())
            {
                var session = context.Sessions.FirstOrDefault(s => s.SessionKey == sessionKey && s.ExpiresAt > DateTime.Now);
                if (session == null)
                    return null;

                return session.UserId;
            }
        }

        protected void ExecuteDeleteSession(string sessionKey)
        {

            if (string.IsNullOrEmpty(sessionKey))
                return;

            using (var context = new SessionContext())
            {
                var session = context.Sessions.FirstOrDefault(s => s.SessionKey == sessionKey);
                if (session != null)
                {
                    context.Sessions.Remove(session);
                    context.SaveChanges();
                }
            }
        }
    }
}
