using MediatR;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.Finish
{
    public class FinishAdminRentRequestHandler : IRequestHandler<FinishAdminRentRequest, IActionResult>
    {
        private readonly DataManager _dataManager;

        public FinishAdminRentRequestHandler(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task<IActionResult> Handle(FinishAdminRentRequest request, CancellationToken cancellationToken)
        {
            var rent = await _dataManager.Rents.GetByIdAsync(request.RentId);

            if (rent is null)
                return new BadRequestObjectResult(new { error = ActionMessages.RentNotFound() });

            rent.EndTime = DateTime.UtcNow;
            rent.FinalPrice = rent.CalculateFinalPrice();

            _dataManager.Rents.Update(rent);
            await _dataManager.Rents.SaveChangesAsync();

            return new OkObjectResult(new { messages = ActionMessages.RentUpdated() });
        }
    }
}
