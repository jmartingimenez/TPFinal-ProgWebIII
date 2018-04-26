using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.Entity {
    public class Usuario {
        //public int idUsuario {get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un apellido mas corto")]
        public string Apellido { get; set; }

        /*LAS VALIDACIONES LAS MOVI A LA CLASE DE LA VISTA 'Login'. PREGUNTAR LUEGO 
         QUE HAY QUE HACER CON ESTO...*/
        public string Email { get; set; }

        /*El diagrama de la BDD dice que el máximo es 50, el enunciado que es 20 (?)
         LAS VALIDACIONES LAS MOVI A LA CLASE DE LA VISTA 'Login'. PREGUNTAR LUEGO 
         QUE HAY QUE HACER CON ESTO...*/
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