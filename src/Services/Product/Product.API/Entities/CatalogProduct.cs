using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class CatalogProduct : EntityAuditBase<long>
    {
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string No { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Summanry { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}