using Batsamayi.Shared.BL.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BookXChangeDB.Models
{
    public class Role : IDbEntity
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
