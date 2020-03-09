using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db.Models;
using MongoDB.Driver;
using db.Interfaces;

namespace db.Services
{
    public class MarcaService : BaseService<Marca>
    {
        public MarcaService(IDatabaseSettings settings) : base(settings)
        {
        }
    }
}
