namespace TutorialHell.Models
{
    public class Contact 
    {
        public string Id { get; set; } =  Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
