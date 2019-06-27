using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjemploWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                _context.Facturas.Add(new Factura {  Nombre="Test", Detalles= new List<FacturaDetalle> { new FacturaDetalle { Nombre="Producto1" }, new FacturaDetalle { Nombre = "Producto2" } } });
                _context.SaveChanges();
            }
        }


        // GET: api/Factura
        [HttpGet]
        public IEnumerable<Factura> Get()
        {
            return _context.Facturas.Include(t=>t.Detalles).ToList();
        }

        // POST: api/Factura
        [HttpPost]
        public void Post(Factura factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }


        
    }
}
