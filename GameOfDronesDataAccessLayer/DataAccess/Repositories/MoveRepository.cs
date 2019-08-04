using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Repositories
{
    public class MoveRepository : BaseRepository<Move, AppContext>, IMoveRepository
    {
        private readonly AppContext _context;

        public MoveRepository(AppContext context) : base(context)
        {
            _context = context;
        }
    }
}
