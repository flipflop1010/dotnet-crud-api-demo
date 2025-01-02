using System;

namespace FLIP_CRUD.DTOs.Post;

public class UpdatePostDto
{
    public  string? Author { get; set; }
    public string? Title { get; set; } // Optional
    public string? Description { get; set; } // Optional
    public string? Image { get; set; } // Optional if you support updating images

}
