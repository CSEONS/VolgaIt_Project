using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.MediatR.TransportCommands.Models;

namespace VolgaIt.MediatR.AccountCommands.GetMyself
{
    public class GetMyselfRequestHandler : IRequestHandler<GetMyselfRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataManager _dataManager;
        private readonly IMapper _mapper;

        public GetMyselfRequestHandler(UserManager<AppUser> userManager, DataManager dataManager, IMapper mapper)
        {
            _userManager = userManager;
            _dataManager = dataManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetMyselfRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);
            var transports = _dataManager.Transports.GetAll().Where(t => t.OwnerId == user.Id);

            if (user == null)
                return new UnauthorizedObjectResult(new { });

            var userViewModel = _mapper.Map<AppUser, AppUserViewModel>(user);

            userViewModel.Transports = transports.AsQueryable().ProjectTo<TransportViewModel>(_mapper.ConfigurationProvider).ToList();

            return new OkObjectResult(userViewModel);
        }
    }
}
