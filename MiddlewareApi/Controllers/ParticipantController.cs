using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiddlewareApi.Data;
using MiddlewareApi.Dtos.Participant;
using MiddlewareApi.Models;

namespace MiddlewareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ParticipantController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var participants = await _appDbContext.Participants.ToListAsync();

            if (participants == null)
            {
                return NotFound();
            }

            var lstParticipants = new List<ReadParticipantDto>();

            foreach (var participant in participants)
            {
                lstParticipants.Add(new ReadParticipantDto()
                {
                    Id = participant.Id,
                    Name = participant.Name,
                    LastName = participant.LastName,
                    Birth = participant.Birth
                });
            }

            return Ok(lstParticipants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var participantFound = await _appDbContext.Participants.FirstOrDefaultAsync(p => p.Id == id);

            if (participantFound == null)
            {
                return NotFound();
            }

            var participant = new ReadParticipantDto()
            {
                Id = participantFound.Id,
                Name = participantFound.Name,
                LastName = participantFound.LastName,
                Birth = participantFound.Birth
            };

            return Ok(participant);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateParticipantDto participantDto)
        {
            var participant = new Participant()
            {
                Name = participantDto.Name,
                LastName = participantDto.LastName,
                Birth = participantDto.Birth
            };

            await _appDbContext.Participants.AddAsync(participant);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = participant.Id }, participant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateParticipantDto participantDto)
        {
            var participantFound = await _appDbContext.Participants.FirstOrDefaultAsync(x => x.Id == id);

            if (participantFound == null)
            {
                return NotFound();
            }

            participantFound.Name = participantDto.Name;
            participantFound.LastName = participantDto.LastName;
            participantFound.Birth = participantDto.Birth;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var participant = await _appDbContext.Participants.FirstOrDefaultAsync(x => x.Id == id);

            if (participant == null)
            {
                return NotFound();
            }

            _appDbContext.Remove(participant);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
