
using ChamCong.API.Data.Data;
using ChamCong.API.Data.Data.Profile;
using ChamCong.Business.Services.Model;
using ChamCong.Business.Services.V1;
using ChamCong.Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChamCongAPI.WebAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly ImDbContext _dbcontext;
        private readonly AppSettings _appSettings;
        public loginController(ImDbContext LoginController, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _dbcontext = LoginController;
            _appSettings = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// đăng nhập vào hệ thống
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Validate(AccountModel model)
        {
            try
            {
                var checkuser = _dbcontext.im_User.SingleOrDefault(p => p.UserName == model.UserName && model.Password == p.PassWord);
                if (checkuser == null) //không đúng
                {
                    return Ok(new APIReponsitory(false, "Invalid username/password"));
                }
                else
                {
                    checkuser.LastLoginDate = DateTime.Now;

                    return Ok(new APIReponsitory(true, "Authenticate success", GenerateToken(checkuser)));
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi không lấy ra được token");
            }
        }

        private string GenerateToken(im_User nguoiDung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, nguoiDung.UserName),
                    new Claim(ClaimTypes.Email, nguoiDung.Email),
                    new Claim("UserName", nguoiDung.UserName),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}