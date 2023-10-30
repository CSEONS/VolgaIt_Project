using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminRentCommands.GetAdminRentDetails
{
    public class GetAdminRentDetailsRequest : IRequest<IActionResult>
    {
        public string RentId { get; set; }
    }
}
