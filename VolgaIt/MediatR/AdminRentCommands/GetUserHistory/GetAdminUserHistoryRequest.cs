using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace VolgaIt.MediatR.AdminRentCommands.GetUserHistory
{
    public class GetAdminUserHistoryRequest : IRequest<IActionResult>
    {
        public string UserId { get; set; }
    }
}
