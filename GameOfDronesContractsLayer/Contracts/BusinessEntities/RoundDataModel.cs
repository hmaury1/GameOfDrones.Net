using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesContractsLayer.Contracts.BusinessEntities
{
    public class RoundDataModel
    {
        public int id { get; set; }
        public int gameId { get; set; }
        public int roundWinnerId { get; set; }
        public int playerOneMoveId { get; set; }
        public int playerTwoMoveId { get; set; }
    }
}
