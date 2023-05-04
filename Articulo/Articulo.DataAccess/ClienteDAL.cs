using Articulo.Entities;
using Articulo.Entities.AppContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulo.DataAccess
{
    public class ClienteDAL
    {
        private static ClienteDAL _instance;

        public static ClienteDAL Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClienteDAL();
                }
                return _instance;

            }


        }

        public List<Cliente> SellectAll()
        {
            List<Cliente> result = null;
            using (AppDBContext _context = new AppDBContext())
            {
                result = _context.Clientes.ToList();
            }

            return result;


        }

        public Cliente SellectById(int id)
        {
            Cliente result = null;
            using (AppDBContext _context = new AppDBContext())
            {
                result = _context.Clientes
                    .FirstOrDefault(x => x.ClienteId == id);
            }

            return result;


        }
        public bool Insert(Cliente entity)
        {
            bool result = false;
            using (AppDBContext _context = new AppDBContext())
            {
                var query = _context.Clientes.FirstOrDefault(x => x.Nombre.Equals(entity.Nombre));
                if (query == null)
                {
                    _context.Clientes.Add(entity);
                    result = _context.SaveChanges() > 0;

                }

                return result;

            }

        }
    }
}
