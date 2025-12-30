using Employee_Mgmt_Back.Data;
using Employee_Mgmt_Back.DTOs;
using Employee_Mgmt_Back.Models;

namespace Employee_Mgmt_Back.Services
{
    public class AuthService
    {
        private readonly EmpDbContext _db;
        private readonly JwtService _jwt;


        public AuthService(EmpDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt; 
        }

        public LoginResponse? Login(LoginRequest request)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null) { return null; }


            var token = _jwt.GenerateToken(user.Email, user.Role);
            return new LoginResponse
            {
                Token = token,
                Role = user.Role,
                Fullname = user.Fullname
            };
        }

        public bool Signup(SignupRequest req)
        {
            if (_db.Users.Any(u => u.Email == req.Email))
                return false;

            var newUser = new User
            {
             
                Role = req.Role,
                Email = req.Email,
                Password = req.Password,
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
            return true;

       
         } 
    }
}
