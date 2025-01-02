using System;
using FLIP_CRUD.Data;
using FLIP_CRUD.DTOs.Post;
using FLIP_CRUD.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLIP_CRUD.Services.PostService;

public class PostService : IPostService
{
    private readonly ApplicationDbContext _dbContext;

    public PostService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PostEntity>> GetAllPostsAsync()
    {
        return await _dbContext.Posts.ToListAsync();
        // throw new NotImplementedException();
    }

    public async Task<PostEntity> GetPostByIdAsync(int id)
    {
        var post = await _dbContext.Posts.FindAsync(id);

        return post ?? throw new InvalidOperationException($"Post with ID {id} not found.");
    }

    public async Task<PostEntity> CreatePostAsync(CreatePostDto postDto)
    {
        var post = new PostEntity()
        {
            Author=postDto.Author,
            Title = postDto.Title,
            Description=postDto.Description,
        };

        await _dbContext.Posts.AddAsync(post);
        _dbContext.SaveChanges();
        return post;
    }


    public async Task<PostEntity?> UpdatePostAsync(int id, UpdatePostDto updatePostDto)
    {
        // Retrieve the post by ID
        var post = await _dbContext.Posts.FindAsync(id);

        // If the post doesn't exist, return null
        if (post == null)
        {
            return null;
        }

        // Update the fields only if they are provided
        if (!string.IsNullOrEmpty(updatePostDto.Title))
        {
            post.Title = updatePostDto.Title;
        }
        if (!string.IsNullOrEmpty(updatePostDto.Author))
        {
            post.Author = updatePostDto.Author;
        }
        if (!string.IsNullOrEmpty(updatePostDto.Description))
        {
            post.Description = updatePostDto.Description;
        }
        if (!string.IsNullOrEmpty(updatePostDto.Image))
        {
            post.Image = updatePostDto.Image;
        }

        // Update the timestamp
        post.UpdatedAt = DateTime.UtcNow;

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();

        // Return the updated post
        return post;

    }

    public async Task<bool> DeletePostAsync(int id)
    {
        // Retrieve the post by ID
        var post = await _dbContext.Posts.FindAsync(id);
        // Console.WriteLine("postId="+ id);
        // If the post doesn't exist, return null
        if (post == null)
        {
            return false;
        }
        Console.WriteLine(post.ToString());

        _dbContext.Posts.Remove(post);
        
        // Save changes asynchronously
        await _dbContext.SaveChangesAsync();

        return true;



    }
}
