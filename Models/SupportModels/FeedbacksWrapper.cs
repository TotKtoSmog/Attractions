using Attractions.Models.dboModels;

namespace Attractions.Models.SupportModels
{
    public class FeedbacksWrapper
    {
        string? Id { get; set; }
        List<Feedback>? Feedback { get; set; }
        public FeedbacksWrapper() { }
        public FeedbacksWrapper(List<Feedback>? f) 
        {
            Feedback = f;
        }
        public FeedbacksWrapper(List<Feedback>? f, string id)
        {
            Id = id;
            Feedback = f;
        }
    }
}
