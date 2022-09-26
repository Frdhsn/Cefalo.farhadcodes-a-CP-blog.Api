using Cefalo.farhadcodes_a_CP_blog.Database.Context;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.Repositories
{
    public class StoryRepository: IStoryRepository
    {
        private readonly CPContext _cpContext;
        public StoryRepository(CPContext cpContext)
        {
            _cpContext = cpContext;
        }
        public async Task<List<Story>> GetStories()
        {
            return await _cpContext.Stories.ToListAsync();
        }
        public async Task<Story?> GetStory(int id)
        {
            return await _cpContext.Stories.FindAsync(id);

        }
        public async Task<Story?> CreateStory(Story body)
        {
            _cpContext.Stories.Add(body);
            await _cpContext.SaveChangesAsync();
            return body;
            //return await _cpContext.Stories.SingleOrDefaultAsync();
        }
        public async Task<Story?> UpdateStory(int id, Story body)
        {
            var updatedStory = await _cpContext.Stories.FindAsync(id);
            if (updatedStory == null)
                return null;
            if(body.Title != null) updatedStory.Title = body.Title;

            if (body.Description != null) updatedStory.Description = body.Description;

            if (body.Difficulty != null) updatedStory.Difficulty = body.Difficulty;

            if (body.Topic != null) updatedStory.Topic = body.Topic;

            updatedStory.LastModifiedTime = DateTime.UtcNow;
            await _cpContext.SaveChangesAsync();
            return updatedStory;
        }
        public async Task<Boolean?> DeleteStory(int id)
        {
            var story = await _cpContext.Stories.FindAsync(id);
            if (story == null)
                return false;
            _cpContext.Stories.Remove((Story)story);
            await _cpContext.SaveChangesAsync();
            return true;
        }
    }
}
