using System;
using System.Collections.Generic;
using System.Text;

namespace Progra1Bot.Clases.Alumnos
{
    public class mdAlumnos
    {


        
        public String carnet { get; set; }
        public String nombres { get; set; }
      
      
        public String notaparcial1 { get; set; }
        public String notaparcial2 { get; set; }
        public String notaparcial3 { get; set; }
        public String idbot { get; set; }
        public List<mdAlumnos> Alumnos { get; set; }


        public mdAlumnos()
        {
            Alumnos = new List<mdAlumnos>();
        }


        public List<mdAlumnos> cargaTodosAlumnosBaseDatos()
        {

            return new clsCapaNegocio().CargarAlumnosBaseDatos();
        }


        //public mdAlumnos cargaAlumnoDatosBaseDatos(String correo)
        //{
        //    clsCapaNegocio ReglaNegocio = new clsCapaNegocio();
        //    return ReglaNegocio.EncontrarAlumnoPorMail(correo);
        //}



    }// fin de la clase
}
