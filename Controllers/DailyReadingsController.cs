using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAO_Backend.Models;

namespace TAO_Backend.Controllers
{
    public class DailyReadingsController: BaseApiController
    {
        private readonly DBContext _context;

        public DailyReadingsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<DailyReading>>> GetDailyReadings()
        {
            return await _context.DailyReadings.ToListAsync();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyReading>> GetDailyReading(int id)
        {
            return await _context.DailyReadings.FindAsync(id);
        }
    }
}
