using System;
using System.Collections.Generic;
using System.Text;

namespace db.Interfaces
{
    public interface IDatabaseSettings
    {
        string Peca { get; set; }
        string Marca { get; set; }
        string Modelo { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
