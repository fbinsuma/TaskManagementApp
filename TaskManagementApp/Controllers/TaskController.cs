using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }
        // Task
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.TaskItems.ToListAsync();
            return View(tasks);
        }

        // Task/Create
        public IActionResult Create()
        {
            return View();
        }

        //Post: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate,IsCompleted")] TaskItem taskItem)
        {
            if (ModelState.IsValid) 
            {
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(taskItem);
        }


        //Get:Task/Edit
        public async Task<IActionResult> Edit(Guid id) 
        {
            if (id == null) { return NotFound("Task Not Found"); }

            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null) { return NotFound("Task Not Found"); }
            
            return View(taskItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,DueDate,IsCompleted")] TaskItem taskItem)
        {
            if (id != taskItem.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!TaskItemExists(taskItem.Id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskItem);
        }
        public bool TaskItemExists(Guid id) 
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
