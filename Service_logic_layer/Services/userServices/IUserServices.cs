using Service_logic_layer.Models.requestmodel;
using Service_logic_layer.Models.responsemodel;

namespace Service_logic_layer.Services.userServices
{
    public interface IUserServices
    {
        public Task<Response> Register(UserRegisterRequest request);
        public Task<Response> Login(UserLoginRequest request);
        public Task<string> Verify(string token);
        public Task<Response> Forgotpassword(string email);
        public Task<string> ResetPassword(ResetPasswordRequest request);
    }
}
