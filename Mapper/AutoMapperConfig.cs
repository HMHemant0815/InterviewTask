using AutoMapper;
using PracticalTask1;

public class AutoMapperConfig
{
    public static IMapper Initialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Mappers>(); 
        });

        return config.CreateMapper();
    }
}
