using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Desafio.WebAPI.Core.Services.Identidade;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Desafio.SisGerTarefas.EndTEndTests.Base
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _defaultSerializeOptions;
        private const string _adminUser = "teste@teste.com";
        private const string _adminPassword = "123456";
        private readonly UserManager<IdentityUser> _userManager;

        public ApiClient(HttpClient httpClient)
        { 
            _httpClient = httpClient;
            //_defaultSerializeOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    PropertyNameCaseInsensitive = true,
            //};
            //AddAuthorizationHeader();
        }

        private void AddAuthorizationHeader()
        {
            var usuarioRespostaLogin = GerarJwt(_adminUser)
                .GetAwaiter().GetResult();
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue(
                    "Bearer", usuarioRespostaLogin.AccessToken);
        }       

        public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(
            string route,
            object payload
        )
            where TOutput : class
        {
            //var payloadJson = JsonSerializer.Serialize(
            //    payload,
            //    _defaultSerializeOptions
            //);
            var response = await _httpClient.PostAsync(
                route,
                new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json"
                )
            );
            var outputString = await response.Content.ReadAsStringAsync();
            TOutput? output = null;
            if (!string.IsNullOrWhiteSpace(outputString))
                output = JsonSerializer.Deserialize<TOutput>(outputString,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );
            return (response, output);
        }

        private async Task<UsuarioRespostaLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
                claims.Add(new Claim("Tarefa", "Criar"));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MEUSEGREDOSUPERSECRETOMEUSEGREDOSUPERSECRETOMEUSEGREDOSUPERSECRETOMEUSEGREDOSUPERSECRETO");
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "MeuSistema",
                Audience = "https://localhost",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }

        private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UsuarioRespostaLogin
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(2).TotalSeconds,
                UsuarioToken = new UsuarioToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
