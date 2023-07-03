using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/home")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INews _manageNews;
        public NewsController(INews manageNews)
        {
            _manageNews = manageNews;
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


        [Route("get-newsmoi")]
        [HttpGet]
        public async Task<IActionResult> getNewsGanDay()
        {
            var result = await _manageNews.GetNewsGanDay();
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }
    }
}
