using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.Delete
{
    public class DeleteAdminRentRequestHandler : IRequestHandler<DeleteAdminRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;

        public DeleteAdminRentRequestHandler(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Handle(DeleteAdminRentRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request.RentId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            _dataManager.Rents.Delte(rent);
            return new BadRequestObjectResult(new { message = ActionMessages.RentDeleted() });
        }
    }
}
