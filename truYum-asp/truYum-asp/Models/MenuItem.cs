using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace truYum_asp.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name ="Free Delivery")]
        public Boolean freeDelivery { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name="Date of Launch")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime dateOfLaunch { get; set; }
        public Boolean Active { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public virtual Category Category { get; set; }
    }
}