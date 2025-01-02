using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models.Entities;

public abstract class BaseEntity
{
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

}
