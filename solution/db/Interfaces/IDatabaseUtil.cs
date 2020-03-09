using System;
using System.Collections.Generic;
using System.Text;

namespace db.Interfaces
{
    public interface IDatabaseUtil
    {
        /// <summary>
        /// Generate Table or Collection (MongoDB) if it not exist on the current database.
        /// </summary>
        void GenerateTableIfNotExist();
    }
}
