using VolgaIt.Domain.Repositories.Abstract;

namespace VolgaIt.Domain
{
    public class DataManager
    {
        public readonly ITransportRepository Transports;
        public readonly ITransportTypeRepository TransportTypes;
        public readonly IRentRepository Rents;

        public DataManager(ITransportRepository transportRepository, ITransportTypeRepository transportTypes, IRentRepository rents)
        {
            Transports = transportRepository;
            TransportTypes = transportTypes;
            Rents = rents;
        }
    }
}
