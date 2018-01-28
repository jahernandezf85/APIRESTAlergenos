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
    public class PlatosController : ApiController
    {

        private dbRestauranteEntities gDB = new dbRestauranteEntities();

        // POST: api/Platos
        public IHttpActionResult Post(tbPlatos pPlato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gDB.tbPlatos.Add(pPlato);

            try
            {
                gDB.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExistePlato(pPlato.Id_Plato))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = pPlato.Id_Plato }, pPlato);
        }

        //Se usa para comprobar si un id existe en la BBDD
        private bool ExistePlato(int pId)
        {
            return gDB.tbPlatos.Count(e => e.Id_Plato == pId) > 0;
        }
    }
}
