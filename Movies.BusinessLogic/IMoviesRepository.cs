using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BusinessLogic
{
    public interface IMoviesRepository
    {
        List<DB.Movies> SelectAll();
        DB.Movies GetById(int id);
        
        void DeleteMovie(int id);
        void AddMovie(DB.Movies m);
    }
}
