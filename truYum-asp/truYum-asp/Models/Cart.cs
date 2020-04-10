using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace truYum_asp.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int menuItemId { get; set; }
        [ForeignKey("menuItemId")]
        public virtual MenuItem MenuItem { get; set; }
    }
}