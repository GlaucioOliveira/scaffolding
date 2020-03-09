using System;
using db.Models;
using db.Services;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using pecacompativel.cli.Models;
using MongoDB.Bson;

namespace cli
{
    class Program
    {
        #region [Variáveis Globais]
        static DatabaseSettings dbConnection;
        static PecaService pecadb;
        static MarcaService marcadb;
        static ModeloService modelodb;
        #endregion

        static void Main(string[] args)
        {
            carregaServices();
            Console.WriteLine("Console Util - CLI");

            //processa as marcas, modelos e anos do JSON na pasta DUMP/
            var listaMarcas = pegaJSONProcessado();

            //apaga todas as marcas e modelos cadastrados.
            marcadb.RemoveAll();
            modelodb.RemoveAll();

            foreach (var item in listaMarcas)
            {
                Console.WriteLine($"Importando: {item.Key}");

                var marca = new Marca()
                {
                    Nome = item.Key.Trim()
                };

                foreach (var modelo in item.Value)
                {
                    Modelo novoModelo = new Modelo()
                    {
                        Nome = modelo.Modelo.Trim(),
                        Ano = modelo.Ano,
                        MarcaId = marca.Id,
                        MarcaNome = marca.Nome
                    };
                     //cadastra no banco um novo modelo.
                    modelodb.Create(novoModelo);

                    marca.Modelo.Add(new Modelo()
                    {
                        Id = novoModelo.Id,
                        Nome = modelo.Modelo.Trim(),
                        Ano = modelo.Ano,
                        MarcaNome = marca.Nome
                    });
                }

                //cadastro de marcas;
                marcadb.Create(marca);

                foreach (var modelo in marca.Modelo)
                {
                    //var modeloParaEditar = modelodb.Get(modelo.Id);
                    modelo.MarcaId = marca.Id;
                    modelo.MarcaNome = marca.Nome;

                    //modelodb.Update(modeloParaEditar.Id, modeloParaEditar);
                }

                marcadb.Update(marca.Id, marca);
            }

            Console.WriteLine("\n> Pressione uma Tecla para Encerrar... ");
            Console.ReadKey();
        }

        private static Dictionary<string, List<Automovel>> pegaJSONProcessado()
        {
            Dictionary<string, List<Automovel>> listaMarcas = new Dictionary<string, List<Automovel>>();

            string jsonContent = File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\..\dump\moto.json");
            var jsonBody = JsonDocument.Parse(jsonContent).RootElement;

            foreach (var item in jsonBody.EnumerateArray())
            {
                var marca = item.GetProperty("marca").GetString();
                var modelo = item.GetProperty("modelo").GetString();
                var cod_fipe = item.GetProperty("cod_fipe").GetString();

                if (!listaMarcas.ContainsKey(marca))
                {
                    listaMarcas.Add(marca, new List<Automovel>());
                }
                else
                {
                    Automovel moto = new Automovel()
                    {
                        Marca = marca,
                        Modelo = modelo
                    };

                    moto.Ano = pegaMotoAno(cod_fipe);

                    listaMarcas[marca].Add(moto);
                }
            }

            return listaMarcas;
        }

        private static List<string> pegaMotoAno(string cod_fipe)
        {
            List<string> listaAnoModelo = new List<string>();

            string jsonContent = File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\..\dump\moto_modelo_ano\{cod_fipe}.json");
            var jsonBody = JsonDocument.Parse(jsonContent).RootElement;

            foreach (var item in jsonBody.EnumerateArray())
            {
                var tipo = item.GetProperty("tipo").GetString().Replace("gasolina", "").Replace("alcool", "").Trim();

                if (!listaAnoModelo.Contains(tipo) && !tipo.ToLower().Equals("zero km")) listaAnoModelo.Add(tipo);
            }

            return listaAnoModelo;
        }

        /// <summary>
        /// Carrega os services para manipular os dados no banco de dados.
        /// </summary>
        static void carregaServices()
        {
            //mongodb://localhost:27017
            string ConnectionString = "mongodb://127.0.0.1:1234/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";

            dbConnection = new DatabaseSettings()
            {
                DatabaseName = "pecacompativel",
                ConnectionString = ConnectionString,
                Peca = "pecacompativel"
            };
            
            pecadb = new PecaService(dbConnection);

            marcadb = new MarcaService(new DatabaseSettings()
            {
                DatabaseName = "pecacompativel",
                ConnectionString = ConnectionString,
                Marca = "marca"
            });

            modelodb = new ModeloService(new DatabaseSettings()
            {
                DatabaseName = "pecacompativel",
                ConnectionString = ConnectionString,
                Modelo = "modelo"
            });
        }
    }
}
