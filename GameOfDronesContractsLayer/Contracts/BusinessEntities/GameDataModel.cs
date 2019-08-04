using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesContractsLayer.Contracts.BusinessEntities
{
    public class GameDataModel
    {
        public int id { get; set; }
        public int playerOneId { get; set; }
        public PlayerDataModel playerOne;
        public int playerTwoId { get; set; }
        public PlayerDataModel playerTwo;
        public int gameWinnerId { get; set; }
        public PlayerDataModel gameWinner;
        public DateTime createdOn { get; set; }
        public IEnumerable<RoundDataModel> rounds { get; set; }
    }
}
