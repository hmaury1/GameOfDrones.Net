using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfDronesContractsLayer.Contracts.BusinessEntities;

namespace GameOfDronesContractsLayer.Contracts.Interfaces
{
    public interface IAppService
    {
        GameDataModel StartNewGame(string playerOneName, string playerTwoName);
        IEnumerable<MoveDataModel> GetMoves();
        IEnumerable<GameDataModel> GetStatistics();
        GameDataModel SetRound(RoundDataModel round);

    }
}
