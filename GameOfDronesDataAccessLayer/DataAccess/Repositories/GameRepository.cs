using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Repositories
{
    public class GameRepository : BaseRepository<Game, AppContext>, IGameRepository
    {
        private readonly AppContext _context;

        public GameRepository(AppContext context) : base(context)
        {
            _context = context;
        }

        public Game StartNewGame(string playerOneName, string playerTwoName)
        {
            try
            {
                Player playerOneInstance = _context.Players.Where(p => p.name == playerOneName).FirstOrDefault();
                if (playerOneInstance == null)
                {
                    playerOneInstance = new Player
                    {
                        name = playerOneName
                    };
                    _context.Players.Add(playerOneInstance);
                }
                Player playerTwoInstance = _context.Players.Where(p => p.name == playerTwoName).FirstOrDefault();
                if (playerTwoInstance == null)
                {
                    playerTwoInstance = new Player
                    {
                        name = playerTwoName
                    };
                    _context.Players.Add(playerTwoInstance);
                }
                _context.SaveChanges();

                Game gameInstance = new Game
                {
                    playerOneId = playerOneInstance.id,
                    playerTwoId = playerTwoInstance.id,
                    createdOn = DateTime.Now
                };
                this.Add(gameInstance);
                this.Save();

                return gameInstance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Game SetRound(Round round)
        {
            try
            {
                // get round winner
                Nullable<int> roundWinnerId = null;
                var gameInstance = this.FindBy(g => g.id == round.gameId).FirstOrDefault();

                var moveOne = _context.Moves.Where(m => m.id == round.playerOneMoveId).FirstOrDefault();
                var moveTwo = _context.Moves.Where(m => m.id == round.playerTwoMoveId).FirstOrDefault();

                if (moveOne.beatMoveId == moveTwo.id)
                {
                    roundWinnerId = gameInstance.playerOneId;
                }

                if (moveTwo.beatMoveId == moveOne.id)
                {
                    roundWinnerId = gameInstance.playerTwoId;
                }

                // save round
                var roundInstance = new Round
                {
                    id = round.id,
                    gameId = round.gameId,
                    playerOneMoveId = round.playerOneMoveId,
                    playerTwoMoveId = round.playerTwoMoveId,
                    roundWinnerId = roundWinnerId
                };
                _context.Rounds.Add(roundInstance);
                _context.SaveChanges();
                
                return gameInstance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Game getWinner(int id)
        {
            try
            {
                Game gameInstance = this.FindBy(g => g.id == id).FirstOrDefault();
                var scoreOne = this._context.Rounds.Where(r => r.gameId == gameInstance.id && gameInstance.playerOneId == r.roundWinnerId).Count();
                var scoreTwo = _context.Rounds.Where(r => r.gameId == gameInstance.id && gameInstance.playerTwoId == r.roundWinnerId).Count();

                if (scoreOne >= 3)
                {
                    gameInstance.gameWinnerId = gameInstance.playerOneId;
                    this.Save();
                }

                if (scoreTwo >= 3)
                {
                    gameInstance.gameWinnerId = gameInstance.playerOneId;
                    this.Save();
                }

                return gameInstance;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
