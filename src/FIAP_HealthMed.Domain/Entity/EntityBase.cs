using System.ComponentModel.DataAnnotations;

namespace FIAP_HealthMed.Domain.Entity
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}
