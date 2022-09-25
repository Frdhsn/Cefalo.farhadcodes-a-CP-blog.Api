using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Services
{
    public class StoryService: IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IMapper _mapper;
        public StoryService(IStoryRepository storyRepository, IMapper mapper)
        {
            _storyRepository = storyRepository;
            _mapper = mapper;
        }

        public async Task<List<StoryDTO>> GetStories()
        {
            var stories = await _storyRepository.GetStories();
            return stories.Select(story => _mapper.Map<StoryDTO>(story)).ToList();
        }

        public async Task<Story> GetStory(int id)
        {
            var story = await _storyRepository.GetStory(id);
            return story;
        }

        public async Task<Story> CreateStory(StoryDTO body)
        {
            Story story = _mapper.Map<Story>(body);
            story.CreationTime = DateTime.UtcNow;
            story.LastModifiedTime = DateTime.UtcNow;
            var createdStory = await _storyRepository.CreateStory(story);
            return createdStory;
        }

        public async Task<Story> UpdateStory(int id, UpdateStory body)
        {
            if (id != body.Id)
                return null;
            Story story = _mapper.Map<Story>(body);
            var updatedStory = await _storyRepository.UpdateStory(id, story);
            return updatedStory;
        }
        public async Task<bool?> DeleteStory(int id)
        {
            return await _storyRepository.DeleteStory(id);
        }
    }
}
