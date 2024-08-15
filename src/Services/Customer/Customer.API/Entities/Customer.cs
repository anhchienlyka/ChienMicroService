using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Entities
{
    public class Customer : EntityBase<int>
    {
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "varchar(150)")]
        public string EmailAddress { get; set; }
    }
}