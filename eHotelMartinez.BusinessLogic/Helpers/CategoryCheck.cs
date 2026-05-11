using eHotelMartinez.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace eHotelMartinez.BusinessLogic.Helpers
{
    public class CategoryCheck
    {
        public static async Task<bool> CategoryExists(int? categoryId)
        {
            using (var db = new CategoryContext())
            {
                return await db.Categories.AnyAsync(c => c.Id == categoryId && c.IsActive);
            }
        }
    }
}
