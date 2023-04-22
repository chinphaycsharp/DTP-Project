using EPS.Service.Dtos.Menu;
using EPS.Utils.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EPS.Service.Dtos.New
{
    public class NewGridPagingDto : PagingParams<NewGridDto>
    {
        public string FilterText { get; set; }
        public string ParentId { get; set; } = null;

        public override List<Expression<Func<NewGridDto, bool>>> GetPredicates()
        {
            var predicates = base.GetPredicates();

            if (!string.IsNullOrEmpty(FilterText))
            {
                predicates.Add(x => x.Title.Contains(FilterText));
            }
            return predicates;
        }
    }
}
