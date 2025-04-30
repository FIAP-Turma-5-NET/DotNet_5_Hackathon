using AutoMapper;

namespace FIAP_HealthMed.Application.Mapper
{
    public class MapperConfiguration
    {
        public static IMapper RegisterMapping()
        {
            return new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile<EntityToModel>();
                mc.AddProfile<ModelToEntity>();
            }).CreateMapper();
        }
    }


}
