using Mapster;

namespace ECommerce.Application.Common.Mappings;

public interface IMapFrom<T> : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        config.NewConfig(typeof(T), GetType());
    }
}