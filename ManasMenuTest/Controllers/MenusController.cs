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
    public class MenusController : ControllerBase
    {
        private readonly ManasMenuContext _context;

        public MenusController(ManasMenuContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
          if (_context.Menu == null)
          {
              return NotFound();
          }
            return await _context.Menu.ToListAsync();
        }

        [HttpGet("GetSoups")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetSoups()
        {
            if (_context.Menu == null)
            {
                return NotFound();
            }
            return await _context.Menu.Where(x => x.Type == "SOUP").ToListAsync();
        }
        [HttpGet("GetWithMeat")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetWithMeat()
        {
            if (_context.Menu == null)
            {
                return NotFound();
            }
            return await _context.Menu.Where(x => x.Type == "WITHMEAT").ToListAsync();
        }
        [HttpGet("GetWithoutMeat")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetWithoutMeat()
        {
            if (_context.Menu == null)
            {
                return NotFound();
            }
            return await _context.Menu.Where(x => x.Type == "WITHOUTMEAT").ToListAsync();
        }
        [HttpGet("GetDesserts")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetDesserts()
        {
            if (_context.Menu == null)
            {
                return NotFound();
            }
            return await _context.Menu.Where(x => x.Type == "DESSERT").ToListAsync();
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
          if (_context.Menu == null)
          {
              return NotFound();
          }
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
          if (_context.Menu == null)
          {
              return Problem("Entity set 'ManasMenuContext.Menu'  is null.");
          }
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            if (_context.Menu == null)
            {
                return NotFound();
            }
            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return (_context.Menu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
