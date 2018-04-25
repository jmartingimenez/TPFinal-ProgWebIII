using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.Entity {
    public class Carpeta {
        //public int idCarpeta {get; set; }
        //public int idUsuario {get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "Ingrese una descripción mas corta")]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}