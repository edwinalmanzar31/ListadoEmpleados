using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListadoEmpleados.Models.ViewModels
{
    public class EmpleadoVM
    {
        public Empleado oEmpleado { get; set; }
        public List<SelectListItem> oListaCargo { get; set; }
    }
}
