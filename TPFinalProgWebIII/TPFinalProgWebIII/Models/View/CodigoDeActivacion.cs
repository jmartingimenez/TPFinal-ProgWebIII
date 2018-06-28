using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPFinalProgWebIII.Models.View
{
    public class CodigoDeActivacion
    {
        [Required(ErrorMessage = "Debe ingresar el código de activación envíado a su correo")]
        [StringLength(200)]
        public string CodigoActivacion { get; set; }
        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(200, ErrorMessage = "Ingrese un email mas corto")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El email es invalido")]
        public string Email { get; set; }

    }
}