namespace Employee_Mgmt_Back.DTOs
{
    public class SignupRequest
    {
        public string Role { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
