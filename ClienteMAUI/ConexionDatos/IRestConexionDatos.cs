using ClienteMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMAUI.ConexionDatos 
{
    interface IRestConexionDatos //se dice rest porque debe cum[lir ciertas caracteristicas que garanticen el correcto funcionamiento de la bd
    {
        Task<List<Plato>> GetPlatosAsync();//solo para trabajar en hilos diferentes y no colapse el procesamiento de datos
        Task AddPlatoAsync(Plato plato);
        Task UpdatePlatoASync(Plato plato);
        Task DeletePlatoAsync(int id);
    }
}
