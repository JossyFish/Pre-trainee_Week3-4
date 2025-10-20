using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WK_34.DTOs;
using WK_34.Models;
using WK_34.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WK_34.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }


        [HttpGet("getAll")]
        public async Task<ActionResult<List<Book>>> GetAll()
        {
            try
            {
                return Ok(await _booksService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("getById")]
        public async Task<ActionResult<Book>> GetById([Required] int id)
        {
            try
            {
                var book = await _booksService.GetByIdAsync(id);
                return book == null ? NotFound("Book not found") : Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }


        [HttpGet("getInInterval")]
        public async Task<ActionResult<List<Book>>> GetAllInInterval(
            [Required] DateTime startDate,
            [Required] DateTime endDate)
        {
            try
            {
                var books = await _booksService.GetAllInIntervalAsync(startDate, endDate);
                return Ok(books);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Book>> Create(CreateBookDto request)
        {
            try
            {
                var book = await _booksService.CreateAsync(request.Title, request.PublishedYear, request.AuthorId);
                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpPost("update")]
        public async Task<ActionResult<Book>> UpdateById(UpdateBookDto request)
        {
            try
            {
                var book = await _booksService.UpdateByIdAsync(request.Id, request.Title, request.PublishedYear, request.AuthorId);
                return Ok(book);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpDelete("deleteById")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            try
            {
                var result = await _booksService.DeleteByIdAsync(id);
                return result ? Ok() : StatusCode(500, "Failed to delete book");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }
    }

}

