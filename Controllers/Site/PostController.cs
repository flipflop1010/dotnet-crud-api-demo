using System.Collections.Specialized;
using FLIP_CRUD.DTOs.Post;
using FLIP_CRUD.Services.PostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace FLIP_CRUD.Controllers.Site
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // [HttpGet]
        // public IActionResult Test()
        // {
        //     return Ok("This is a test route.");
        // }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            // return Ok("Hello");
            var posts = await _postService.GetAllPostsAsync();
            var respData = new Dictionary<string, object>();
            respData["data"] = posts;

            return Ok(respData);
        }

        [HttpPost]
        public async Task<IActionResult> Store([FromBody]CreatePostDto createPostDto)
        {
            if(!ModelState.IsValid){
                return  BadRequest(ModelState);
            }
            // return Ok(createPostDto);

            var resp = await _postService.CreatePostAsync(createPostDto);

            var respData = new Dictionary<string, object>();

            respData["data"] = resp;

            return Ok(respData);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Show(int id)
        {

            try
            {
                var resp = await _postService.GetPostByIdAsync(id);
                var respData = new Dictionary<string, object>();

                respData["data"] = resp;

                return Ok(respData);
            }
            catch (System.Exception e)
            {

                return NotFound(e.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePostDto updatePostDto)
        {


            try
            {
                var resp = await _postService.UpdatePostAsync(id, updatePostDto);
                var respData = new Dictionary<string, object>();
                respData["message"] = "data updated successfully";
                respData["data"] = resp;
                return Ok(respData);
            }
            catch (System.Exception e)
            {

                return NotFound(e.Message);
            }
        }


        // delete post
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            // Console.WriteLine("id="+id);

            try
            {
                var resp = await _postService.DeletePostAsync(id);
                if(!resp){
                    return NotFound("No data found");
                }
                var respData = new Dictionary<string, object>();
                respData["message"] = "data deleted successfully";
                respData["data"] = resp;
                return Ok(respData);
            }
            catch (System.Exception e)
            {

                return NotFound(e.Message);
            }
        }

    }
}
