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
        IEnumerable<EmployeeDataModel> GetPagedEmployeeDataById(int id, int pageSize = 10, int pageNo = 1);
        IEnumerable<EmployeeDataModel> GetPagedEmployeeDataByName(string name, int pageSize = 10, int pageNo = 1);
        GameDataModel StartNewGame(string playerOneName, string playerTwoName);
        IEnumerable<MoveDataModel> GetMoves();
        IEnumerable<GameDataModel> GetStatistics();
        GameDataModel SetRound(RoundDataModel round);

    }
}
