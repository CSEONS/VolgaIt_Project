using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminAccountCommands.Get
{
    public class GetAccountRequest : IRequest<IActionResult>
    {
        public string Id { get; set; }
    }
}
