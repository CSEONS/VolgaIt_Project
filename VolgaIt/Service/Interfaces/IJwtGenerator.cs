using VolgaIt.Domain.Entities;

namespace VolgaIt.Service.Interfaces
{
    public interface IJwtGenerator
    {
        string Generate(AppUser user, TimeSpan timeSpan);
    }
}
