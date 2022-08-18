using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Personas.Fisica
{
    public class MPersonaFisicaGet
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public int Uid_persona { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Cuit { get; set; }
        public string Fecha_nacimiento { get; set; }
        public int Id_genero { get; set; }
        public int Id_pais { get; set; }
        public int Id_estado_civil { get; set; }
        public int Id_tipo_dni { get; set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Estado_civil { get; set; }
        public string Tipo_dni { get; set; }
    }
}
