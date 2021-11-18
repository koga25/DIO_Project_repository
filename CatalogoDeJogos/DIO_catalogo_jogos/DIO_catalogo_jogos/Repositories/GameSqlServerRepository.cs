using DIO_catalogo_jogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> Get(int page, int quantidade)
        {
            var result = new List<Game>();

            var command = $"select * from Jogos order by id offset {((page - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                result.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return result;
        }

        public async Task<Game> Get(Guid id)
        {
            Game game = null;

            var command = $"select * from Jogos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> Get(string name, string producer)
        {
            var result = new List<Game>();

            var command = $"select * from Jogos where Nome = '{name}' and Produtora = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                result.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Producer = (string)sqlDataReader["Produtora"],
                    Price = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return result;
        }

        public async Task Insert(Game game)
        {
            var command = $"insert Jogos (Id, Nome, Produtora, Preco) values ('{game.Id}', '{game.Name}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"update Jogos set Nome = '{game.Name}', Produtora = '{game.Producer}', Preco = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var command = $"delete from Jogos where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
