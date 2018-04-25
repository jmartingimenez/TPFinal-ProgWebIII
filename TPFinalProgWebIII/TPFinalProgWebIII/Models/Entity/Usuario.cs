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

        //Si el DataType no sirve, usar [RegularExpression(blabla)]
        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(200, ErrorMessage = "Ingrese un email mas corto")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El email es invalido")]     
        public string Email { get; set; }

        //El diagrama de la BDD dice que el máximo es 50, el enunciado que es 20 (?)
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(20, ErrorMessage = "Ingrese una contraseña mas corta")]  
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