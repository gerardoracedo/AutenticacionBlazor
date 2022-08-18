using AutenticacionBlazor.Server.Data.Entidades;
using System.Collections.Generic;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public interface IServicioTicketArchivos
    {
        public Documento InsertDocument(Documento doc, int IdTicket);

        public void InsertarIntermediaDocTicket(Documento_Ticket DocTick);

        public void UpdateDocument(Documento doc);

        public Documento GetDocumentByName(string nameFile);

        public Documento GetDocumentById(int Id);

        public IEnumerable<Documento> GetListDocumentByTicketId(int Id);
        
    }
}