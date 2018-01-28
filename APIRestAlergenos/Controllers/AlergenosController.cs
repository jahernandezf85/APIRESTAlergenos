using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using APIRestAlergenos.Models;

namespace APIRestAlergenos.Controllers
{
    public class AlergenosController : ApiController
    {
        private dbRestauranteEntities gDB = new dbRestauranteEntities();

        // POST: api/Alergenos
        public IHttpActionResult Post(tbAlergenos pAlergeno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gDB.tbAlergenos.Add(pAlergeno);

            try
            {
                gDB.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExisteAlergeno(pAlergeno.id_Alergeno))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = pAlergeno.id_Alergeno }, pAlergeno);
        }

        //Se usa para comprobar si un id existe en la BBDD
        private bool ExisteAlergeno(int pId)
        {
            return gDB.tbAlergenos.Count(e => e.id_Alergeno == pId) > 0;
        }
    }
}
