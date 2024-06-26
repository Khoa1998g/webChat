﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.Entities;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {


        private readonly UserManager<ManageUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ManageAppDbContext _context;
        public UserController(UserManager<ManageUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ManageAppDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;


        }

        [HttpPost]

        public async Task<IActionResult> PostUser(UserCreateRequest request) // vì khởi tạo lên ta dùng request
        {
            var dob = DateTime.Parse(request.Dob);
            var user = new ManageUser() // vì tạo một User lên ta dùng User Entites luân vì nó có đủ các tường
            {
               

            };
            var result = await _userManager.CreateAsync(user, request.Password); // phương thức CreateAsync đã được Identity.Core, hỗ trợ , bài miên phí ta phải viết nó
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, request);
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse(result));
            }
        }




        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users;

            var uservms = await users.Select(u => new UserViewModel() // vì muốn xem lên ta dùng UserVm
            {
                

            }).ToListAsync();

            return Ok(uservms);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new ApiNotFoundResponse($"Cannot found user with id: {id}"));

            var userVm = new UserViewModel()
            {
                
            };
            return Ok(userVm);
        }


    }
}