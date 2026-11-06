using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly AppDb _db;
    public ItemsController(AppDb db) => _db = db;

    [HttpGet]
    public async Task<List<Item>> Get() => await _db.Items.ToListAsync();

    [HttpPost]
    public async Task<IActionResult> Post(Item item)
    {
        if (await _db.Items.AnyAsync(i => i.ItemId == item.ItemId)) return Conflict();
        _db.Items.Add(item);
        await _db.SaveChangesAsync();
        return Ok(item);
    }
}