using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ListadoEmpleados.Models;
using ListadoEmpleados.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListadoEmpleados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ListadoEmpContext _context;

        public HomeController(ListadoEmpContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Empleado> lista = _context.Empleados.Include(c => c.oCargo).ToList();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado)
        {
            EmpleadoVM oEmpleadoVM = new EmpleadoVM()
            {
                oEmpleado = new Empleado(),
                oListaCargo = _context.Cargos.Select(cargo => new SelectListItem()
                {
                    Text = cargo.Descripcion,
                    Value = cargo.IdCargo.ToString()
                }).ToList()
            };

            if (idEmpleado != 0)
            {
                oEmpleadoVM.oEmpleado = _context.Empleados.Find(idEmpleado);
            }

            return View(oEmpleadoVM);
        }

        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVM oEmpleadoVM)
        {
            if (oEmpleadoVM.oEmpleado.IdEmpleado == 0)
            {
                _context.Empleados.Add(oEmpleadoVM.oEmpleado);
            }
            else
            {
                _context.Empleados.Update(oEmpleadoVM.oEmpleado);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idEmpleado)
        {
            Empleado oEmpleado = new Empleado();

            if (idEmpleado != 0)
            {
                oEmpleado = _context.Empleados.Include(r => r.oCargo).Where(r => r.IdEmpleado == idEmpleado).FirstOrDefault();
            }

            return View(oEmpleado);
        }

        [HttpPost]
        public IActionResult Eliminar(Empleado oEmpleado)
        {
            _context.Empleados.Remove(oEmpleado);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
