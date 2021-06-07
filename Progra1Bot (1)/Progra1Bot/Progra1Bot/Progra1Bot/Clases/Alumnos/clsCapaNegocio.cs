using Progra1Bot.conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Progra1Bot.Clases.Alumnos
{
    class clsCapaNegocio
    {

        public clsCapaNegocio()
        {
                
        }


        //public mdAlumnos LocalizaAlumnoPorMail(String correo,String IDUsuarioTelegram)
        //{
        //    mdAlumnos AlumnoEncontrado = new mdAlumnos();
        //    AlumnoEncontrado.nombre = "no encontrado";
        //    int linea = 1;
        //    int linea_encontrada = 0;

        //    var TodosLosAlunnos = new mdAlumnos().cargaDatos("c:\\tmp\\alumnos.csv","T");
        //    ClsManejoArchivos ClaseArchivos = new ClsManejoArchivos();
        //    foreach (mdAlumnos item in TodosLosAlunnos)
        //    {
        //        if (correo.ToLower().Equals(item.correo))
        //        {
        //            linea_encontrada = linea;
        //            AlumnoEncontrado = item;
        //           // string nuevaLinea = item.apellido + ";" + item.nombre + ";" + item.correo + ";" + item.carnet + ";" + item.seccion + ";" + IDUsuarioTelegram;
        //         //   ClaseArchivos.CambioLinea(nuevaLinea, "c:\\tmp\\alumnos.csv", linea_encontrada);


        //        }
        //        linea++;
        //    }
        //    return AlumnoEncontrado;
        //}
        public List<mdAlumnos> CargarAlumnosBaseDatos()
        {
            clsConexion cn = new clsConexion();
            mdAlumnos Alumno = new mdAlumnos();
            List<mdAlumnos> TodosLosAlumnos = new List<mdAlumnos>();

            DataTable dt = cn.consultaTablaDirecta("SELECT *  FROM [tb_alumnos]");

            foreach (DataRow dr in dt.Rows)
            {
                Alumno.carnet = dr["idcarnet"].ToString();
                Alumno.nombres = dr["nombres"].ToString();             
                Alumno.notaparcial1 = dr["notaparcial1"].ToString();
                Alumno.notaparcial2 = dr["notaparcial2"].ToString();
                Alumno.notaparcial3 = dr["notaparcial3"].ToString();
                Alumno.idbot = dr["idbot"].ToString();
                //Alumno.apellido = dr["apellido"].ToString();
                //Alumno.correo = dr["correo"].ToString();
                //Alumno.seccion = dr["seccion"].ToString();
                TodosLosAlumnos.Add(Alumno);
                Alumno = new mdAlumnos();
                int pause = 0;
            }
            return TodosLosAlumnos;
        }

    }
}
