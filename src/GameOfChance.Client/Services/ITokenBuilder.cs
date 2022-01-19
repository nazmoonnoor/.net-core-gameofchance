using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    /// <summary>
    /// Token builder to build jwt token
    /// </summary>
    public interface ITokenBuilder
    {
        Task<Token> GetToken(Claim[] claims);
    }
}
