using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Client.Helpers
{
    public struct MSelectorMultiple
    {
        public MSelectorMultiple(string llave, string valor)
        {
            Llave = llave;
            Valor = valor;
        }
        public string Llave { get; set; }
        public string Valor { get; set; }
    }

}
