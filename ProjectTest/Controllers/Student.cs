using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectTest.Models;

namespace ProjectTest.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> GetRegisteredStudents() =>
            JsonConvert.DeserializeObject<List<Student>>(HttpContext.Session.GetString("RegisteredStudents") ?? "[]");
        private void SaveRegisteredStudents(List<Student> students) =>
            HttpContext.Session.SetString("RegisteredStudents", JsonConvert.SerializeObject(students));

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult ShowKQ(Student student)
        {
            var students = GetRegisteredStudents();
            students.Add(student);
            SaveRegisteredStudents(students);
            return RedirectToAction("ShowKQ", new { student.MSSV, student.HoTen, student.ChuyenNganh });
        }
        public IActionResult ShowKQ(string MSSV, string HoTen, string ChuyenNganh)
        {
            var students = GetRegisteredStudents();
            var count = students.Count(s => s.ChuyenNganh == ChuyenNganh);
            ViewBag.MSSV = MSSV;
            ViewBag.HoTen = HoTen;
            ViewBag.ChuyenNganh = ChuyenNganh;
            ViewBag.SameMajorCount = count - 1;
            return View();
        }
    }
}
