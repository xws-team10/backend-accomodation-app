namespace account_service.Model.DTO
{
    public class RegisterDto : LoginDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }

        public string UserRole { get; set; }

    }
}
