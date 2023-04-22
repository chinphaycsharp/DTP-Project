using EPS.Service.Dtos.Menu;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EPS.Service.Dtos.Footer
{
    public class FooterGridPagingDto : PagingParams<FooterGridDto>
    {
        public string FilterText { get; set; }
        public string ParentId { get; set; } = null;

        public override List<Expression<Func<FooterGridDto, bool>>> GetPredicates()
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
