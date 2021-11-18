using DIO_catalogo_jogos.Exceptions;
using DIO_catalogo_jogos.InputModel;
using DIO_catalogo_jogos.Services;
using DIO_catalogo_jogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }


        /// <summary>
        /// Buscar todos os jogos de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="page">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var result = await _gameService.Get(page, quantity);
            if(result.Count() == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// Buscar um jogo pelo seu Id
        /// </summary>
        /// <param name="idGame">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> Get([FromRoute] Guid idGame)
        {
            var result = await _gameService.Get(idGame);
            if(result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }


        /// <summary>
        /// Inserir um jogo no catálogo
        /// </summary>
        /// <param name="gameInputModel">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>  
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var result = await _gameService.Insert(gameInputModel);
                return Ok(result);
            }
            catch(GameAlreadyRegisteredException exception)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Atualizar um jogo no catálogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="gameInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.Update(idGame, gameInputModel);
                return Ok();
            }
            catch(GameNotRegisteredException exception)
            {
                return NotFound("Esse jogo não existe");
            }  
        }


        /// <summary>
        /// Atualizar o preço de um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser atualizado</param>
        /// <param name="price">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPatch("{idGame:guid}/preco/{price:double}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(idGame, price);
                return Ok();
            }
            catch (GameNotRegisteredException exception)
            {
                return NotFound("Esse jogo não existe");
            }
        }


        /// <summary>
        /// Excluir um jogo
        /// </summary>
        /// /// <param name="idGame">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gameService.Delete(idGame);
                return Ok();
            }
            catch(GameNotRegisteredException exception)
            {
                return NotFound("Esse jogo não existe");
            } 
        }
    }
}
