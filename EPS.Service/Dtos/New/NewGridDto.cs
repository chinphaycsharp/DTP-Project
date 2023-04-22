using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Service.Dtos.New
{
    public class NewGridDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string? Image { get; set; }
        public int Status { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public int Order { get; set; }
    }
}
