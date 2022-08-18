using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas
{
    public class MMisDirecciones
    {
        public int Id_persona_direccion { get; set; }
        public int Id_persona { get; set; }
        public int Id_tipo_direccion { get; set; }
        public string Tipo_direccion { get; set; }
        public int Id_direccion { get; set; }
        public int Id_pais { get; set; }
        public string Pais { get; set; }
        public int Id_provincia { get; set; }
        public string Provincia { get; set; }
        public int Id_localidad { get; set; }
        public string Localidad { get; set; }
        public string Codigo_postal { get; set; }
        public string Nombre_calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Sector { get; set; }
        public string Manzana { get; set; }
        public string Lote { get; set; }
        public string Descripcion { get; set; }
    }
}
