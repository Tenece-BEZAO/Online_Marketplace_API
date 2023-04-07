using Online_Marketplace.DAL.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Marketplace.DAL.Entities
{
    public class Wallet
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(10), MinLength(10)]
        public string WalletNo { get; set; }

        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }

        [Column(TypeName = "decimal(38,2)")]
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public virtual Seller Seller { get; set; }
    }
}
