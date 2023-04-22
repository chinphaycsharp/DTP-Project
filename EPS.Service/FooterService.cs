using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Footer;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class FooterService
    {
        private EPSBaseService _baseService;
        private IMapper _mapper;

        public FooterService(EPSRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }
        public async Task<PagingResult<FooterGridDto>> GetFooters(FooterGridPagingDto dto)
        {
            return await _baseService.FilterPagedAsync<Footer, FooterGridDto>(dto);
        }

        public async Task<string> CreateFooter(FooterCreateDto dto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Footer, FooterCreateDto>(dto);
            return dto.Id;
        }

        public async Task<int> UpdateFooter(string id, FooterUpdateDto dto)
        {
            return await _baseService.UpdateAsync<Footer, FooterUpdateDto>(id, dto);
        }

        public async Task<int> DeleteFooter(string id)
        {
            return await _baseService.DeleteAsync<Footer, string>(id);
        }

    }
}
