namespace Employee_Mgmt_Back.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
        public string Fullname { get; set; } = "";
    }
}
