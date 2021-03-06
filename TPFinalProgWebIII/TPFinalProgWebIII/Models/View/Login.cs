﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TPFinalProgWebIII.Models.View {
    public class Login {
        [Required(ErrorMessage = "El email es obligatorio")]
        [StringLength(200, ErrorMessage = "Ingrese un email mas corto")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El email es invalido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(20, ErrorMessage = "Ingrese una contraseña mas corta")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }

        public bool Recordarme { get; set; } = false;
    }
}