﻿using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Service.Contracts
{
    public interface IStoryService
    {

        Task<List<ShowStoryDTO>> GetStories();
        Task<PagedResponse<List<ShowStoryDTO>>> GetPaginatedStories(PaginationFilter validFilter);
        Task<List<ShowStoryDTO>> GetStoriesByUser(int id);
        Task<ShowStoryDTO> GetStory(int id);
        Task<Story> CreateStory(StoryDTO body);
        Task<Story> UpdateStory(int id, UpdateStory body);
        Task<bool?> DeleteStory(int id);
    }
}
