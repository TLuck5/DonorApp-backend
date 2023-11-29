using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VerifyEmailPass.Data;
using VerifyEmailPass.Models;
using Mapster;
using Service_logic_layer.Models.model;
using Service_logic_layer.Models.requestmodel;
using Service_logic_layer.Models.responsemodel;

namespace Service_logic_layer.Services.userServices
{
    public class UserServices : IUserServices
    {
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public UserServices(DataContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<Response> Forgotpassword(string email)
        {
            Response response = new Response();
            var user = await _db.Users.FirstOrDefaultAsync(y => y.Email == email);
            if (user == null)
            {
                response.statuscode = 400;
                response.message = "Not found the user";
                return response;
            }
            user.PasswordResetToken = generateToken();
            user.ResetTokenExpires = DateTime.Now.AddHours(1);
            await _db.SaveChangesAsync();

            var ForgotPassModel = user.Adapt<ForgotPassModel>();
            response.statuscode = 200;
            response.message = "Reset Token has been generated successfully";
            response.ForgotPassData = ForgotPassModel;
            return response;

        }

        public async Task<Response> Login(UserLoginRequest request)
        {
            Response response = new Response();

            if (request == null)
            {
                response.statuscode = 400;
                response.message = "request is null";
                return response;
            }
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
            {
                response.statuscode = 400;
                response.message = "user not found";
                return response;
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.statuscode = 400;
                response.message = "Wrong password";
                return response;
            }
            if (user.VerifiedAt == null)
            {
                response.statuscode = 400;
                response.message = "Not verified";
                return response;
            }
            var jwtToken = CreateJwtToken(user);
            loginModel lm = new loginModel()
            {
                Email = user.Email,
                JwtToken = jwtToken,
                VerifiedAt = user.VerifiedAt
            };

            var loginModel = user.Adapt<loginModel>();
            response.statuscode = 200;
            response.message = "Login Successful";
            response.LoginModel = lm;
            return response;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public async Task<Response> Register(UserRegisterRequest request)
        {
            Response response = new Response();
            if (request == null)
            {
                response.statuscode = 400;
                response.message = "Request is null";
                return response;
            }

            if (await _db.Users.AnyAsync(x => x.Email.Equals(request.Email)))
            {
                response.statuscode = 400;
                response.message = "Already registered";
                return response;
            }

            createPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = generateToken()
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            var userModel = user.Adapt<UserModel>();
            response.statuscode = 200;
            response.User = userModel;
            response.message = "User Created Successfully";
            return response;
        }

        private string generateToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        private void createPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<string> ResetPassword(ResetPasswordRequest request)
        {
            if (request == null)
            {
                return "request is null";
            }
            var user = await _db.Users.FirstOrDefaultAsync(x => x.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return "Invalid Token";
            }
            createPassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;
            await _db.SaveChangesAsync();

            return "Password Reset successfully ";
        }

        public async Task<string> Verify(string token)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.VerificationToken == token);
            if (user == null)
            {
                return "Invalid Token";
            }
            user.VerifiedAt = DateTime.Now;
            await _db.SaveChangesAsync();
            return "User Verified";
        }

        private string CreateJwtToken(User users)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, users.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var Token = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: creds
                                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return jwt;
        }
    }
}
