﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VolgaIt.MediatR.AccountCommands.Register
{
    public class RegisterRequest : IRequest<IActionResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
