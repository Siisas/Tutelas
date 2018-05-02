using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo
{
    public class mSorteoRevision
    {
        public mSorteoRevision()
        {
            Despacho = new mDespacho();
        }

        public int Id { get; set; }

        public int Ano { get; set; }

        public int Mes { get; set; }

        public string MesString { get; set; }

        public int Dia { get; set; }

        public int IdDespacho { get; set; }

        public mDespacho Despacho { get; set; }
    }
}
