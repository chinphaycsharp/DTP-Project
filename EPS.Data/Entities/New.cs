using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class New
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Column(TypeName = "text")]
        [MaxLength]
        public string Content { get; set; }
        [Required]
        [StringLength(255)]
        public string Url { get; set; }
        public string? Image { get; set; }
        public int Status { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public int Order { get; set; }
    }
}
