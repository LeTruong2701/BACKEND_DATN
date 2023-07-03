using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.WebAPI.jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class NewsController : ControllerBase
    {
        private readonly IManageNews _manageNews;
        public NewsController(IManageNews manageNews)
        {
            _manageNews = manageNews;
        }

        [Authorize(Roles = SecurityRoles.Admin + "," + SecurityRoles.Manager)]
        [Route("create-news")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] NewsModel news)
        {
            var result = await _manageNews.Create(news);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Create Failed");
        }

        [Route("search-news")]
        [HttpPost]

        public async Task<IActionResult> SearchNews([FromBody] Dictionary<string, object> formData)
        {
            var result = await _manageNews.SearchNewsPaging(formData);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

       
        [Route("get-by-id-news/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var result = await _manageNews.GetById(Id);
            if (result == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(result);

        }

        [Authorize(Roles = SecurityRoles.Admin + "," + SecurityRoles.Manager)]
        [Route("update-news")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] NewsModel request)
        {
            var result = await _manageNews.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }
        [Authorize(Roles = SecurityRoles.Admin + "," + SecurityRoles.Manager)]
        [Route("delete-news/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNews(int Id)
        {
            var result = await _manageNews.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }
    }
}
