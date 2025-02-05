using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Forum_RP.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string TopicTitle { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public  ICollection<Post> Posts { get; set; }
    }
}
