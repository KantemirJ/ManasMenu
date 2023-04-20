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
    public class OneDayMenusController : ControllerBase
    {
        private readonly ManasMenuContext _context;

        public OneDayMenusController(ManasMenuContext context)
        {
            _context = context;
        }

        // GET: api/OneDayMenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OneDayMenuDto>>> GetOneDayMenu()
        {
            if (_context.OneDayMenu == null)
            {
                return NotFound();
            }

            List<OneDayMenu> oneDayMenuList = await _context.OneDayMenu.ToListAsync();
            List<OneDayMenuDto> oneDayMenuDtoList = new List<OneDayMenuDto>();
            OneDayMenuDto oneDayMenuDto = new OneDayMenuDto();
            Menu menu = new Menu();
            Menu menu1 = new Menu();
            Menu menu2 = new Menu();
            Menu menu3 = new Menu();
            List<Menu> menuList = new List<Menu>();

            foreach (var oneDayMenu in oneDayMenuList)
            {
                oneDayMenuDto.Id = oneDayMenu.Id;
                oneDayMenuDto.Date = oneDayMenu.Date;
                menu = await _context.Menu.FindAsync(oneDayMenu.SoupId);
                menuList.Add(menu);
                menu1 = await _context.Menu.FindAsync(oneDayMenu.WithoutMeetId);
                menuList.Add(menu1);
                menu2 = await _context.Menu.FindAsync(oneDayMenu.WithMeetId);
                menuList.Add(menu2);
                menu3 = await _context.Menu.FindAsync(oneDayMenu.DessertId);
                menuList.Add(menu3);

                oneDayMenuDto.Menus = menuList;
                
                oneDayMenuDtoList.Add(oneDayMenuDto);
            }

            return oneDayMenuDtoList;
        }

        // GET: api/OneDayMenus/12.12.2023
        [HttpGet("{date}")]
        public async Task<ActionResult<List<Menu>>> GetOneDayMenu(string date)
        {
          if (_context.OneDayMenu == null)
          {
              return NotFound();
          }
            var oneDayMenu = _context.OneDayMenu.Where(b => b.Date == date)
                    .FirstOrDefault();
            var Menu1 = await _context.Menu.FindAsync(oneDayMenu.SoupId);
            var Menu2 = await _context.Menu.FindAsync(oneDayMenu.WithMeetId);
            var Menu3 = await _context.Menu.FindAsync(oneDayMenu.WithoutMeetId);
            var Menu4 = await _context.Menu.FindAsync(oneDayMenu.DessertId);
            List<Menu> menuList = new()
            {
                Menu1,
                Menu2,
                Menu3,
                Menu4
            };

            if (oneDayMenu == null)
            {
                return NotFound();
            }

            return menuList;
        }

        // PUT: api/OneDayMenus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOneDayMenu(int id, OneDayMenu oneDayMenu)
        {
            if (id != oneDayMenu.Id)
            {
                return BadRequest();
            }

            _context.Entry(oneDayMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OneDayMenuExists(id))
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

        // POST: api/OneDayMenus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OneDayMenu>> PostOneDayMenu(OneDayMenu oneDayMenu)
        {
          if (_context.OneDayMenu == null)
          {
              return Problem("Entity set 'ManasMenuContext.OneDayMenu'  is null.");
          }
            _context.OneDayMenu.Add(oneDayMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOneDayMenu", new { id = oneDayMenu.Id }, oneDayMenu);
        }

        // DELETE: api/OneDayMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneDayMenu(int id)
        {
            if (_context.OneDayMenu == null)
            {
                return NotFound();
            }
            var oneDayMenu = await _context.OneDayMenu.FindAsync(id);
            if (oneDayMenu == null)
            {
                return NotFound();
            }

            _context.OneDayMenu.Remove(oneDayMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OneDayMenuExists(int id)
        {
            return (_context.OneDayMenu?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
