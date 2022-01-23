namespace BlazorStack.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Points { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool AcceptedTermsAgreements { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime RegisteredOn { get; set; } = DateTime.Now;
    }
}
