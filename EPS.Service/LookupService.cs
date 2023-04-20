using AutoMapper;
using EPS.Data;
using EPS.Data.Entities;
using EPS.Service.Dtos.Common;
using EPS.Service.Dtos.Privilege;
using EPS.Service.Dtos.RolePrivilege;
using EPS.Service.Helpers;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Service
{
    public class LookupService
    {
        private EPSBaseService _baseService;
        private EPSRepository _repository;
        private IMapper _mapper;

        public LookupService(EPSRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _baseService = new EPSBaseService(repository, mapper);
        }


        public async Task<List<SelectItem>> GetRoles()
        {
            return await _baseService.Filter<Role, SelectItem>().ToListAsync();
        }

        public async Task<string> CreatePrivilege(PrivilegeCreateDto dto, bool isExploiting = false)
        {
            await _baseService.CreateAsync<Privilege, PrivilegeCreateDto>(dto);
            return dto.Id;
        }

        public async Task<PagingResult<PrivilegeGridDto>> GetPrivileges(PrivilegeGridPagingDto dto)
        {
            return await _baseService.FilterPagedAsync<Privilege, PrivilegeGridDto>(dto);
        }

        public async Task<int> UpdatePrivilege(string id, PrivilegeUpdateDto dto)
        {
            return await _baseService.UpdateAsync<Privilege, PrivilegeUpdateDto>(id, dto);
        }

        public async Task<List<ListNamePrivilegeDto>> GetNamePrivilege()
        {
            //var privileges = await _baseService.All<Privilege, PrivilegeGridDto>().ToListAsync();
            var cate = new List<ListNamePrivilegeDto>();
            string[] names = new string[] { "User", "Role", "Privilege", "Category", "Tree", "Order", "Post", "Tag", "Tool","Test" , "Image"};
            string[] description = new string[] { "Quản lý người dùng", "Quản lý nhóm", "Quản lý quyền", "Quản lý danh mục", "Quản lý cây cảnh"
                , "Quản lý đơn hàng",  "Quản lý bài viết","Quản lý danh mục bài viết", "Quản lý dụng cụ cây cảnh","Test","Quản lý ảnh" };
            for (int i = 0; i < names.Length; i++)
            {
                cate.Add(new ListNamePrivilegeDto()
                {
                    Name = names[i],
                    Description = description[i]
                });
            }
            return cate;
        }

        public async Task<List<RolePrivilegeGridDto>> GetContainPrivilegeName(string name,int id)
        {
            var privileges = await _baseService.All<Privilege, PrivilegeGridDto>().ToListAsync();
            var rolePrivileges = await _baseService.All<RolePrivilege, RolePrivilegeGridDto>().ToListAsync();
            List<RolePrivilegeGridDto> privilegeGridDtos = new List<RolePrivilegeGridDto>();
            RolePrivilegeGridDto p = null;

            foreach (var item in privileges.Where(x=>x.Status == true))
            {
                p = new RolePrivilegeGridDto()
                {
                    PrivilegeId = item.Id,
                    PrivilegeName = item.Name,
                    Status = false
                };
                if (item.Id.Contains(name))
                {
                    privilegeGridDtos.Add(p);
                }
            }

            foreach (var item in rolePrivileges.Where(x=>x.RoleId == id))
            {
                foreach (var privilege in privileges.Where(x => x.Status == true))
                {
                    if (item.PrivilegeId == privilege.Id && privilege.Id.Contains(name))
                    {
                        var c = privilegeGridDtos.FirstOrDefault(x => x.PrivilegeId == privilege.Id);
                        c.Status = true;
                        c.RoleId = id;
                    }
                }
            }

            return privilegeGridDtos.Where(x => x != null).ToList();
        }

        //public async Task<List<TurnoverFollowMonthGridDto>> GetTurnoverFollowMonths()
        //{
        //    //List<TopProductBestSellerGridDto> result = new List<TopProductBestSellerGridDto>();
        //    var turnoverFollowMonths = await _baseService.All<TurnoverFollowMonth, TurnoverFollowMonthGridDto>().ToListAsync();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        var a = turnoverFollowMonths.FirstOrDefault(x => x.month == i);
        //        if (a == null)
        //        {
        //            TurnoverFollowMonthGridDto dto = new TurnoverFollowMonthGridDto();
        //            dto.month = i;
        //            dto.doanh_thu = 0;
        //            turnoverFollowMonths.Add(dto);
        //        }
        //    }
        //    return turnoverFollowMonths.OrderBy(x=>x.month).ToList();
        //}

        //public async Task<List<TopProductBestSellerGridDto>> GetTopProducts()
        //{
        //    //List<TopProductBestSellerGridDto> result = new List<TopProductBestSellerGridDto>();
        //    var trees = await _baseService.All<topTreeBestSaller, TopProductBestSellerGridDto>().ToListAsync();
        //    var tools = await _baseService.All<topToolBestSaller, TopProductBestSellerGridDto>().ToListAsync();
        //    foreach (var item in tools)
        //    {
        //        trees.Add(item);
        //    }
        //    return trees.Take(5).ToList();
        //}

        //public async Task<int[]> GetInformation()
        //{
        //    //List<TopProductBestSellerGridDto> result = new List<TopProductBestSellerGridDto>();
        //    int[] final = new int[3];
        //    var totalCustomers = await _baseService.All<TotalCustomer, TotalCustomerGridDto>().ToListAsync();
        //    final[0] = totalCustomers.FirstOrDefault().so_khach_hang;
        //    var totalPrices = await _baseService.All<list_order_user_view, OrderGridDto>().ToListAsync();
        //    final[1] = Convert.ToInt32(totalPrices.Sum(x => x.totalPrice));
        //    var result = await _baseService.All<order, OrderGridDto>().ToListAsync();
        //    final[2] = Convert.ToInt32(result.Count);
        //    return final;
        //}

        //public async Task<List<UnitTreeDto>> GetUnitTree()
        //{
        //    var units = await _baseService.All<Unit, UnitTreeDto>().ToListAsync();

        //    return BuildTree(units);
        //}

        //private List<UnitTreeDto> BuildTree(IEnumerable<UnitTreeDto> allUnits, int? rootUnit = null)
        //{
        //    var tree = new List<UnitTreeDto>();

        //    List<UnitTreeDto> roots = new List<UnitTreeDto>();
        //    if (rootUnit == null)
        //    {
        //        roots = allUnits.Where(x => x.ParentId == null).ToList();
        //    }
        //    else
        //    {
        //        var rootNode = allUnits.FirstOrDefault(x => x.Id == rootUnit);
        //        if (rootNode != null)
        //        {
        //            roots.Add(rootNode);
        //        }
        //    }

        //    foreach (var r in roots)
        //    {
        //        tree.Add(r);
        //        UnitTreeDto nextNode = r;
        //        UnitTreeDto currentNode;
        //        UnitTreeDto parentNode;
        //        int index;
        //        int level = 1;


        //        while (nextNode != null)
        //        {
        //            currentNode = nextNode;
        //            nextNode = null;

        //            foreach (var child in allUnits.Where(x => x.ParentId == currentNode.Id))
        //            {
        //                child.Parent = currentNode;
        //                currentNode.Children.Add(child);
        //            }

        //            if (currentNode.Children.Any())
        //            {
        //                nextNode = currentNode.Children[0];
        //                level++;
        //            }
        //            else
        //            {
        //                while (currentNode.Parent != null)
        //                {
        //                    parentNode = currentNode.Parent;
        //                    index = parentNode.Children.IndexOf(currentNode);
        //                    if (index < parentNode.Children.Count - 1)
        //                    {
        //                        nextNode = parentNode.Children[index + 1];
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        currentNode = parentNode;
        //                        level--;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return tree;
        //}
    }
}
