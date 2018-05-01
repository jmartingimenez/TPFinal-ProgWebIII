using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.View {
    public class Registro {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un apellido mas corto")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(200, ErrorMessage = "Ingrese un email mas corto")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El email es invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(20, ErrorMessage = "Ingrese una contraseña mas corta")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{0,20}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(20, ErrorMessage = "Ingrese una contraseña mas corta")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{0,20}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Contrasenia", ErrorMessage = "Las contraseñas deben coincidir")]
        public string ConfirmarContrasenia { get; set; }
    }
}