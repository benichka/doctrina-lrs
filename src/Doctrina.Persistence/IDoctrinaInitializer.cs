using System.Threading;

namespace Doctrina.Persistence
{
    public interface IDoctrinaInitializer
    {
        void SeedAdminUser(CancellationToken cancellationToken);
        void SeedEverything(CancellationToken cancellationToken = default);
    }
}