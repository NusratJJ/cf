using P10.Models;
using System.ComponentModel.DataAnnotations;

namespace P10.ViewModel
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = null!;
        [Display(Name = "Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Dob { get; set; }

        public string MobileNo { get; set; } = null!;

        public bool IsEnrolled { get; set; }

        public string ImageUrl { get; set; } = null!;

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int CourseModuleId { get; set; }

        public string ModuleName { get; set; } = null!;

        public int Duration { get; set; }


        public IFormFile ProfileFile { get; set; }
        public IList<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public virtual IList<CourseModule> Modules { get; set; } = new List<CourseModule>();

    }
}
