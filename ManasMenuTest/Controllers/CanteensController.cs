using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManasMenuTest;
using ManasMenuTest.Data;

namespace ManasMenuTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanteensController : ControllerBase
    {
        private readonly ManasMenuContext _context;

        public CanteensController(ManasMenuContext context)
        {
            _context = context;
        }

        // GET: api/Canteens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetCanteen()
        {
          if (_context.Canteen == null)
          {
              return NotFound();
          }
            return await _context.Canteen.ToListAsync();
        }

        // GET: api/Canteens/GetDrinks
        [HttpGet("GetDrinks")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetCanteenDrinks()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.Type == "DRINKS").ToListAsync();
        }
        // GET: api/Canteens/GetPaP
        [HttpGet("GetPaP")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetPaP()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.Type == "PIZZA AND PIDES").ToListAsync();
        }
        [HttpGet("GetBakeryProducts")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetBakeryProducts()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.Type == "BAKERY PRODUCTS").ToListAsync();
        }
        [HttpGet("GetDesserts")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetDesserts()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.Type == "DESSERTS").ToListAsync();
        }

        [HttpGet("GetOtherFoods")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetOtherFoods()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.Type == "OTHER FOODS").ToListAsync();
        }


        // GET: api/Canteens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canteen>> GetCanteen(int id)
        {
          if (_context.Canteen == null)
          {
              return NotFound();
          }
            var canteen = await _context.Canteen.FindAsync(id);

            if (canteen == null)
            {
                return NotFound();
            }

            return canteen;
        }

        // PUT: api/Canteens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanteen(int id, Canteen canteen)
        {
            if (id != canteen.Id)
            {
                return BadRequest();
            }

            _context.Entry(canteen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CanteenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPut("FreeFood")]
        public async Task<IActionResult> FreeFood(int id, int amount)
        {
            var food = await _context.Canteen.FindAsync(id);
            if(food != null)
            {
                food.AmountForFree = amount;
                await _context.SaveChangesAsync();

                return Ok();
            }
            return NotFound();
        }

        // POST: api/Canteens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canteen>> PostCanteen(Canteen canteen)
        {
          if (_context.Canteen == null)
          {
              return Problem("Entity set 'ManasMenuContext.Canteen'  is null.");
          }
            _context.Canteen.Add(canteen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanteen", new { id = canteen.Id }, canteen);
        }

        // DELETE: api/Canteens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanteen(int id)
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            var canteen = await _context.Canteen.FindAsync(id);
            if (canteen == null)
            {
                return NotFound();
            }

            _context.Canteen.Remove(canteen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CanteenExists(int id)
        {
            return (_context.Canteen?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteFreeFood")]
        public async Task<IActionResult> DeleteFreeFood(int id)
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            var canteen = await _context.Canteen.FindAsync(id);
            if (canteen == null)
            {
                return NotFound();
            }
            try
            {
                canteen.AmountForFree = 0;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }
        [HttpGet("GetFreeFoods")]
        public async Task<ActionResult<IEnumerable<Canteen>>> GetFreeFoods()
        {
            if (_context.Canteen == null)
            {
                return NotFound();
            }
            return await _context.Canteen.Where(x => x.AmountForFree > 0).ToListAsync();
        }
    }
}
