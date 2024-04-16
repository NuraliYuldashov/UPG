using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Accessories : BaseEntity
{
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
