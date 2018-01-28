using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIRestAlergenos.Models;

namespace APIRestAlergenos.Controllers
{
    //Obtiene los platos a partir de un alergeno dado
    public class ObtenerPlatosController : ApiController
    {

        private dbRestauranteEntities gDB = new dbRestauranteEntities();

        // GET: api/ObtenerPlatos/5
        public IQueryable<tbPlatos> Get(int pIdAlergeno)
        {
            tbAlergenos vAlergeno = gDB.tbAlergenos.Find(pIdAlergeno);
            List<tbPlatos> lPlatos = new List<tbPlatos>();

            if (vAlergeno != null)
            {
                lPlatos = RecuperarPlatos(vAlergeno);
            }
            return lPlatos.AsQueryable();
        }
            
    
        //Devuelve los platos de un alergeno dado 
        private List<tbPlatos> RecuperarPlatos(tbAlergenos pAlerg)
        {
            List<tbPlatos> lRespPlato = new List<tbPlatos>();
            //Recorremos las tablas para poder llegar a los Platos
            foreach (var vAlergXIng in pAlerg.tbAlerg_Ing)
            {
                foreach(var vIngXPlato in vAlergXIng.tbIngredientes.tbIng_Plato)
                {
                    if (!lRespPlato.Exists(elemento => elemento.Id_Plato == vIngXPlato.tbPlatos.Id_Plato))
                        lRespPlato.Add(LimpiarHerencia(vIngXPlato.tbPlatos));
                }
            }
            return lRespPlato;
        }
        
        //Funcion que borra la herencia para que la salida sea mas limpia
        private tbPlatos LimpiarHerencia(tbPlatos pPlato)
        {
            pPlato.tbIng_Plato.Clear();
            return pPlato;
        }
    }
}
