using CoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWeb.ViewComponents
{
    
    public class HeadCountViewComponent:ViewComponent
    {
        private readonly WeB1dbContext _db;

        public HeadCountViewComponent(WeB1dbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult>InvokeAsync(int courseId)
        {
            var courseCounts = await _db.Students
                .Include(s => s.Course).GroupBy(s => new { s.Course.CourseId, s.Course.CourseName }).
                Select(g => new CourseHeadCount
                {
                    CourseId = g.Key.CourseId,
                    CourseName = g.Key.CourseName,
                    Count = g.Count()
                }).ToListAsync();
            return View(courseCounts);
        }
    }
}
