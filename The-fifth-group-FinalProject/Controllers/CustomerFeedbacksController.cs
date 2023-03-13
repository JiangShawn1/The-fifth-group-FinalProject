using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalProject.Models;
using Microsoft.AspNetCore.Http;
using 專題.Models.Services;

namespace The_fifth_group_FinalProject.Controllers
{
    public class CustomerFeedbacksController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public CustomerFeedbacksController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: CustomerFeedbacks
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                var theFifthGroupOfTopicsContext = _context.CustomerFeedbacks.Include(c => c.QuestionType);
                return View(await theFifthGroupOfTopicsContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        // GET: CustomerFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                if (id == null || _context.CustomerFeedbacks == null)
                {
                    return NotFound();
                }

                var customerFeedback = await _context.CustomerFeedbacks
                    .Include(c => c.QuestionType)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (customerFeedback == null)
                {
                    return NotFound();
                }

                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Create");
            }

        }
        // GET: CustomerFeedbacks/Create
        public IActionResult Create()
        {
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "QuestionType1");
            return View();
        }

        // POST: CustomerFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeedbackContent,CustomerName,Email,QuestionTypeId,Status,CreateTime")] CustomerFeedback customerFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["QuestionTypeId"] = new SelectList(_context.QuestionTypes, "Id", "QuestionType1", customerFeedback.QuestionTypeId);
            return View(customerFeedback);
        }

        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            CustomerFeedback customerFeedback = _context.CustomerFeedbacks.Find(id);
            if (customerFeedback == null)
            {
                return NotFound();
            }
            ViewBag.email = customerFeedback.Email;
            ViewBag.feedbackcontent = customerFeedback.FeedbackContent;
            ViewBag.createtime = customerFeedback.CreateTime;
            ViewBag.customername = customerFeedback.CustomerName;
            return View(customerFeedback);
        }
        public static string _emailTo;
        public static string _subject;
        public static string _body;

        [HttpPost]
        public ActionResult SentEmail()
        {
            //         string _emailTo = post["emailTo"];
            //string _subject = post["emailSubject"];
            //string _body = post["emailBody"];

            string _emailTo = Request.Form["emailTo"];
            string _subject = Request.Form["emailSubject"];
            string _body = Request.Form["emailBody"];

            //_emailTo = Request.Form["emailTo"];
            //_subject = Request.Form["emailSubject"];
            //_body = Request.Form["body"];


            var emailService = new EmailService();
            if (_body != null && _subject != null && _emailTo != null && _body != "" && _subject != "" && _emailTo != "")
            {
                emailService.SendEmail(_emailTo, _subject, _body);
                return View("SentEmail");
            }
            else
            {
                return View("FailEmail");
            }
        }

    }
}
