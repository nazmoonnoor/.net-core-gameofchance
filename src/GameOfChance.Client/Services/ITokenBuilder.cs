using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameOfChance.Client.Services
{
    public interface ITokenBuilder
    {
        Task<Token> GetToken(Claim[] claims);
    }
}
