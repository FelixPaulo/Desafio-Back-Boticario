using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cashbot.Services.Api.Configurations
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "avaliacaoboticario@meuambienteToken";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
