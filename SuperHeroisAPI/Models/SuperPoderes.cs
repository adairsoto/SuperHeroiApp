using System.ComponentModel.DataAnnotations;

namespace SuperHeroisAPI.Models
{
    public class SuperPoderes
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string SuperPoder { get; set; } = string.Empty;
        [StringLength(250)]
        public string? Descricao { get; set; }
        public ICollection<HeroiSuperPoderes> HeroiSuperPoderes { get; set; }
    }
}
