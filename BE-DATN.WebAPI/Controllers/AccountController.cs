using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AccountController : ControllerBase
    {
        private readonly IManageAccount _manageAccount;
        public AccountController(IManageAccount manageAccount)
        {
            _manageAccount = manageAccount;
        }

        [Route("search-account")]
        [HttpPost]

        public async Task<IActionResult> SearchAccount([FromBody] Dictionary<string, object> formData)
        {
            var accounts = await _manageAccount.SearchDanhMucPaging(formData);
            if (accounts == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(accounts);
        }

        [Route("get-by-id-account/{id}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(int Id)
        {
            var acc = await _manageAccount.GetById(Id);
            if (acc == null)
            {
                return BadRequest("Cannot find category");
            }
            return Ok(acc);

        }


        [Route("getall-account")]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            var acc = await _manageAccount.GetAccount();
            if (acc == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(acc);
        }

        [Route("create-account")]
        [HttpPost]
        public async Task<IActionResult> create([FromBody] AccountModel account)
        {
            var acc = await _manageAccount.Create(account);
            if (acc == null)
            {
                return BadRequest("Get Failed");
            }
            else
            {
                return Ok(acc);
            }

        }

        [Route("update-account")]
        [HttpPut]
        public async Task<IActionResult> update([FromBody] AccountModel request)
        {
            var result = await _manageAccount.Update(request);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Update Failed");
        }




        [Route("delete-account/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDanhMuc(int Id)
        {
            var result = await _manageAccount.Delete(Id);
            if (result == 1)
            {
                return Ok(new { data = "OK" });
            }

            return BadRequest("Delete Failed");
        }

    }
}
