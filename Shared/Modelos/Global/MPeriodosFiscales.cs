using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Global
{
    public class MBuscaPeriodoFiscalResultado
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Uid_periodo { get; set; }
        public int Id_periodo { get; set; }
        public int Id_tipo_periodo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Vencimiento { get; set; }
        public int Orden { get; set; }
        public int Periodo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo_periodo { get; set; }
    }

    public class MBuscarPeriodoFiscal
    {
        public int Periodo { get; set; }
        public int Tipo_periodo { get; set; }
    }

    public class MListaPeriodosFiscales
    {
        public int Id { get; set; }
        public int Periodo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Vencimiento { get; set; }
        public int Uid_periodo { get; set; }
    }
}
