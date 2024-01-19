using SuperHeroisAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroisAPI.Dtos
{
    public class HeroiDto
    {
        public int Id { get; set; }
        [StringLength(120)]
        public string Nome { get; set; } = string.Empty;
        [StringLength(120)]
        public string NomeHeroi { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public double Altura { get; set; }
        public double Peso { get; set; }
        public List<int> SuperPoderesIds { get; set;}
    }
}
