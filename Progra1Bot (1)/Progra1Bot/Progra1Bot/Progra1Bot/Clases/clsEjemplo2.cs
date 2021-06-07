using Progra1Bot.Clases.Alumnos;
using Progra1Bot.Clases.emojis;
using Progra1Bot.conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Progra1Bot.Clases
{
   public  class clsEjemplo2
    {
        private static TelegramBotClient Bot;
       

        public  async Task IniciarTelegram()
        {
            Bot = new TelegramBotClient("1823184540:AAGLHw8DmcY8v2bp_ZEfFnBdFlXBsFeVWmI"); 
            

        var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            Bot.OnMessage += BotCuandoRecibeMensajes;
            Bot.OnMessageEdited += BotCuandoRecibeMensajes;
            Bot.OnReceiveError += BotOnReceiveError;

            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"escuchando solicitudes del BOT @{me.Username}");

           

            Console.ReadLine();
            Bot.StopReceiving();
        }

        // cuando recibe mensajes
        private static async void BotCuandoRecibeMensajes(object sender, MessageEventArgs messageEventArgumentos)
        { 
            var ObjetoMensajeTelegram = messageEventArgumentos;
            var mensajes = ObjetoMensajeTelegram.Message;
           
            string mensajeEntrante = mensajes.Text;
           
            string id_manda_mensaje = mensajes.Chat.Id.ToString();

            string respuesta = "No te entiendo";
            if (mensajes == null || mensajes.Type != MessageType.Text)
                return;

            Console.WriteLine($"Recibiendo Mensaje del chat {id_manda_mensaje}.");
            Console.WriteLine($"Dice {ObjetoMensajeTelegram.Message.Text}.");
            List<mdAlumnos> alu = new mdAlumnos().cargaTodosAlumnosBaseDatos();
             
            int indice=0;
            //tolower
            if (mensajes.Text != null && mensajes.Text.ToLower().Contains("hola"))
            {

                respuesta = mdEmoji.saludo + "Hola, bienvenido! por favor ingresa tu numero de carnet\n\n " +
                "Debes de ingresar el numero de carnet de la sigiente manera:\n\n 090501\n\n";

            }
            bool existe = alu.Any(alum => alum.carnet.StartsWith(mensajes.Text));
            string ganaste = "FELICIDADES, GANASTE EL CURSO " + mdEmoji.smile;
            string perdiste = "LO LAMENTO, PERDISTE EL CURSO " + mdEmoji.pulgarAbajo;
            string resp;

            if (existe == true)

            {
                indice = alu.IndexOf(alu.Single(i => i.carnet.Contains(mensajes.Text)));
                int suma = int.Parse(alu[indice].notaparcial1) + int.Parse(alu[indice].notaparcial2) + int.Parse(alu[indice].notaparcial3); 
                   
                if (suma >= 61) { resp = ganaste; }
                else { resp = perdiste; }

                respuesta = mdEmoji.saludo + "Hola " + alu[indice].nombres + " \nTus notas son las siguientes: \n\n" +
                   " Primer Parcial " + alu[indice].notaparcial1 + "\n" +
                   " Segundo Parcial " + alu[indice].notaparcial2 + "\n" +
                   " Tercer Parcial " + alu[indice].notaparcial3 + "\n\n" +
                   "\t\t\tTu nota final es de: " + suma+" puntos"+"\n"+
                   resp;
            }


           
            //}

            //}
            //if (mensajes.Text! != null && idbotexis == false)

            //{
            //    respuesta = mdEmoji.saludo + "Hola, bienvenido! por favor ingresa tu No. de Carnet para verificar que eres parte de este grupo";

            //}


            //if (mensajes.Text! != null && idbotexis == true)

            //{
            //    int indi = alu.IndexOf(alu.Single(i => i.idbot.Contains(id_manda_mensaje)));
            //    respuesta = "Hola " + alu[indi].nombres + " Que deseas consultar?";
            //    if (mensajes.Text.ToLower().Contains("Notas"))

            //    {

            //        respuesta = alu[indi].nombres + "Tus notas son las siguientes: \n\n" +
            //           " Primer Parcial " + alu[indi].notaparcial1 +
            //           " Segundo Parcial " + alu[indi].notaparcial2 +
            //           " Tercer Parcial " + alu[indi].notaparcial3;

            //    }
            //}


            // bool existe = alu.Any(alum => alum.carnet.StartsWith(mensajes.Text));
            //bool vacio = false;
            //if (existe == true)
            //{
            //    indice = alu.IndexOf(alu.Single(i => i.carnet.Contains(mensajes.Text)));
            //    if (String.IsNullOrEmpty(alu[indice].idbot))
            //    {
            //        vacio = true;
            //    }

            //}

            //if (vacio == true)
            //{


            //    actualizardb(int.Parse(mensajes.Text), int.Parse(id_manda_mensaje));

            //    respuesta = "Tu numero quedò registrado, solo puedes consultar tus datos, envia \"ok, \"para continuar";

            //}




           






            if (!String.IsNullOrEmpty(respuesta))//    
            {
                await Bot.SendTextMessageAsync(
                    chatId: ObjetoMensajeTelegram.Message.Chat,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown,
                    text: respuesta

            );
            }

        } // fin del metodo de recepcion de mensajes

        public List<mdAlumnos> CargarAlumnosBaseDatos()
        {
            clsConexion cn = new clsConexion();
            mdAlumnos Alumno = new mdAlumnos();
            List<mdAlumnos> TodosLosAlumnos = new List<mdAlumnos>();

            DataTable dt = cn.consultaTablaDirecta("SELECT *  FROM [tb_alumnos]");

            foreach (DataRow dr in dt.Rows)
            {

                ///crear base de datos con esos nombres en las columnas
                Alumno.carnet = dr["idcarnet"].ToString();
                Alumno.nombres = dr["nombres"].ToString();
                Alumno.notaparcial1 = dr["notaparcial1"].ToString();
                Alumno.notaparcial2 = dr["notaparcial2"].ToString();
                Alumno.notaparcial3 = dr["notaparcial3"].ToString();
                Alumno.idbot = dr["idbot"].ToString();
               
                TodosLosAlumnos.Add(Alumno);
                Alumno = new mdAlumnos();
               
            }
            return TodosLosAlumnos;
        }

        public static void actualizardb(int id,int dato )
        {
            clsConexion cn = new clsConexion();
            mdAlumnos Alumno = new mdAlumnos();

            var evento = "update tb_alumnos Set idbot='"+dato+"' Where idcarnet='"+id+"'";
          
            cn.EjecutaSQLDirecto(evento);

            
               
                Alumno = new mdAlumnos();

            
            
        }



        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("UPS!!! Recibo un error!!!: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }

        //Agregamos



    }
}
