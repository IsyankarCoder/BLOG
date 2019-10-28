using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLOG.Data;
using BLOG.Model;
using Microsoft.EntityFrameworkCore;

namespace BLOG.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BlogPostController 
        : ControllerBase
    {
        private readonly BlogPostsContext _blogPostsContext;
        private readonly IRepository<BlogPost> _dataRepository;

        public BlogPostController(BlogPostsContext blogPostsContext,IRepository<BlogPost> repository)
        {
            _blogPostsContext = blogPostsContext;
            _dataRepository = repository;
        }


        // GET : api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPost()
        {
            return await _blogPostsContext.BlogPost.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blogPost = await _blogPostsContext.BlogPost.FindAsync(id);
            if (blogPost != null)
                return Ok(blogPost);

            return NotFound();
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<IActionResult> PostBlogPost([FromBody] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dataRepository.Add(blogPost);
            var save = _dataRepository.SaveAsync(blogPost);

            return CreatedAtAction("GetBlogPost", new { id = blogPost.PostId }, blogPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] int id, [FromBody] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != blogPost.PostId)
                return BadRequest(ModelState);
            if (!BlogPostExist(id))
                return NotFound();

            _blogPostsContext.Entry(blogPost).State = EntityState.Modified;

            try
            {
                _dataRepository.Update(blogPost);
                var save = await _dataRepository.SaveAsync(blogPost);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }


            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bl = await _blogPostsContext.BlogPost.FindAsync(id);
            if (bl == null)
                return NotFound();

            _dataRepository.Delete(bl);

            var save = await _dataRepository.SaveAsync(bl);
            return Ok(bl);
        }

        private bool BlogPostExist(int id)
        {
            return _blogPostsContext.BlogPost.Any(d => d.PostId == id);
        }


        

    }
}