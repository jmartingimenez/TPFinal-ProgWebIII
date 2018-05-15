using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPFinalProgWebIII.Models.View
{
    public class CarpetaVal
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Descripcion { get; set; }
    }
}