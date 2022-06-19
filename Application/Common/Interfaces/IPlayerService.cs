using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPlayerService
    {
        
        Task<int> FindPlayerById(int id);

        Task<Player> CreatePlayer(int id, string name, string? postion);

    }
}
