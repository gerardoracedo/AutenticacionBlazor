using AutenticacionBlazor.Server.Data.Entidades;
using System.Collections.Generic;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public interface IServiciosPersonaArchivos
    {
        public Documento InsertDocument(Documento doc, int id);

        public void InsertarIntermediaDocPer(Documento_Persona DocPer);

        public void UpdateDocument(Documento doc);

        public Documento GetDocumentByName(string nameFile);

        public Documento GetDocumentById(int Id);

        public IEnumerable<Documento> GetListDocumentByPersonId(int Id);
        
    }
}