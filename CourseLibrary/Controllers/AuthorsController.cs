using System;
using System.Collections.Generic;
using AutoMapper;
using CourseLibrary.Entities;
using CourseLibrary.Helpers;
using CourseLibrary.Models;
using CourseLibrary.ResourceParameters;
using CourseLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.Controllers
{
    [ApiController]
    [Route("api/v1/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                                       throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> Index([FromQuery] AuthorsResourceParameters authorsResourceParameters)
        {
            var authors = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authors));
        }

        [HttpGet("{authorId}", Name = "GetAuthor")]
        public ActionResult<AuthorDto> GetAuthor(Guid authorId)
        {
            
            var author = _courseLibraryRepository.GetAuthor(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AuthorDto>(author));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(CreateAuthorDto author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();
            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new {authorId = authorToReturn.Id}, authorToReturn);
        }
    }
}