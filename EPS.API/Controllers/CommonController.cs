using EPS.API.Commons;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Footer;
using EPS.Service.Dtos.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/common")]
    [Authorize]
    public class CommonController : BaseController
    {
        private Data.EPSContext _context;
        private MenuService _menuService;
        private FooterService _footerService;

        public CommonController(MenuService menuService, FooterService footerService)
        {
            _menuService = menuService;
            _footerService = footerService;
        }

        #region menu
        [HttpGet("menus")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListMenus([FromQuery] MenuGridPagingDto pagingModel)
        {
            return Ok(await _menuService.GetMenus(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.CreateMenu)]
        [HttpPost("menu")]
        public async Task<ApiResult<string>> CreateMenu(MenuCreateDto menuCreateDto)
        {
            ApiResult<string> result = new ApiResult<string>();
            menuCreateDto.Id = menuCreateDto.ParentId + "-" + menuCreateDto.Id;
            menuCreateDto.Created_date = DateTime.Now;
            var dto = await _menuService.CreateMenu(menuCreateDto);
            if (dto != "")
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
        [HttpPut("menu/{id}")]
        public async Task<ApiResult<int>> UpdateMenu(string id, MenuUpadateDto menuUpdateDto)
        {
            ApiResult<int> result = new ApiResult<int>();
            menuUpdateDto.Id = menuUpdateDto.ParentId + "-" + menuUpdateDto.Id;
            menuUpdateDto.Updated_date = DateTime.Now;
            var dto = await _menuService.UpdateMenu(id, menuUpdateDto);
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
        [HttpDelete("meunu/{id}")]
        public async Task<ApiResult<int>> DeleteMenu(string id)
        {
            ApiResult<int> result = new ApiResult<int>();
            var check = await _menuService.DeleteMenu(id);
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
        #endregion

        #region footer
        [HttpGet("footers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListFooters([FromQuery] FooterGridPagingDto pagingModel)
        {
            return Ok(await _footerService.GetFooters(pagingModel));
        }

        [CustomAuthorize(PrivilegeList.CreateMenu)]
        [HttpPost("footer")]
        public async Task<ApiResult<string>> CreateFooter(FooterCreateDto footerCreateDto)
        {
            ApiResult<string> result = new ApiResult<string>();
            footerCreateDto.Created_date = DateTime.Now;
            var dto = await _footerService.CreateFooter(footerCreateDto);
            if (dto != "")
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
        [HttpPut("footer/{id}")]
        public async Task<ApiResult<int>> UpdateFooter(string id, FooterUpdateDto menuUpdateDto)
        {
            ApiResult<int> result = new ApiResult<int>();
            menuUpdateDto.Id = menuUpdateDto.ParentId + "-" + menuUpdateDto.Id;
            menuUpdateDto.Updated_date = DateTime.Now;
            var dto = await _footerService.UpdateFooter(id, menuUpdateDto);
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
        [HttpDelete("footer/{id}")]
        public async Task<ApiResult<int>> DeleteFooter(string id)
        {
            ApiResult<int> result = new ApiResult<int>();
            var check = await _footerService.DeleteFooter(id);
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
        #endregion
    }
}
