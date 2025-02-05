using Forum_RP.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_RP.Controllers
{
    public class PostController : Controller
    {
        private readonly ForumContext _context;

        public PostController(ForumContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int topicId, string postContent)
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    TopicId = topicId,
                    PostContent = postContent,
                    User = HttpContext.Session.GetString("UserName"),
                    Date = DateTime.Now
                };
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Details", "Topic", new { id = topicId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null || post.User != HttpContext.Session.GetString("UserName"))
            {
                return Unauthorized();
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            var existingPost = _context.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost == null || existingPost.User != HttpContext.Session.GetString("UserName"))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                existingPost.PostContent = post.PostContent;
                existingPost.Date = DateTime.Now; // Update the date to reflect the edit

                _context.SaveChanges();
                return RedirectToAction("Details", "Topic", new { id = post.TopicId });
            }

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int postId)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null || post.User != HttpContext.Session.GetString("UserName"))
            {
                return Unauthorized();
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();

            return RedirectToAction("Details", "Topic", new { id = post.TopicId });
        }
    }
}
