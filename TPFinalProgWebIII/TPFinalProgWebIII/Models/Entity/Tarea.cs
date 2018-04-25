using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.Entity {
    public class Tarea {
        //public int idTarea {get; set; }
        //public int idCarpeta {get; set; }
        //public int idUsuario {get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "Ingrese una descripción mas corta")]
        public string Descripcion { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Se permiten dos decimales máximo")]
        public decimal EstimadoHoras { get; set; }

        public DateTime FechaFin { get; set; }

        //Los elementos posibles son: Urgente, Alta, Media, Baja. Opcional. Ver esto
        [Required]
        public Int16 Prioridad { get; set; }

        [Required]
        public Int16 Completada { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}