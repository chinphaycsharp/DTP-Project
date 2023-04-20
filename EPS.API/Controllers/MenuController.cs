using EPS.API.Commons;
using EPS.API.Helpers;
using EPS.Data.Entities;
using EPS.Service;
using EPS.Service.Dtos.Menu;
using EPS.Service.Dtos.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EPS.API.Controllers
{
    [Produces("application/json")]
    [Route("api/menus")]
    [Authorize]
    public class MenuController : BaseController
    {
        private Data.EPSContext _context;
        private MenuService _menuService;

        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }

        //list all
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListMenus([FromQuery] MenuGridPagingDto pagingModel)
        {
            return Ok(await _menuService.GetMenus(pagingModel));
        }

        //create
        [CustomAuthorize(PrivilegeList.CreateMenu)]
        [HttpPost]
        public async Task<ApiResult<string>> Create(MenuCreateDto menuCreateDto)
        {
            ApiResult<string> result = new ApiResult<string>();
            menuCreateDto.Id = menuCreateDto.ParentId +"-"+ menuCreateDto.Id;
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
            //return Ok(await _authorizationService.CreateRole(roleCreateDto));
        }

        //update
        [CustomAuthorize(PrivilegeList.EditMenu)]
        [HttpPut("{id}")]
        public async Task<ApiResult<int>> UpdateRole(string id, MenuUpadateDto menuUpdateDto)
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
            //return Ok(await _authorizationService.UpdateRole(id, roleUpdateDto));
        }

        [CustomAuthorize(PrivilegeList.DeleteMenu)]
        [HttpDelete("{id}")]
        public async Task<ApiResult<int>> Delete(string id)
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
            // return Ok(await _authorizationService.DeleteRole(id));
        }
    }
}
