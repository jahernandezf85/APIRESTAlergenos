using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIRestAlergenos.Models;



namespace APIRestAlergenos.Controllers
{
    //Obtiene los alergenos a partir de un plato dado
    public class ObtenerAlergenosController : ApiController
    {
        private dbRestauranteEntities gDB = new dbRestauranteEntities();
        

        // GET: api/ObtenerAlergenos/1
        public IQueryable<tbAlergenos> Get(int pIdPlato)
        {
            tbPlatos vPlato = gDB.tbPlatos.Find(pIdPlato);
            List<tbAlergenos> lAlergenos = new List<tbAlergenos>();

            if (vPlato != null)
            {
                lAlergenos = RecuperarAlergenos(vPlato);
            }
            return lAlergenos.AsQueryable();
        }

        //Recorremos las tablas para poder llegar a los alergenos
        private List<tbAlergenos> RecuperarAlergenos(tbPlatos pPlato)
        {
            List<tbAlergenos> lRespAlerg = new List<tbAlergenos>();

            foreach(var vIngXPlato in pPlato.tbIng_Plato)
            {
                foreach(var vAlergXIng in vIngXPlato.tbIngredientes.tbAlerg_Ing)
                {
                    if (!lRespAlerg.Exists(elemento => elemento.id_Alergeno == vAlergXIng.tbAlergenos.id_Alergeno))
                        lRespAlerg.Add(LimpiarHerencia(vAlergXIng.tbAlergenos));
                }
            }
            return lRespAlerg;
        } 
        
        //Funcion que elimina la herencia para devolver una salida mas limpia
        private tbAlergenos LimpiarHerencia(tbAlergenos pAlerg)
        {
            pAlerg.tbAlerg_Ing.Clear();
            return pAlerg;
        }   
    }
}
