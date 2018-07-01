using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPFinalProgWebIII.Models.View
{
    public class TareaListar
    {

        public Nullable<System.DateTime> FechaFin { get; set; }
        public string Nombre { get; set; }
        public short Prioridad { get; set; }
        public string NombreCarpeta { get; set; }
        public Nullable<decimal> EstimadoHoras { get; set; }
        public short Completada { get; set; }
        public int IdTarea { get; set; }

    }
}