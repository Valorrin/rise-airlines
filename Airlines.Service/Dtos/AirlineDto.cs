﻿using System.ComponentModel.DataAnnotations;

namespace Airlines.Service.Dto;
public class AirlineDto
{
    public int AirlineId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(6, ErrorMessage = "Name must be up to 6 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Founded is required")]
    public DateOnly Founded { get; set; }

    [Required(ErrorMessage = "FleetSize is required")]
    public int FleetSize { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }
}