using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.MediatR.TransportCommands.Delete;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminTransportCommands.Delete
{
    public class DeleteTransportRequestHadler : IRequestHandler<DeleteAdminTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;

        public DeleteTransportRequestHadler(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Handle(DeleteAdminTransportRequest request, CancellationToken cancellationToken)
        {
            await _dataManager.Transports.DeleteAsync(request.TransportId);

            return new OkObjectResult(new { mesage = ActionMessages.TransportDeleted() });
        }
    }
}
