﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;
using VolgaIt.Domain.Entities;

namespace VolgaIt.MediatR.TransportCommands.Create
{
    public class CreateTransportRequest : IRequest<IActionResult>
    {
        public bool CanBeRented { get; set; }
        public string TrnasportType { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Identifier { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? MinutePrice { get; set; }
        public double? DayPrice { get; set;}
        [JsonIgnore]
        public string? OwnerId { get; set; }
        [JsonIgnore]
        public ClaimsPrincipal? User { get; internal set; }
    }
}
