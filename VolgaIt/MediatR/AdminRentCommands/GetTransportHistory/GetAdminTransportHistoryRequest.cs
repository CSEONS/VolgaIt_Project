using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AdminRentCommands.GetTransportHistory
{
    public class GetAdminTransportHistoryRequest: IRequest<IActionResult>
    {
        public string TransportId { get; set; }
    }
}
