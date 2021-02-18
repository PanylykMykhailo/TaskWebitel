
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebitel.Model
{
    public class Product
    {
        [Key]
        [JsonIgnore]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(50),NotNull]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public  List<Order> Orders { get; set; }

    }
}
