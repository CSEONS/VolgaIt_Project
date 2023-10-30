using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.RentCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminRentCommands.GetUserHistory
{
    public class GetAdminUserHistoryRequestHandler : IRequestHandler<GetAdminUserHistoryRequest, IActionResult>
    {
        private readonly DataManager _dataManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetAdminUserHistoryRequestHandler(DataManager dataManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAdminUserHistoryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return new BadRequestObjectResult(new { error = ActionMessages.UserNotFound() });

            var rents = _dataManager.Rents.GetAll().Where(r => r.UserId == user.Id).AsQueryable();

            var rentsViewModel = rents.ProjectTo<RentViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(rentsViewModel);
        }
    }
}
