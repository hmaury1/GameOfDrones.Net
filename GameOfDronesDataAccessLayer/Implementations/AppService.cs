using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfDronesContractsLayer.Contracts.BusinessEntities;
using GameOfDronesContractsLayer.Contracts.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;

namespace GameOfDronesDataAccessLayer.Implementations
{
    public class AppService :IAppService
    {
        public readonly IGameRepository GameRepository;
        public readonly IPlayerRepository PlayerRepository;
        public readonly IMoveRepository MoveRepository;
        public readonly IRoundRepository RoundRepository;

        public AppService(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IMoveRepository moveRepository,
            IRoundRepository roundRepository
        )
        {
            GameRepository = gameRepository;
            PlayerRepository = playerRepository;
            MoveRepository = moveRepository;
            RoundRepository = roundRepository;
        }

        public GameDataModel StartNewGame(string playerOneName, string playerTwoName)
        {
            try
            {
                Player playerOneInstance = PlayerRepository.FindBy(p => p.name == playerOneName).FirstOrDefault();
                if(playerOneInstance == null)
                {
                    playerOneInstance = new Player
                    {
                        name = playerOneName
                    };
                    PlayerRepository.Add(playerOneInstance);
                }
                Player playerTwoInstance = PlayerRepository.FindBy(p => p.name == playerTwoName).FirstOrDefault();
                if (playerTwoInstance == null)
                {
                    playerTwoInstance = new Player
                    {
                        name = playerTwoName
                    };
                    PlayerRepository.Add(playerTwoInstance);
                }
                PlayerRepository.Save();

                Game gameInstance = new Game
                {
                    playerOneId = playerOneInstance.id,
                    playerTwoId = playerTwoInstance.id,
                    createdOn = DateTime.Now
                };
                GameRepository.Add(gameInstance);
                GameRepository.Save();

                return new GameDataModel
                {
                    id = gameInstance.id,
                    playerOneId = gameInstance.playerOneId,
                    playerOne = new PlayerDataModel {
                        id = gameInstance.playerOneId,
                        name = playerOneInstance.name
                    },
                    playerTwoId = gameInstance.playerTwoId,
                    playerTwo = new PlayerDataModel
                    {
                        id = gameInstance.playerTwoId,
                        name = playerTwoInstance.name
                    },
                    gameWinnerId = gameInstance.gameWinnerId.HasValue ? gameInstance.gameWinnerId.Value : 0,
                    createdOn = gameInstance.createdOn
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<MoveDataModel> GetMoves()
        {
            try
            {
                var data = from m in MoveRepository.GetAll().ToList()
                           select new MoveDataModel
                           {
                                id = m.id,
                                name = m.name,
                                iconClass = m.iconClass,
                           };
                return data.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<GameDataModel> GetStatistics()
        {
            try
            {
                var data = this.GetResult(0);
                return data.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public GameDataModel SetRound(RoundDataModel round)
        {
            try
            {
                // get round winner
                Nullable<int> roundWinnerId = null;
                var gameInstance = GameRepository.FindBy(g => g.id == round.gameId).FirstOrDefault();

                var moveOne = MoveRepository.FindBy(m => m.id == round.playerOneMoveId).FirstOrDefault();
                var moveTwo = MoveRepository.FindBy(m => m.id == round.playerTwoMoveId).FirstOrDefault();

                if (moveOne.beatMoveId == moveTwo.id) {
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
                RoundRepository.Add(roundInstance);
                RoundRepository.Save();

                // get game winner
                var scoreOne = RoundRepository.FindBy(r => r.gameId == gameInstance.id && gameInstance.playerOneId == r.roundWinnerId).Count();
                var scoreTwo = RoundRepository.FindBy(r => r.gameId == gameInstance.id && gameInstance.playerTwoId == r.roundWinnerId).Count();

                if (scoreOne >= 3) {
                    gameInstance.gameWinnerId = gameInstance.playerOneId;
                    GameRepository.Save();
                }

                if (scoreTwo >= 3)
                {
                    gameInstance.gameWinnerId = gameInstance.playerOneId;
                    GameRepository.Save();
                }

                // get result 
                var data = this.GetResult(gameInstance.id);
                return data.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private IEnumerable<GameDataModel> GetResult(int id)
        {
            var data = from g in GameRepository.GetAll().ToList()
                       join p1 in PlayerRepository.GetAll().ToList() on g.playerOneId equals p1.id
                       join p2 in PlayerRepository.GetAll().ToList() on g.playerTwoId equals p2.id
                       where id == 0 || g.id == id
                       select new GameDataModel
                       {
                           id = g.id,
                           playerOneId = g.playerOneId,
                           playerOne = new PlayerDataModel
                           {
                               id = p1.id,
                               name = p1.name,
                               score = RoundRepository.FindBy(r => r.gameId == g.id && g.playerOneId == r.roundWinnerId).Count()
                           },
                           playerTwoId = g.playerTwoId,
                           playerTwo = new PlayerDataModel
                           {
                               id = p2.id,
                               name = p2.name,
                               score = RoundRepository.FindBy(r => r.gameId == g.id && g.playerTwoId == r.roundWinnerId).Count()
                           },
                           gameWinnerId = g.gameWinnerId.HasValue ? g.gameWinnerId.Value : 0,
                           gameWinner = (from w in PlayerRepository.FindBy(f => f.id == g.gameWinnerId.Value).ToList()
                                         select new PlayerDataModel
                                         {
                                             id = w.id,
                                             name = w.name
                                         }).FirstOrDefault(),
                           createdOn = g.createdOn,
                           rounds = (from r in RoundRepository.GetAll().ToList()
                                     where r.gameId == g.id
                                     select new RoundDataModel
                                     {
                                         id = r.id,
                                         gameId = r.gameId,
                                         roundWinnerId = r.roundWinnerId.HasValue ? r.roundWinnerId.Value : 0,
                                         playerOneMoveId = r.playerOneMoveId,
                                         playerTwoMoveId = r.playerTwoMoveId
                                     })
                       };
            return data;
        }
    }
}
