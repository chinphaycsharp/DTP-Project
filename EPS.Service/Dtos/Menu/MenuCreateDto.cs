using System;
using System.Collections.Generic;
using System.Text;

namespace EPS.Service.Dtos.Menu
{
    public class MenuCreateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? ParentId { get; set; }
        public int Status { get; set; }
        public DateTime Created_date { get; set; }
        public string Position { get; set; }
        public int Order { get; set; }
    }
}
