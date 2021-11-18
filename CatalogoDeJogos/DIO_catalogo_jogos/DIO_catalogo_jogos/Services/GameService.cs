using DIO_catalogo_jogos.Entities;
using DIO_catalogo_jogos.Exceptions;
using DIO_catalogo_jogos.InputModel;
using DIO_catalogo_jogos.Repositories;
using DIO_catalogo_jogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _GameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _GameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> Get(int page, int quantity)
        {
            var result = await _GameRepository.Get(page, quantity);

            return result.Select(Game => new GameViewModel
            {
                Id = Game.Id,
                Name = Game.Name,
                Producer = Game.Producer,
                Price = Game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var result = await _GameRepository.Get(id);

            if(result == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Producer = result.Producer,
                Price = result.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameEntity = await _GameRepository.Get(game.Name, game.Producer);

            if(gameEntity.Count > 0)
            {
                throw new GameAlreadyRegisteredException();
            }

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _GameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameEntity = await _GameRepository.Get(id);

            if(gameEntity == null)
            {
                throw new GameNotRegisteredException();
            }

            gameEntity.Name = game.Name;
            gameEntity.Producer = game.Producer;
            gameEntity.Price = game.Price;

            await _GameRepository.Update(gameEntity);
        }

        public async Task Update(Guid id, double price)
        {
            var gameEntity = await _GameRepository.Get(id);

            if (gameEntity == null)
            {
                throw new GameNotRegisteredException();
            }
            gameEntity.Price = price;

            await _GameRepository.Update(gameEntity);
        }

        public async Task Delete(Guid id)
        {
            var game = await _GameRepository.Get(id);

            if(game == null)
            {
                throw new GameNotRegisteredException();
            }

            await _GameRepository.Delete(id);
        }

        public void Dispose()
        {
            _GameRepository?.Dispose();
        }
    }
}
