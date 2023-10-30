using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.Service;

namespace VolgaIt.MediatR.TransportCommands.Delete
{
    public class DeleteTransportRequestHandler : IRequestHandler<DeleteTransportRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;

        public DeleteTransportRequestHandler(DataManager dataManager, UserManager<AppUser> userManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(DeleteTransportRequest request, CancellationToken cancellationToken)
        {
            var from = await _userManager.GetUserAsync(request.From);
            var transport = _dataManager.Transports.GetById(request.TransportId);

            if (string.IsNullOrEmpty(transport?.Id) || string.IsNullOrEmpty(from?.Id))
                return new BadRequestObjectResult(new { error = ActionMessages.TransportNotFound() });

            if (from.Id == transport.OwnerId)
            {
                await _dataManager.Transports.DeleteAsync(request.TransportId);
                await _dataManager.Transports.SaveChangesAsyn();
                return new OkObjectResult(new { message = ActionMessages.TransportDeleted()});
            }

            return new BadRequestObjectResult(new { error = ActionMessages.YouNotOwner() });
        }
    }
}
