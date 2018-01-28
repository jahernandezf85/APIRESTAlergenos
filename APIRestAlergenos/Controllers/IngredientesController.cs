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
    public class IngredientesController : ApiController
    {
        private dbRestauranteEntities gDB = new dbRestauranteEntities();

        // POST: api/Ingredientes
        public IHttpActionResult Post(tbIngredientes pIngrediente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gDB.tbIngredientes.Add(pIngrediente);

            try
            {
                gDB.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ExisteIngrediente(pIngrediente.Id_Ingrediente))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = pIngrediente.Id_Ingrediente }, pIngrediente);
        }

        //Se usa para comprobar si un id existe en la BBDD
        private bool ExisteIngrediente(int pId)
        {
            return gDB.tbIngredientes.Count(e => e.Id_Ingrediente == pId) > 0;
        }
    }
}
