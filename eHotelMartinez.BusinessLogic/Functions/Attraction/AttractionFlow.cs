using eHotelMartinez.BusinessLogic.Core.Attraction;
using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Entities.Attraction;
using eHotelMartinez.Domain.Models.Base;
using eHotelMartinez.Domain.Models.Attraction;

namespace eHotelMartinez.BusinessLogic.Functions.Attraction
{
    public class AttractionFlow : AttractionActions, IAttractionActions
    {
        public List<AttractionDTO> GetAllAttractions()
        {
            return ExecuteGetAllAttractions();
        }
        public AttractionDTO GetAttractionById(int id)
        {
            return ExecuteGetAttractionById(id);
        }
        public ResponseAction CreateAttraction(CreateAttractionDTO attraction)
        {
            return ExecuteCreateAttraction(attraction);
        }
        public ResponseMsg UpdateAttraction(UpdateAttractionDTO attraction)
        {
            return ExecuteUpdateAttraction(attraction);
        }
        public ResponseMsg DeleteAttraction(int id)
        {
            return ExecuteDeleteAttraction(id);
        }   

    }
}
