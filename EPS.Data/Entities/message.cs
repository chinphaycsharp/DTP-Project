using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPS.Data.Entities
{
    public partial class message
    {
        public int Id { get; set; }
        public string content { get; set; }
        public string username{ get; set; }
        public int user_id { get; set; }    
        public DateTime createdDate { get; set; }
        public int? isAdmin { get; set; }
        public int? clientId { get; set; }
    }
}
