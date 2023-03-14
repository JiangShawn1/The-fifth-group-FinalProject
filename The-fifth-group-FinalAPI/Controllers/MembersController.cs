using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.Models;
using The_fifth_group_FinalAPI.DTOs;
using Microsoft.AspNetCore.Cors;


namespace The_fifth_group_FinalAPI.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public MembersController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Members>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Members>> GetMembers(int id)
        {
            var members = await _context.Members.FindAsync(id);

            if (members == null)
            {
                return NotFound();
            }

            return members;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembers(int id, Members members)
        {
            if (id != members.MemberId)
            {
                return BadRequest();
            }

            _context.Entry(members).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Members
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Members>> PostMembers(Members members)
        //{
        //    _context.Members.Add(members);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMembers", new { id = members.MemberId }, members);
        //}
        [HttpPost]
        [Route("MemberLogin")]
        [AllowAnonymous]
        public async Task<MemberDTO> MemberLogin(MemberDTO member)
        {
            var result = GetByAccount(member.Account, member.Password);

            if (result == null)
            {
                return null;
            }
          
            return result;
        }
        //public async Task<string> MemberLogin(MemberDTO member)
        //{
        //    var result = GetByAccount(member.Account, member.Password);

        //    if (result == null)
        //    {
        //        return "登入失敗";
        //    }
           
        //    HttpContext.Response.Cookies.Append("MemberId", result.MemberId.ToString(), new CookieOptions()
        //    {
        //        Expires = DateTime.UtcNow.AddHours(1), // 設置過期時間為1小時後               
        //        HttpOnly = true, // 防止跨站腳本攻擊
        //        Secure = true // 只能在HTTPS下傳輸
        //    });
        //    return "登入成功";
        //}

		[HttpPost("MemberLogOut")]
        public async Task<string> MemberLogOut()
		{
			Response.Cookies.Delete("MemberId");
			
			return "登出成功";
		}

		MemberDTO GetByAccount(string Account,string Password)
        {
            var member=_context.Members.SingleOrDefault(x => x.Account == Account);
			

			if (member == null) { return null; }
            if (member.Password != Password) { return null; }
            return new MemberDTO
            {
                MemberId= member.MemberId,
                Account=member.Account,
                Password=member.Password,
            } ;
        }


		// DELETE: api/Members/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembers(int id)
        {
            var members = await _context.Members.FindAsync(id);
            if (members == null)
            {
                return NotFound();
            }

            _context.Members.Remove(members);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembersExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}
