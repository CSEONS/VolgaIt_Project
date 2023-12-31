﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace VolgaIt.MediatR.AdminAccountCommands.Update
{
    public class UpdateAccountRequest : IRequest<IActionResult>
    {
        [JsonIgnore]
        public string? AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public double Balance { get; set; }
    }
}
