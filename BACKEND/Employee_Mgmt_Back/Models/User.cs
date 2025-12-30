namespace Employee_Mgmt_Back.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        //public string PhoneNo { get; set; } = "";
        //public string Designation { get; set; } = "";
        //public string Department { get; set; } = "";
        //public decimal Salary { get; set; }
        //public DateTime DateOfJoining { get; set; }
        //public bool IsActive { get; set; }
        //public string? Address { get; set; }
        public string Role { get; set; } = "";  //Admin or SubAdmin
    }
}
