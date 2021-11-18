using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO_catalogo_jogos.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException() : base("Esse jogo já está cadastrado")
        {

        }
    }
}
