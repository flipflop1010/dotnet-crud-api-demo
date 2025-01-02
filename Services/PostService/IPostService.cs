using System;
using FLIP_CRUD.DTOs.Post;
using FLIP_CRUD.Models.Entities;

namespace FLIP_CRUD.Services.PostService;

public interface IPostService
{
    Task<IEnumerable<PostEntity>> GetAllPostsAsync();
    Task<PostEntity> GetPostByIdAsync(int id);
    Task<PostEntity> CreatePostAsync(CreatePostDto post);
    Task<PostEntity?> UpdatePostAsync(int id,UpdatePostDto post);
    Task<bool> DeletePostAsync(int id);
}
