using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FLIP_CRUD.Models.Entities;

namespace CRUD.Models.Entities;

public class UserEntity:BaseEntity
{

        [Key]
        public int Id { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }


        // Navigation property
        public ICollection<PostEntity>? Posts { get; set; } // User can have many posts


}
