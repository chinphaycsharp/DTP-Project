using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Menu
{
    public class MenuUpadateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? ParentId { get; set; }
        public int Status { get; set; }
        public DateTime Updated_date { get; set; }
        public string Position { get; set; }
        public int Order { get; set; }
    }
}
