using System;
using System.ComponentModel.DataAnnotations;

namespace EPS.Data.Entities
{
    public partial class Menu
    {
        public string Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Url { get; set; }
        public string? ParentId { get; set; }
        public int Status { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Updated_date { get; set; }
        public string Position { get; set; }
        public int Order { get; set; }
    }
}
