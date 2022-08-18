using AutenticacionBlazor.Shared.Modelos.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MDdjj_InformacionPersona
    {
        public int Uid { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }
    }
    public class MDdjj_agregar
    {
        public int Uid_persona { get; set; }
        public int Id_periodo { get; set; }
        public int Id_tipo_ddjj { get; set; }
        public List<MDdjj_agregar_detalle> Detalle { get; set; }
        public MCuentaCorriente Detalle_cc { get; set; }
    }
    public class MDdjj_agregar_detalle
    {
        public int Id_actividad { get; set; }
        public double Monto_imponible { get; set; }
    }
    public class MDdjj_ver_carga
    {
        public int Persona { get; set; } // Uid_persona
        public int Periodo { get; set; } // Id_periodo
    }

    public class MDdjj_actividades
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string Inicio_actividad { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public string Alicuota { get; set; }
        public bool Publicidad_y_propaganda { get; set; }
        public int Id_direccion { get; set; }
        public string Nombre_calle { get; set; }
        public string Numero_calle { get; set; }
        public string Pais { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string Codigo_postal { get; set; }
        public double Monto_imponible { get; set; }
        public double SubTotal { get; set; }
        public double Importe_publicidad_y_propaganda { get; set; }

    }
}
