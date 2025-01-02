using System;
using System.ComponentModel.DataAnnotations;

namespace FLIP_CRUD.DTOs.Post;

public class CreatePostDto
{
    [Required(ErrorMessage = "Author is required.")]
    public required string Author { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    
    public required string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string? Description { get; set; }
}
