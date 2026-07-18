using Mapster;

namespace ECommerce.Application.Common.Mappings;

public interface IMapFrom<T> 
{
    void Mapping(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(T), GetType());
    }
}