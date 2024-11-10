using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "RequireSysAdminRole")]
        public IActionResult Create([FromBody] UserCreateRequest user)
        {
            try
            {
                var obj = _userService.Create(user);

                return Ok(obj);
            }
            catch (DuplicateElementException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(Policy = "RequireSysAdminRole")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        [Authorize(Policy = "RequireSysAdminRole")]
        public IActionResult Update([FromRoute] int id, [FromBody] UserUpdateRequest user)
        {
            try
            {
                _userService.Update(id, user);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]/{id}")]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            try
            {
                return _userService.GetById(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<List<User>> GetAll()
        {
            return _userService.GetAll();
        }
    }
}