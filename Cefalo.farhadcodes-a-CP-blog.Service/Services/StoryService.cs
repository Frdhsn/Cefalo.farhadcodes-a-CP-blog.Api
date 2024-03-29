﻿using AutoMapper;
using Cefalo.farhadcodes_a_CP_blog.Database.Models;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.CustomExceptions;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.Helper;
using Cefalo.farhadcodes_a_CP_blog.Service.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
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
        private readonly IPassword _passwordH;
        //private readonly IUriService _uriService;
        private readonly BaseDTOValidator<UpdateStory> _updatestorydtovalidator;
        private readonly BaseDTOValidator<StoryDTO> _storydtovalidator;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public StoryService(IStoryRepository storyRepository, IMapper mapper, IPassword password, BaseDTOValidator<UpdateStory> updatestorydtovalidator, BaseDTOValidator<StoryDTO> storydtovalidator)
        {
            _storyRepository = storyRepository;
            _mapper = mapper;
            _passwordH = password;
            _updatestorydtovalidator = updatestorydtovalidator;
            _storydtovalidator = storydtovalidator;
        }

        public async Task<List<ShowStoryDTO>> GetStories()
        {
            var stories = await _storyRepository.GetStories();
            return stories.Select(story => _mapper.Map<ShowStoryDTO>(story)).ToList();
        }
        public async Task<PagedResponse<List<ShowStoryDTO>>> GetPaginatedStories(PaginationFilter validFilter)
        {
            var stories = await _storyRepository.GetPaginatedStories(validFilter.PageNumber,validFilter.PageSize);
            var response = stories.Select(story => _mapper.Map<ShowStoryDTO>(story)).ToList();
            var totalRecords = await _storyRepository.CountStory();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ShowStoryDTO>(response, validFilter, totalRecords);
            return pagedReponse;
        }
        public async Task<List<ShowStoryDTO>> GetStoriesByUser(int id)
        {
            var stories = await _storyRepository.GetStoriesByUser(id);
            return stories.Select(story => _mapper.Map<ShowStoryDTO>(story)).ToList();
        }

        public async Task<ShowStoryDTO> GetStory(int id)
        {
            var story = await _storyRepository.GetStory(id);

            if (story == null) throw new NotFoundHandler("No Story was found with that Id");
       
            return _mapper.Map<ShowStoryDTO>(story);
        }

        public async Task<Story> CreateStory(StoryDTO body)
        {
            _storydtovalidator.ValidateDTO(body);

            Story story = _mapper.Map<Story>(body);
            story.CreationTime = DateTime.UtcNow;
            story.LastModifiedTime = DateTime.UtcNow;
            var createdStory = await _storyRepository.CreateStory(story);
            return createdStory;
        }

        public async Task<Story> UpdateStory(int id, UpdateStory body)
        {
            _updatestorydtovalidator.ValidateDTO(body);

            if (id != body.Id) throw new BadRequestHandler("Bad request! Story Id doesn't match!");
            // no story exists
            var fetchedStory = await _storyRepository.GetStory(id);
            if(fetchedStory == null) throw new NotFoundHandler("No Story was found with that Id");

            var currUserId = _passwordH.GetLoggedInId();
            if(currUserId != fetchedStory.AuthorID) throw new ForbiddenHandler("You don't have the permission!");
            // authorization
            var creationTime = _passwordH.GetTokenCreationTime();
            if (creationTime == null) throw new UnauthorisedHandler("You're not logged in! Please log in to get access.");
            
            // jwt token expiration check
            //DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            //if (tokenCreationTime.ToOADate() + 86400000 < fetchedStory.LastModifiedTime.ToOADate())
            //    throw new UnauthorisedHandler("JWT Expired! Login again!");
            
            
            Story story = _mapper.Map<Story>(body);
            var updatedStory = await _storyRepository.UpdateStory(id, story);
            return updatedStory;
        }
        public async Task<bool?> DeleteStory(int id)
        {
            var fetchedStory = await _storyRepository.GetStory(id);
            if (fetchedStory == null) throw new NotFoundHandler("No Story was found with that Id");
            var currUserId = _passwordH.GetLoggedInId();
            if (currUserId != fetchedStory.AuthorID) throw new ForbiddenHandler("You don't have the permission!");
            var creationTime = _passwordH.GetTokenCreationTime();
            if (creationTime == null) throw new UnauthorisedHandler("You're not logged in! Please log in to get access.");
            //DateTime tokenCreationTime = Convert.ToDateTime(creationTime);

            //if (tokenCreationTime.ToOADate() + 86400000 < fetchedStory.LastModifiedTime.ToOADate())
            //    throw new UnauthorisedHandler("JWT Expired! Login again!");

            return await _storyRepository.DeleteStory(id);
        }
    }
}
