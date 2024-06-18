using Attractions.Models.dtoModels;

namespace Attractions.Models.dboModels
{
    public class Feedback: dtoFeedback
    {
        public int Id { get; set; }
        public bool IsAccepted { get; set; }
        public int? Id_User { get; set; } 
    }
}
