using GameOfDronesDataAccessLayer.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Interfaces
{
    public interface IGameRepository : IRepository<Game>
    {
        Game StartNewGame(string playerOneName, string playerTwoName);
        Game SetRound(Round round);
        Game getWinner(int id);
    }
}
