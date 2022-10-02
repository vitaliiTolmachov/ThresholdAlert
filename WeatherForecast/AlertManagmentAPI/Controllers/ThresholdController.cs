using AlertManagmentAPI.Mappers;
using AlertManagmentAPI.Models;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbContext = DataAccess.DbContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlertManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThresholdController : ControllerBase
    {
        private readonly DbContext _dbContext;

        public ThresholdController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<ThresholdController>
        [HttpGet]
        public Task<ThresholdResponse[]> GetAsync()
        {
            return _dbContext.Thresholds.Select(x => x.ToResponse())
                .ToArrayAsync();
        }

        // GET api/<ThresholdController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var threshold = await _dbContext.Thresholds.FindAsync(id);

            if (threshold == null)
                return NotFound();

            return Ok(threshold.ToResponse());
        }

        // POST api/<ThresholdController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ThresholdRequest request)
        {
            var entity = new Threshold
            {
                HostName = request.HostName,
                MaxCalls = request.MaxCalls,
                NotificationLevel = request.NotificationLevel,
                UserIdId = request.UserIdId,
            };

            _dbContext.Thresholds.Add(entity);
            await _dbContext.SaveChangesAsync();
            return Ok(entity.ToResponse());
        }

        // PUT api/<ThresholdController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ThresholdUpdateRequest request)
        {
            var threshold = await _dbContext.Thresholds.FirstOrDefaultAsync(x => x.ThresholdId == id);
            
            if (threshold == null)
                return NotFound();

            threshold.NotificationLevel = request.NotificationLevel;
            threshold.HostName =  request.HostName;
            threshold.MaxCalls = request.MaxCalls;
            threshold.UserIdId = request.UserIdId;
            threshold.IsAlertSent = request.IsAlertSent;

            await _dbContext.SaveChangesAsync();
            return Ok(threshold.ToResponse());
        }

        // DELETE api/<ThresholdController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var threshold = await _dbContext.Thresholds.FirstOrDefaultAsync(x => x.ThresholdId == id);

            if (threshold == null)
                return NotFound();

            _dbContext.Thresholds.Remove(threshold);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
