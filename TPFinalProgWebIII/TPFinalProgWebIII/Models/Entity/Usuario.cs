using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.Entity {
    public class Usuario {
        //public int idUsuario {get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        [Required]
        public Int16 Activo { get; set; }

        [Required]
        public DateTime FechaRegistracion { get; set; }

        public DateTime FechaActivacion { get; set; }

        [Required]
        [StringLength(200)]
        public string CodigoActivacion { get; set; }
    }
}