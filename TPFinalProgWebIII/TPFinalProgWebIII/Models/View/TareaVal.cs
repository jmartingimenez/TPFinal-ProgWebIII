using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Enum;

namespace TPFinalProgWebIII.Models.View
{
    public class TareaVal
    {
        [Required(ErrorMessage = "Debe seleccionar una carpeta")]
        public int IdCarpeta { get; set; }
      
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "Ingrese un nombre mas corto")]
        public string Descripcion { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "La cantidad de horas estimadas solo puede tener hasta dos decimales")]
        [Range(0.00,23.59,ErrorMessage ="Ingrese una hora valida; ej: 9.35")]
        [Timestamp]
        public Nullable<decimal> EstimadoHoras { get; set; }
        
        public TipoPrioridad Prioridad { get; set; }     
          
    }
}