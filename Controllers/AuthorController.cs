using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rest_api_template.Models;
using rest_api_template.Services;

namespace rest_api_template.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly ILibraryService _libraryService;

        public AuthorController(ILogger<AuthorController> logger, ILibraryService library)
        {
            _logger = logger;
            _libraryService = library;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _libraryService.GetAuthorsAsync();
            if (authors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
            }
            return StatusCode(StatusCodes.Status200OK, authors);
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetAuthor(Guid id, bool includeBooks = false)
        {
            Author author = await _libraryService.GetAuthorAsync(id, includeBooks);

            if (author == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            var dbAuthor = await _libraryService.AddAuthorAsync(author);

            if (dbAuthor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be added.");
            }

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAuthor(Guid id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            Author dbAuthor = await _libraryService.UpdateAuthorAsync(author);

            if (dbAuthor == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Name} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _libraryService.GetAuthorAsync(id, false);
            (bool status, string message) = await _libraryService.DeleteAuthorAsync(author);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, author);
        }
    }
}