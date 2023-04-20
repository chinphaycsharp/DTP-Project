using EPS.Service.Dtos.Role;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Menu
{
    public class MenuGridPagingDto : PagingParams<MenuGridDto>
    {
        public string FilterText { get; set; }
        public string ParentId { get; set; } = null;

        public override List<Expression<Func<MenuGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Name.Contains(FilterText));
            }  

            if (!string.IsNullOrEmpty(ParentId))
            {
                predicates.Add(x => x.ParentId == ParentId);
            }
            else
            {
                predicates.Add(x => x.ParentId == null);
            }
            return predicates;
        }
    }
}
