using DataBase.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using CORE.DTOs;


namespace CuponMania.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private static readonly List<Cupon> CuponesBaseDeDatos = new List<Cupon>
        {
            new Cupon { Codigo = "DESC10", Descuento = 10, Vencimiento = new DateTime(2024, 12, 31), Utilizado = false },
            new Cupon { Codigo = "DESC20", Descuento = 20, Vencimiento = new DateTime(2024, 11, 30), Utilizado = false },
            new Cupon { Codigo = "DESC15", Descuento = 15, Vencimiento = new DateTime(2024, 10, 15), Utilizado = false },
            new Cupon { Codigo = "DESC05", Descuento = 5, Vencimiento = new DateTime(2023, 11, 30), Utilizado = false },
            new Cupon { Codigo = "DESC50", Descuento = 50, Vencimiento = new DateTime(2024, 12, 31), Utilizado = true },
            new Cupon { Codigo = "DESC30", Descuento = 30, Vencimiento = new DateTime(2024, 12, 07), Utilizado = false },
            new Cupon { Codigo = "BLACKFRIDAY", Descuento = 40, Vencimiento = new DateTime(2024, 11, 28), Utilizado = true },
            new Cupon { Codigo = "CYBERMONDAY", Descuento = 25, Vencimiento = new DateTime(2024, 12, 03), Utilizado = false },
            new Cupon { Codigo = "EXPIRED", Descuento = 10, Vencimiento = new DateTime(2023, 09, 30), Utilizado = false }
        };

        [HttpGet]
        public ActionResult<List<Cupon>> GetCupones()
        {
            return Ok(CuponesBaseDeDatos);
        }

        [HttpGet("{codigo}")]
        public ActionResult<Cupon> GetCupon(string codigo)
        {
            var cupon = CuponesBaseDeDatos.FirstOrDefault(c => c.Codigo == codigo);
            if (cupon == null)
            {
                return NotFound("Cupon no encontrado");
            }
            return Ok(cupon);
        }
        [HttpPatch("utilizarCupon{codigo}")]
        public ActionResult<RespuestaPrivada<Cupon>> PatchDesabilitar(string codigo) {
            var respuesta = new RespuestaPrivada<Cupon>();

            var cupon = CuponesBaseDeDatos.FirstOrDefault(c => c.Codigo == codigo);
            if (cupon == null)
            {
                respuesta.Datos = null;
                respuesta.Mensaje = "Cupon No encontrado";
                respuesta.Exito = false;
                return NotFound(respuesta);
            }
            else
            {
                if (cupon.Utilizado == true)
                {
                   
                    respuesta.Mensaje = "cupon no disponible";
                    respuesta.Datos = cupon;
                    respuesta.Exito = false;
                    return Ok(respuesta);
                }
                else {
                    cupon.Utilizado = true;
                    respuesta.Mensaje = "Cupon desabilitado";
                    respuesta.Datos = cupon;
                    respuesta.Exito = true;
                    return Ok(respuesta);
                }
            } 



        }


    }

}
