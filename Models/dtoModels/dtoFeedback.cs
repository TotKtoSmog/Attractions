namespace Attractions.Models.dtoModels
{
    public class dtoFeedback
    {
        public string NameSender { get; set; } = null!;
        public string FeedBackText { get; set; } = null!;

        public int Id_Sight { get; set; }
        public int Ball { get; set; }
    }
}
