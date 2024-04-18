﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
