using RedisApp.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisApp.Api.Services.Interfaces
{
    public interface IMomo
    {

        Task<IEnumerable<MomoModel>> GetMomo(int id);
        Task<IEnumerable<MomoModel>> GetMomoAU(int id);
    }
}
