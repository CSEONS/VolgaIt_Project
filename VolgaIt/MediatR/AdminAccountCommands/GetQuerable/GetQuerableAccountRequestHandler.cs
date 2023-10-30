using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain;
using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.AccountCommands.Models;
using VolgaIt.Service;

namespace VolgaIt.MediatR.AdminAccountCommands.GetQuerable
{
    public class GetQuerableAccountRequestHandler : IRequestHandler<GetQuerableAccountRequest, IActionResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetQuerableAccountRequestHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetQuerableAccountRequest request, CancellationToken cancellationToken)
        {
            if (request.Start < 0 || request.Count < 0)
                return new BadRequestObjectResult(new { error = ActionMessages.InvalidQueryParametrs() });

            var users = _userManager.Users.Skip(request.Start).Take(request.Count);

            var usersViewModel = users.ProjectTo<AppUserViewModel>(_mapper.ConfigurationProvider);

            return new OkObjectResult(usersViewModel);
        }
    }
}
