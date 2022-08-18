using AutenticacionBlazor.Server.Data;
using AutenticacionBlazor.Server.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public class ServicioTicketArchivos : IServicioTicketArchivos
    {
        private readonly ApplicationDbContext contexto;
        public ServicioTicketArchivos(ApplicationDbContext context)
        {
            contexto = context;
        }

        public Documento InsertDocument(Documento doc, int IdTicket)
        {
            var result = contexto.Documentos.Add(doc).Entity;
            contexto.SaveChanges();
            var docPer = new Documento_Ticket
            {
                IdDocumento = result.Id,
                IdTicket = IdTicket
            };
            InsertarIntermediaDocTicket(docPer);

            return result;
        }
        public void InsertarIntermediaDocTicket(Documento_Ticket DocTick)
        {
            contexto.Documento_Tickets.Add(DocTick);
            contexto.SaveChanges();
        }

        public void UpdateDocument(Documento doc)
        {
            var docUpdate = contexto.Documentos.Find(doc.Id);
            docUpdate.Extencion = doc.Extencion;
            docUpdate.NombreArchivo = doc.NombreArchivo;
            contexto.Entry(docUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            contexto.SaveChanges();
        }

        public Documento GetDocumentByName(string nameFile)
        {
            return contexto.Documentos.FirstOrDefault(x => x.NombreArchivo == nameFile);
        }
        public Documento GetDocumentById(int Id)
        {
            return contexto.Documentos.Find(Id);
        }

        public IEnumerable<Documento> GetListDocumentByTicketId(int Id)
        {
            List<Documento> ListObjectToReturn = new List<Documento>();
            var ListIdDocument = contexto.Documento_Tickets.Where(x => x.IdTicket == Id).ToList();
            foreach (var item in ListIdDocument)
            {
                var doc = contexto.Documentos.Find(item.Id);
                ListObjectToReturn.Add(doc);
            }
            return ListObjectToReturn;
        }
    }
}
