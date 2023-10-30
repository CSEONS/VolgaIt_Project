using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminAccountCommands.GetQuerable
{
    public class GetQuerableAccountRequest : IRequest<IActionResult>
    {
        public int Start {  get; set; }
        public int Count { get; set; }
    }
}
