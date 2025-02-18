using CoreWeb.Models;
using CoreWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly WeB1dbContext _db;
        private readonly IWebHostEnvironment _evn;

        public StudentsController(WeB1dbContext db, IWebHostEnvironment evn)
        {
            _db = db;
            _evn = evn;
        }

        public IActionResult Index()
        {
            var s=_db.Students.Include(s=>s.Course).Include(s=>s.CourseModules).ToList();
            return View(s);
        }
        public IActionResult Delete(int id)
        {
            var s = _db.Students.Find(id);
            _db.Students.Remove(s);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        { 
            StudentViewModels s=new StudentViewModels();
            s.Courses=_db.Courses.ToList();
            return View(s);
        
        }
        [HttpPost]
        public IActionResult Create(StudentViewModels m)
        {
            Student s=new Student();
            string imgFile = null;
            if (m.ProfileFile != null)
            {
                imgFile=Guid.NewGuid().ToString()+Path.GetExtension(m.ProfileFile.FileName);
                var filePath = Path.Combine(_evn.WebRootPath, "images");
                var file = Path.Combine(filePath, imgFile);
                using (var Fs = new FileStream(file, FileMode.Create))
                {
                    m.ProfileFile.CopyTo(Fs);
                }
            }
            s.ImageUrl = imgFile;
            s.StudentName=m.StudentName;
            s.Dob=m.Dob;
            s.CourseFee=m.CourseFee;
            s.IsEnrolled=m.IsEnrolled;
            s.CourseId=m.CourseId;
            s.CourseModules=m.Modules;
            _db.Students.Add(s);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
