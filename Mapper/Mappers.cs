using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace PracticalTask1
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<ViewProductDTO, ProductModel>();
            CreateMap<ProductModel, ViewProductDTO>();
        }
    }
}
