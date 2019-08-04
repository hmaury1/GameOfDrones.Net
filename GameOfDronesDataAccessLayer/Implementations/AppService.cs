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
                Game gameInstance = GameRepository.StartNewGame(playerOneName, playerTwoName);

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
                Round roundInstance = new Round
                {
                    id = round.id,
                    gameId = round.gameId,
                    playerOneMoveId = round.playerOneMoveId,
                    playerTwoMoveId = round.playerTwoMoveId,
                };
                GameRepository.SetRound(roundInstance);

                GameRepository.getWinner(round.gameId);

                // get result 
                var data = this.GetResult(round.gameId);
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
