using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Entities
{
    [Table("players", Schema = "dbo")]
    public class Player
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
