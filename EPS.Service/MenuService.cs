using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Menu;
using EPS.Service.Dtos.Privilege;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class MenuService
    {
        private EPSBaseService _baseService;
        private IMapper _mapper;

        public MenuService(EPSRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<MenuGridDto>> GetMenus(MenuGridPagingDto dto)
        {
            return await _baseService.FilterPagedAsync<Menu, MenuGridDto>(dto);
        }

        public async Task<string> CreateMenu(MenuCreateDto dto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Menu, MenuCreateDto>(dto);
            return dto.Id;
        }

        public async Task<int> UpdateMenu(string id, MenuUpadateDto dto)
        {
            return await _baseService.UpdateAsync<Menu, MenuUpadateDto>(id, dto);
        }

        public async Task<int> DeleteMenu(string id)
        {
            return await _baseService.DeleteAsync<Menu, string>(id);
        }
    }
}
