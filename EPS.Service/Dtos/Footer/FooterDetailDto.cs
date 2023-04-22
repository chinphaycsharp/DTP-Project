using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.Footer
{
    public class FooterDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? ParentId { get; set; }
        public int Status { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public string Position { get; set; }
        public int Order { get; set; }
        public string Updated_dateStr { get; set; }
        public string Created_dateStr { get; set; }
    }
}
