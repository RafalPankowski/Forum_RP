namespace Forum_RP.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string PostContent { get; set; }
        public int TopicId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public Topic Topic { get; set; }
    }
}
