using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.DB;
using PagedList;
using Movies.Data;

namespace Movies.Data
{
    public class RattingRepository
    {
        MovieFPEntities context = new MovieFPEntities();
        public List<Ratting> SelectAll()
        {
            return context.Ratting.ToList();

        }
    }
}
