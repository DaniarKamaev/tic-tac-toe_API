using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ApiMicro.Game;

namespace ApiMicro.Controllers
{
    [ApiController]
    [Route("Size")]
    public class GameController : ControllerBase
    {
        private static List<Game> _games = new List<Game>
        {
            new Game {
                Id = 1,
                Size = 3,
                Board = new char[][]
                {
                    new char[] {' ', ' ', ' '},
                    new char[] {' ', ' ', ' '},
                    new char[] {' ', ' ', ' '}
                },
                CurrentPlayer = 'X',
                Status = "InProgress",
                MoveCount = 0
            }
        };

        [HttpGet]
        public IActionResult GetSize([FromQuery] int gameId)
        {
            var result = _games.FirstOrDefault(g => g.Id == gameId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("Create")]
        public IActionResult CreateGame([FromBody] Game game)
        {
            _games.Add(game);
            return Ok(game);
        }

        [HttpPost("Move")]
        public IActionResult MakeMove([FromBody] MoveRequest request)
        {
            var game = _games.FirstOrDefault(g => g.Id == request.GameId);
            if (game == null) return NotFound("Game not found");

            // Если игра уже завершена
            if (game.Status != "InProgress")
            {
                return BadRequest($"Game Finished. Status: {game.Status}");
            }

            if (request.X < 0 || request.X >= game.Size ||
                request.Y < 0 || request.Y >= game.Size ||
                game.Board[request.X][request.Y] != ' ')
            {
                return BadRequest("Invalid move");
            }

            game.Board[request.X][request.Y] = game.CurrentPlayer;
            game.MoveCount++;

            game.CheckWinner();

            if (game.Status == "InProgress")
            {
                game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';
            }

            return Ok(game);
        }
    }

   

  
}
