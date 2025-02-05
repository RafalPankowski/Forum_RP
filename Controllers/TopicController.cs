using Forum_RP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum_RP.Controllers
{
    public class TopicController : Controller
    {
        private readonly ForumContext _context;

        public TopicController(ForumContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            var topics = _context.Topics.ToList();
            return View(topics);
        }

        // Wyświetlanie formularza tworzenia tematu
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            if (ViewBag.CurrentUser == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        // Przetwarzanie danych formularza i zapisywanie tematu w bazie danych
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTopic(Topic topic)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            if (ViewBag.CurrentUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (!ModelState.IsValid)
            {
                topic.Date = DateTime.Now;
                topic.User = ViewBag.CurrentUser;
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", topic); // Zwraca formularz z błędami walidacji
        }

        public IActionResult Details(int id)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");

            var topic = _context.Topics
                .Include(t => t.Posts)
                .FirstOrDefault(t => t.Id == id);

            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(int id, string postContent)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            if (ViewBag.CurrentUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            var topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            var post = new Post
            {
                TopicId = id,
                PostContent = postContent,
                User = ViewBag.CurrentUser,
                Date = DateTime.Now
            };

            if (postContent != null)
            {
                _context.Posts.Add(post);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}
