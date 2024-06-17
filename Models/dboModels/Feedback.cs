namespace Attractions.Models.dboModels
{
    public class Feedback
    {
        public int Id { get; set; }
        public string NameSender { get; set; } = null!;
        public string FeedBackText { get; set; } = null!;
        public int Ball { get; set; }
        public bool IsAccepted { get; set; }
        public int Id_Sight { get; set; }
        public int? Id_User { get; set; } 
    }
}
