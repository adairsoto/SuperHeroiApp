using System.ComponentModel.DataAnnotations;

namespace SuperHeroisAPI.Models
{
    public class Heroi
    {
        public int Id { get; set; }
        [StringLength(120)]
        public string Nome { get; set; } = string.Empty;
        [StringLength(120)]
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public ICollection<HeroiSuperPoderes> HeroiSuperPoderes { get; set; }
    }
}
