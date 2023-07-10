﻿
using System.ComponentModel.DataAnnotations;


namespace P010Store.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} alanı boş geçilemez"), StringLength(50), Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklaması")]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(150)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"),ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now; 
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

       public virtual ICollection<Product>? Products { get; set; }
    }
}
