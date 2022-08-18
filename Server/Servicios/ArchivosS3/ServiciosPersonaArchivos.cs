using AutenticacionBlazor.Server.Data;
using AutenticacionBlazor.Server.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutenticacionBlazor.Server.Servicios.ArchivosS3
{
    public class ServiciosPersonaArchivos : IServiciosPersonaArchivos
    {
        private readonly ApplicationDbContext contexto;
        public ServiciosPersonaArchivos(ApplicationDbContext context)
        {
            contexto = context;
        }

        public Documento InsertDocument(Documento doc, int id)
        {
            var result = contexto.Documentos.Add(doc).Entity;
            contexto.SaveChanges();
            var docPer = new Documento_Persona
            {
                IdDocumento = result.Id,
                IdPersona = id
            };
            InsertarIntermediaDocPer(docPer);

            return result;
        }
        public void InsertarIntermediaDocPer(Documento_Persona DocPer)
        {
            contexto.Documento_Personas.Add(DocPer);
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

        public IEnumerable<Documento> GetListDocumentByPersonId(int Id)
        {
            List<Documento> ListObjectToReturn = new List<Documento>();
            var ListIdDocument = contexto.Documento_Personas.Where(x => x.IdPersona == Id).ToList();
            foreach (var item in ListIdDocument)
            {
                var doc = contexto.Documentos.Find(item.Id);
                ListObjectToReturn.Add(doc);
            }
            return ListObjectToReturn;
        }
    }
}
