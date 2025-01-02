using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FLIP_CRUD.Models.Entities;

public class PostEntity : BaseEntity
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public required string Author { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    // Image URL or Path (stored as a string)
        public string? Image { get; set; } // URL or path to the image


    // Relationship with User (assumed relation to User entity)
    // public int UserId { get; set; }  // Foreign key for the User entity
    public  UserEntity? User { get; set; }  // Navigation property for related User


}
