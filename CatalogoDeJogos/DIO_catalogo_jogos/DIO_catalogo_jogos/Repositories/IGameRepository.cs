using DIO_catalogo_jogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int quantity);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string producer);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
