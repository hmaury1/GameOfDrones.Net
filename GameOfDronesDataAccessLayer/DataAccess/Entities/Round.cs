using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Entities
{
    [Table("rounds", Schema = "dbo")]
    public class Round
    {
        [Key]
        public int id { get; set; }
        public int gameId { get; set; }
        public Nullable<int> roundWinnerId { get; set; }
        public int playerOneMoveId { get; set; }
        public int playerTwoMoveId { get; set; }
    }
}
