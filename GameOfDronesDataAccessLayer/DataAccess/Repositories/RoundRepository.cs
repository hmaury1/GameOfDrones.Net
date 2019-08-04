using GameOfDronesDataAccessLayer.DataAccess.Entities;
using GameOfDronesDataAccessLayer.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfDronesDataAccessLayer.DataAccess.Repositories
{
    public class RoundRepository : BaseRepository<Round, AppContext>, IRoundRepository
    {
        private readonly AppContext _context;

        public RoundRepository(AppContext context) : base(context)
        {
            _context = context;
        }
    }
}
