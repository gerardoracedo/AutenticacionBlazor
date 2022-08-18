using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Shared.Modelos.Rentas.Comercio
{
    public class MMisActividades
    {
        public int Id { get; set; }
        public int Estado { get; set; }
        public string Estado_descripcion { get; set; }
        public string Estado_badge { get; set; }
        public string Inicio_actividad { get; set; }
        public string Fin_actividad { get; set; }
        public string Nombre_local { get; set; }
        public string Padron_CISI { get; set; }
        public string Motivo_baja { get; set; }
        public int Actividad_codigo { get; set; }
        public string Actividad_descripcion { get; set; }
        public string Actividad_observacion { get; set; }
        public string Actividad_alicuota { get; set; }
        public int Actividad_subgrupo_numero { get; set; }
        public string Actividad_subgrupo_descripcion { get; set; }
        public string Actividad_grupo_letra { get; set; }
        public string Actividad_grupo_descripcion { get; set; }
        public string Actividad_direccion_nombre_calle { get; set; }
        public string Actividad_direccion_numero { get; set; }
        public string Actividad_direccion_piso { get; set; }
        public string Actividad_direccion_departamento { get; set; }
        public string Actividad_direccion_sector { get; set; }
        public string Actividad_direccion_manzana { get; set; }
        public string Actividad_direccion_lote { get; set; }
        public string Actividad_direccion_descripcion { get; set; }
        public string Actividad_direccion_cp { get; set; }
        public string Actividad_direccion_pais { get; set; }
        public string Actividad_direccion_provincia { get; set; }
        public string Actividad_direccion_localidad { get; set; }
        public string Persona_apellido_nombre_legal { get; set; }
        public string Persona_nombre_nombre_fantasia { get; set; }
        public string Persona_cuit { get; set; }
        public int Nro_tramite { get; set; }
        public string Tramite_url { get; set; }
    }
}
