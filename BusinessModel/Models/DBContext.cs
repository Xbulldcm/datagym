using DataAccess.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class DBContext<T> : IDisposable where T : class
    {

        private readonly DataGYMEntities context;
        public IBusinessModelGen<T> genericBM;


        public DBContext(DataGYMEntities _context)
        {

            context = _context;
            genericBM = new BusinessModelGenImpl<T>(context);

        }

        public bool Complete()
        {

            try
            {
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                string msj = e.Message;
                return false;
            }

        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
