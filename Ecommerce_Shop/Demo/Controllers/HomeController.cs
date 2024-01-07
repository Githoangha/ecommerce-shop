using Application.Students;
using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentService _studentService;
        public HomeController(ILogger<HomeController> logger,IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        //public IActionResult Index()
        //{
        //    var students = _studentService.GetStudents();

        //    return View(students);
        //}
        public async Task<IActionResult> Index()
        {
            var students =await _studentService.GetStudentsAsync();
            return View(students);
        }

        #region Add Student
        public IActionResult CreateStudent()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveCreate(CreateStudentRequest request)
        {
            try
            {
                await _studentService.AddStudent(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("[Home/Save]", ex);
                return RedirectToAction("Error");
            }
            finally
            {

            }
        }
        #endregion

        #region Update student
        public async Task<IActionResult> UpdateStudent(Guid id)
        {
            try
            {
                var student = await _studentService.GetStudentsByIdAsync(id);
                if (student != null)
                {
                    return View(student);
                }
                else
                {
                    return Error();
                }

            }
            catch(Exception ex)
            {
                _logger.LogError("[Home/Save]", ex);
                return RedirectToAction("Error");
            }

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveUpdate(UpdateStudentRequest request)
        {
            try
            {
                await _studentService.UpdateStudent(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("[Home/Save]", ex);
                return RedirectToAction("Error");
            }
            finally
            {

            }
        }
        #endregion

        #region Delete Student

        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
            try
            {
                await _studentService.DeleteStudent(Id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError("[Home/Save]", ex);
                return RedirectToAction("Error");
            }
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
