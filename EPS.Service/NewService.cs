using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.New;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class NewService
    {
        private EPSBaseService _baseService;
        private IMapper _mapper;

        public NewService(EPSRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }

        public async Task<PagingResult<NewGridDto>> GetNews(NewGridPagingDto dto)
        {
            return await _baseService.FilterPagedAsync<New, NewGridDto>(dto);
        }

        public async Task<int> CreateNew(NewCreateDto dto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<New, NewCreateDto>(dto);
            return dto.Id;
        }

        public async Task<int> UpdateNew(string id, NewUpdateDto dto)
        {
            return await _baseService.UpdateAsync<New, NewUpdateDto>(id, dto);
        }

        public async Task<int> DeleteNew(string id)
        {
            return await _baseService.DeleteAsync<New, string>(id);
        }
    }
}
