using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroisAPI.Data;
using SuperHeroisAPI.Dtos;
using SuperHeroisAPI.Models;

namespace SuperHeroisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : Controller
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SuperHeroiController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Heroi>>> GetSuperHerois()
        {
            var herois = await _context.Herois.ToListAsync();

            if (herois.Count == 0)
                return NotFound("A lista de heróis está vazia!");

            var heroisDto = _mapper.Map<List<HeroiDto>>(herois);
            return Ok(heroisDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Heroi>> GetHeroi(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
            {
                return NotFound("Herói não encontrado!");
            }
            return heroi;
        }

        [HttpGet("superPoderes")]
        public async Task<ActionResult<List<SuperPoderes>>> GetSuperPoderes()
        {
            var superPoderes = await _context.SuperPoderes.ToListAsync();

            if (superPoderes.Count == 0)
                return NotFound("A lista de super poderes está vazia!");

            return Ok(superPoderes);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSuperHero(HeroiDto heroiDto)
        {
            var checkNomeHeroi = _context.Herois.Where(hero => hero.NomeHeroi == heroiDto.NomeHeroi).FirstOrDefault();

            if (checkNomeHeroi != null)
                return BadRequest("Esse herói já está cadastrado!");

            var heroi = _mapper.Map<Heroi>(heroiDto);
            _context.Herois.Add(heroi);

            await _context.SaveChangesAsync();

            var superHeroisIds = heroiDto.SuperPoderesIds;

            if (superHeroisIds != null)
            {
                foreach (var superHeroiId in superHeroisIds)
                {
                    HeroiSuperPoderes heroiSuperPoder = new HeroiSuperPoderes();

                    heroiSuperPoder.HeroiId = heroi.Id;
                    heroiSuperPoder.SuperPoderesId = superHeroiId;

                    _context.HeroiSuperPoderes.Add(heroiSuperPoder);

                    await _context.SaveChangesAsync();
                }
            }

            return Ok("Herói cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSuperHero(int id, HeroiDto heroiDto)
        {
            if (id != heroiDto.Id)
                return NotFound("Herói não encontrado.");

            var checkNomeHeroi = _context.Herois.Where(hero => hero.NomeHeroi == heroiDto.NomeHeroi && hero.Id != heroiDto.Id).FirstOrDefault();

            if (checkNomeHeroi != null)
                return BadRequest("Esse herói já está cadastrado!");

            var heroi = _mapper.Map<Heroi>(heroiDto);

            _context.Entry(heroi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Herói editado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSuperHero(int id)
        {
            var heroi = await _context.Herois.FindAsync(id);
            if (heroi == null)
                return NotFound("Herói não encontrado.");

            _context.Herois.Remove(heroi);

            await _context.SaveChangesAsync();

            return Ok("Herói removido com sucesso!");
        }
    }
}
