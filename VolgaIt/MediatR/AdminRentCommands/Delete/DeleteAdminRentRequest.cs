using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminRentCommands.Delete
{
    public class DeleteAdminRentRequest : IRequest<IActionResult>
    {
        public string RentId { get; set; }
    }
}
