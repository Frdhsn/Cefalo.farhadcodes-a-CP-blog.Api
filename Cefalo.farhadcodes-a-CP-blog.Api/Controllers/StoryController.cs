using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cefalo.farhadcodes_a_CP_blog.Api.Controllers
{
    [Route("api/stories")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        
            private readonly IStoryService _storyService;
            public StoryController(IStoryService storyService)
            {
                _storyService = storyService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<StoryDTO>>> GetStories()
            {
                return Ok(await _storyService.GetStories());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetStory(int id)
            {
                var story = await _storyService.GetStory(id);
                if (story == null)
                    return BadRequest("Story with this ID NOT FOUND!");
                return Ok(story);
            }
            [HttpPost]
            public async Task<IActionResult> PostStory(StoryDTO body)
            {
                var createdStory = await _storyService.CreateStory(body);
                if (createdStory == null) return BadRequest("Something went wrong! Can't Create the story!");
                return CreatedAtAction(nameof(PostStory), createdStory);
            }

            [HttpPatch("{id}")]
            public async Task<IActionResult> UpdateStory(int id, UpdateStory body)
            {
                if (id != body.Id)
                    return BadRequest("Something went wrong! IDs do not match!");
                var updatedStory = await _storyService.UpdateStory(id, body);
                if (updatedStory == null)
                    return BadRequest("Something went wrong! This Story can not be found!");
                return Ok(updatedStory);
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteStory(int id)
            {
                var deleted = await _storyService.DeleteStory(id);
                if (deleted == false)
                    return BadRequest("Something went wrong! This Story can not be deleted!");
                return NoContent();
            }
    }
}
