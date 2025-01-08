namespace Attractions.Models.dtoModels
{
    public class dtoRegistrationUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
        public int Age { get; set; }
    }
}
