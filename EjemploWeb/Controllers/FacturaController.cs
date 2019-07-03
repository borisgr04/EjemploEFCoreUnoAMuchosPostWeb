using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjemploWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EjemploWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {

        private readonly VentasContext _context;

        public FacturaController(VentasContext context)
        {
            _context = context;

            if (_context.Facturas.Count()==0)
            {
                Cliente cliente1 = new Cliente { Nombre = "Cliente1" };
                _context.Clientes.Add(cliente1);
                _context.SaveChanges();
                _context.Facturas.Add(new Factura {  Nombre="Test", Cliente=cliente1, Detalles= new List<FacturaDetalle> { new FacturaDetalle { Nombre="Producto1" }, new FacturaDetalle { Nombre = "Producto2" } } });
                _context.Facturas.Add(new Factura { Nombre = "Test2", Cliente = cliente1, Detalles = new List<FacturaDetalle> { new FacturaDetalle { Nombre = "Producto21" }, new FacturaDetalle { Nombre = "Producto22" } } });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Factura> GetByDate()
        {
            //return _context.Facturas.ToList();
            return _context.Facturas.Include(t => t.Detalles).Include(t => t.Cliente).ToList();
        }

        // POST: api/Factura
        [HttpPost]
        public void Post(Factura factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }

        // Ejemplo 2 Recepcion de Fecha en Objeto
        // GET: api/Factura
        [HttpPost]
        [Route("QueryDate")]
        public ActionResult<QueryFacturasResponse> PostQuery(QueryFacturasRequest request)
        {
            if (request.Fecha.Month < DateTime.Now.Month) {
                return BadRequest("Error");
            }
            return new QueryFacturasResponse { Fecha = request.Fecha.ToLongDateString() };
        }




        
    }
    public class QueryFacturasResponse
    {
        [JsonProperty("fecha")]
        public string Fecha { get; set; }
    }

    public class QueryFacturasRequest
    {
        public DateTime Fecha { get; set; }
    }
}
