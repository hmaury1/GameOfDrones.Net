using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfDronesDataAccessLayer.DataAccess;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using GameOfDronesDataAccessLayer.DataAccess.Repositories;

namespace GameOfDronesApplicationTests.IntegrationTests
{
    public class GameRepositoryTest
    {
        private IGameRepository _gameRepository;

        [SetUp]
        public void Initialize()
        {
            var context = new AppContext(ConfigurationManager.ConnectionStrings["SampleAppConnection"].ConnectionString);
            _gameRepository = new GameRepository(context);

        }

        [Test]
        public void must_return_new_game()
        {
            string playerOneName = "Pablo";
            string playerTwoName = "Jose";

            var data = _gameRepository.StartNewGame(playerOneName, playerTwoName);

            if (data.id > 0)
            {
                Assert.Warn("There is no data for this test case");
            }

            else
                Assert.IsNotNull(data);
        }

        [Test]
        public void must_return_the_statistics_records()
        {
            string playerOneName = "Pablo";
            string playerTwoName = "Jose";

            var game = _gameRepository.StartNewGame(playerOneName, playerTwoName);
            var data = _gameRepository.GetAll().ToList();

            if (!data.Any())
            {
                Assert.Warn("There is no data in the table for this test case");
            }

            else
                Assert.IsTrue(data.Any());
        }

        [Test]
        public void must_set_a_round()
        {
            string playerOneName = "Pablo";
            string playerTwoName = "Jose";

            var game = _gameRepository.StartNewGame(playerOneName, playerTwoName);

            GameOfDronesDataAccessLayer.DataAccess.Entities.Round instance = new GameOfDronesDataAccessLayer.DataAccess.Entities.Round
            {
                gameId = game.id,
                playerOneMoveId = 1,
                playerTwoMoveId = 2
            };

            var round = _gameRepository.SetRound(instance);

            if (round.id > 0)
            {
                Assert.Warn("There is no data for this test case");
            }

            else
                Assert.IsNotNull(round);
        }

        [Test]
        public void must_get_winner_of_the_game()
        {
            string playerOneName = "Pablo";
            string playerTwoName = "Jose";

            var game = _gameRepository.StartNewGame(playerOneName, playerTwoName);

            GameOfDronesDataAccessLayer.DataAccess.Entities.Round instance = new GameOfDronesDataAccessLayer.DataAccess.Entities.Round
            {
                gameId = game.id,
                playerOneMoveId = 1,
                playerTwoMoveId = 2
            };

            // play three times
            var round1 = _gameRepository.SetRound(instance);
            var round2 = _gameRepository.SetRound(instance);
            var round3 = _gameRepository.SetRound(instance);

            var winner = _gameRepository.getWinner(game.id);

            if (winner.gameWinnerId > 0)
            {
                Assert.Warn("There is no data for this test case");
            }

            else
                Assert.IsNotNull(winner);
        }

    }
}
