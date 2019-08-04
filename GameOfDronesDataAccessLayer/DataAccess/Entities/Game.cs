using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Entities
{
    [Table("games", Schema = "dbo")]
    public class Game
    {
        [Key]
        public int id { get; set; }
        public DateTime createdOn { get; set; }

        //[ForeignKey("Player")]
        public int playerOneId { get; set; }
        //[ForeignKey("id")]
        //public Player playerOne { get; set; }

        //[ForeignKey("Player")]
        public int playerTwoId { get; set; }
        //[ForeignKey("id")]
        //public Player playerTwo { get; set; }

        //[ForeignKey("Player")]
        public Nullable<int> gameWinnerId { get; set; }
        //[ForeignKey("id")]
        //public Player gameWinner { get; set; }
    }
}
