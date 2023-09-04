using AutoMapper;
using ResourceInventory.Model.PurchaseModel;
using ResourceInventory.Service.PurchaseService;

namespace ResourceInventory.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
        }
    }
}
