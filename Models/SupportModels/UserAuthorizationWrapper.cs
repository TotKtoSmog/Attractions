using Attractions.Models.dboModels;
namespace Attractions.Models.SupportModels
{
    public class UserAuthorizationWrapper
    {
        public bool IsError { get; set; }
        public bool IsWarning { get; set; }
        public string Message {  get; set; } = string.Empty;
        public User User { get; set; }
        public UserAuthorizationWrapper()
        {
            User = new User();
            SetWarning("Пустой пользователь!");
        }
        public UserAuthorizationWrapper(User user) 
        {
            User = user;
            IsError = false;
            Message = "";
        }
        public void SetMessage(string message) 
        {
            IsError = false;
            IsWarning = false;
            Message = message; 
        } 
        public void SetErrorMessage(string message)
        {
            IsWarning = false;
            IsError = true;
            Message = message;
        }
        public void SetWarning(string message)
        {
            IsError = false;
            IsWarning = true;
            Message = message;
        }
    }
}
