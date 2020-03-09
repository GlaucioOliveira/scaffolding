using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db.Models;
using db.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace db.Services
{
    public class PecaService : BaseService<Peca>
    {
        public PecaService(IDatabaseSettings settings) : base(settings)
        {
        }

        public List<Peca> ListarPecasAlternativas(string ModeloOrigem)
        {
            return _collection.Find(x => x.ModeloOrigem == ModeloOrigem).ToList();
        }

        public List<PecaQuantidadePorModelo> ListarQuantidadePecasAlternativas()
        {
            //agrupa a lista de peças cadastradas pelo id do Modelo,
            //e tras a quantidade somada de registros.
            var listaQuantidadePecasPorModelo = _collection.Aggregate()
                           .Group(x => x.ModeloOrigem,
                           g => new PecaQuantidadePorModelo()
                           {
                               ModeloOrigem = g.Key,
                               ContagemPecas = g.Select(x=> x.ModeloOrigem).Count()
                           }).ToList();

            return listaQuantidadePecasPorModelo;
        }

        public override Peca Create(Peca peca)
        {
            peca.DataCriacao = DateTime.Now;
            peca.DataAlteracao = DateTime.Now;

            _collection.InsertOne(peca);
            return peca;
        }
    }
}
