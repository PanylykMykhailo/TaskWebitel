
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TasksWebitel.Model
{
    public class Order
    {
        [Key]
        [JsonIgnore]
        public int OrderId { get; set; }
        [Required]
        [MaxLength(50),NotNull]
        public string Number { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Amount { get; set; }
        public  List<Product> Products { get; set; }
    }
}
