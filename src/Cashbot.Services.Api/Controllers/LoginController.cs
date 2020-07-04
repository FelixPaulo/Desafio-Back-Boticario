using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.ViewModels.Login;
using Cashbot.Domain.Models;
using Cashbot.Services.Api.Configurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Cashbot.Services.Api.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly TokenDescriptor TokenDescriptor;
        private readonly IDealerApplication _dealerApplication;

        public LoginController(INotificationHandler<ApplicationNotification> notifications, TokenDescriptor tokenDescriptor, IDealerApplication dealerApplication) : base(notifications)
        {
            this.TokenDescriptor = tokenDescriptor;
            _dealerApplication = dealerApplication;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return base.ResponseError(ModelState.Values);
            }

            var dealer = await this._dealerApplication.GetByEmailAndPassword(model.Email, model.Password);

            if (dealer != null)
            {
                var response = GenerateTokenDealer(dealer);
                return base.ResponseResult(response);
            }

            return base.ResponseError(new { Erro = $"Login invalido, email: {model.Email} ou seha: {model.Password} invalidos" });
        }


        private object GenerateTokenDealer(Dealer dealer)
        {
            var identity = new ClaimsIdentity
                            (
                                new GenericIdentity(dealer.Email),
                                new[] {
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Sub, dealer.Id.ToString())
                                }
                            );
            var handler = new JwtSecurityTokenHandler();
            var signingConf = new SigningCredentialsConfiguration();
            IdentityModelEventSource.ShowPII = true;
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = this.TokenDescriptor.Issuer,
                Audience = this.TokenDescriptor.Audience,
                //IssuedAt = _settings.IssuedAt,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(this.TokenDescriptor.MinutesValid),
                SigningCredentials = signingConf.SigningCredentials
            });

            var jwtToken = handler.WriteToken(securityToken);

            return new
            {
                access_token = jwtToken,
                token_type = "bearer",
                expires_in = this.TokenDescriptor.MinutesValid
            };
        }
    }
}