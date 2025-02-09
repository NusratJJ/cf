using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P10.Models;
using P10.ViewModel;

namespace P10.Controllers
{
    public class StudentsController : Controller
    {
        private readonly P10DbContext _db;
        private readonly IWebHostEnvironment _env;

        public StudentsController(P10DbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Index()
        {
            var o = _db.Students.Include(s => s.Course).Include(s => s.CourseModules).ToList();
            return View(o);
        }
        public IActionResult Delete(int id)
        {
            var o = _db.Students.Find(id);
            _db.Students.Remove(o);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            StudentViewModel o = new StudentViewModel();
            o.Courses = _db.Courses.ToList();
            return View(o);
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel m)
        {
            Student s = new Student();
            string imgFile = null;
            if (m.ProfileFile != null)
            {
                imgFile = Guid.NewGuid().ToString() + Path.GetExtension(m.ProfileFile.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images");
                var file = Path.Combine(filePath, imgFile);
                using (var fileStram = new FileStream(file, FileMode.Create))
                {
                    m.ProfileFile.CopyTo(fileStram);
                }
            }
            s.ImageUrl = imgFile;
            s.StudentName = m.StudentName;
            s.Dob = m.Dob;
            s.MobileNo = m.MobileNo;
            s.IsEnrolled = m.IsEnrolled;
            s.CourseId = m.CourseId;
            s.CourseModules = m.Modules;
            _db.Students.Add(s);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
