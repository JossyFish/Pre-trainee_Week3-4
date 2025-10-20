using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WK_34.Data.Interfaces;
using WK_34.DTOs;
using WK_34.Models;
using WK_34.Services.Interfaces;

namespace WK_34.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        IAuthorsService _authorsService;
        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }


        [HttpGet("getAll")]
        public async Task<ActionResult<List<Author>>> GetAll()
        {
            try
            {
                return Ok(await _authorsService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }

        [HttpGet("getWithBooksAmount")]
        public async Task<ActionResult<List<Author>>> GetAuthorsWithBooksAmount()
        {
            try
            {
                return Ok(await _authorsService.GetAuthorsWithBooksAmountAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server error");
            }
        }


        [HttpGet("getByName")]
        public async Task<ActionResult<Author>> GetByName([Required] string name)
        {
            try
            {
                var author =  await _authorsService.GetByNameAsync(name);

                return author == null ? NotFound("Author not found") : Ok(author);
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

        [HttpGet("getById")]
        public async Task<ActionResult<Author>> GetById([Required] int id)
        {
            try
            {
                var author = await _authorsService.GetByIdAsync(id);

                return author == null ? NotFound("Author not found") : Ok(author);
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
        public async Task<ActionResult<Author>> Create(CreateAuthorDto request)
        {
            try
            {
                return Ok(await _authorsService.CreateAsync(request.Name, request.DateOfBirth));
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
        public async Task<IActionResult> UpdateById(UpdateAuthorDto request)
        {
            try
            {
                return Ok(await _authorsService.UpdateByIdAsync(request.Id, request.Name, request.DateOfBirth));
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
                var result = await _authorsService.DeleteByIdAsync(id);
                return result ?  Ok() : StatusCode(500, "Failed to delete author");
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
