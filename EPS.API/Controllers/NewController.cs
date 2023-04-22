using EPS.API.Commons;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.New;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/new")]
    [Authorize]
    public class NewController : BaseController
    {
        private Data.EPSContext _context;
        private NewService _newService;

        public NewController(NewService newService)
        {
            _newService = newService;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> GetListNews([FromQuery] NewGridPagingDto pagingModel)
        {
            return Ok(await _newService.GetNews(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.CreateMenu)]
        [HttpPost()]
        public async Task<ApiResult<int>> CreateNew(NewCreateDto NewCreateDto)
        {
            ApiResult<int> result = new ApiResult<int>();
            NewCreateDto.Created_date = DateTime.Now;
            var dto = await _newService.CreateNew(NewCreateDto);
            if (dto >0)
            {
                result.ResultObj = dto;
                result.Message = "Thêm mới thành công !";
                result.statusCode = 201;
                return result;
            }
            else
            {
                result.ResultObj = dto;
                result.Message = "Đã có lỗi xẩy ra với hệ thống, vui lòng thử lại !";
                result.statusCode = 500;
                return result;
            }
        }

        [CustomAuthorize(PrivilegeList.EditMenu)]
        [HttpPut("{id}")]
        public async Task<ApiResult<int>> UpdateNew(string id, NewUpdateDto menuUpdateDto)
        {
            ApiResult<int> result = new ApiResult<int>();
            menuUpdateDto.Updated_date = DateTime.Now;
            var dto = await _newService.UpdateNew(id, menuUpdateDto);
            if (dto == 1)
            {
                result.ResultObj = dto;
                result.Message = "Cập nhập thành công !";
                result.statusCode = 200;
                return result;
            }
            else
            {
                result.ResultObj = dto;
                result.Message = "Đã có lỗi xẩy ra với hệ thống, vui lòng thử lại !";
                result.statusCode = 500;
                return result;
            }
        }

        [CustomAuthorize(PrivilegeList.DeleteMenu)]
        [HttpDelete("{id}")]
        public async Task<ApiResult<int>> DeleteNew(string id)
        {
            ApiResult<int> result = new ApiResult<int>();
            var check = await _newService.DeleteNew(id);
            if (check == 1)
            {
                result.ResultObj = check;
                result.Message = "Xóa bản ghi thành công !";
                result.statusCode = 200;
                return result;
            }
            else
            {
                result.ResultObj = check;
                result.Message = "Đã có lỗi xẩy ra với hệ thống, vui lòng thử lại !";
                result.statusCode = 500;
                return result;
            }
        }
    }
}
