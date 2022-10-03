using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.farhadcodes_a_CP_blog.Repository.Contracts
{
    public interface IStoryRepository
    {
        Task<List<Story>> GetStories();
        Task<List<Story>> GetStoriesByUser(int id);
        Task<List<Story>> GetPaginatedStories(int PageNumber, int PageSize);
        Task<Story?> GetStory(int id);

        Task<Story?> CreateStory(Story body);
        Task<Story?> UpdateStory(int id,Story body);
        Task<Boolean?> DeleteStory(int id);

    }
}
