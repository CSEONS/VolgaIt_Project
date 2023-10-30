using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.AdminAccountCommands.Delete
{
    public class DeleteAccountRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string AccountId { get; set; }
    }
}
