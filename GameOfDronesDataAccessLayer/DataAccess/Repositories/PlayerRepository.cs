using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Repositories
{
    public class PlayerRepository : BaseRepository<Player, AppContext>, IPlayerRepository
    {
        private readonly AppContext _context;

        public PlayerRepository(AppContext context) : base(context)
        {
            _context = context;
        }
    }
}
