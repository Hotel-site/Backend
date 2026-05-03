using eHotelMartinez.DataAccess.Context;

namespace eHotelMartinez.BusinessLogic.Helpers
{
    public class CategoryCheck
    {
        public static bool CategoryExists(int? categoryId)
        {
            using (var db = new CategoryContext())
            {
                return db.Categories.Any(c => c.Id == categoryId && c.IsActive);
            }
        }
    }
}
