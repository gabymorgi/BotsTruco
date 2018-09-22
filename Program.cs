using System;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using Utils;
using BotInterface;
using BotClass;

namespace TrucoBot
{
    public static class bots
    {
        public static readonly string[] names = { "RandomBot" };
        public static Bot bot;

        public static int createBot(int choice)
        {
            switch (choice)
            {
                case 0: bot = new RandomBot(); break;
                default:
                    Console.WriteLine("¡ERROR DESCONOCIDO!");
                    Console.ReadKey(true);
                    return -1;
            }
            return 0;
        }
    }

    public class WSHandler : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            GrossData gd = new GrossData();
            gd = JsonConvert.DeserializeObject<GrossData>(e.Data);

            switch (gd.mensaje)
            {
                case "iniciarMano":
                    bots.bot.iniciarMano(new InicioMano(gd));
                    break;
                case "pedirJugada":
                    Jugada jm = bots.bot.jugar(new PedidoJugada(gd));
                    Console.WriteLine(">>>>>{0}", jm.mensaje);
                    string response = JsonConvert.SerializeObject(jm);
                    Send(response);
                    break;
                case "resultadoMano":
                    bots.bot.resultadoMano(new ResultadoMano(gd));
                    break;
                case "resultadoPartida":
                    bots.bot.resultadoPartida(new ResultadoPartida(gd));
                    break;
                case "resultadoEnvido":
                    bots.bot.resultadoEnvido(new ResultadoEnvido(gd));
                    break;
                default:
                    break;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {

            Console.Write("Ingrese el puerto a escuchar: ");
            string port = Console.ReadLine();
            Console.WriteLine("Ingrese el bot que quiere mandar a la trucoarena");
            Console.WriteLine("Opciones: ");
            for (int i = 0; i < bots.names.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i, bots.names[i]);
            }

            int choice;
            while (!Int32.TryParse(Console.ReadLine(), out choice) && choice >= bots.names.Length && choice < 0) ;

            if (bots.createBot(choice) < 0) return;

            Console.WriteLine("{0} está escuchando en localhost:{1}", bots.names[choice], port);
            var wssv = new WebSocketServer("ws://localhost:" + port);
            wssv.AddWebSocketService<WSHandler>("/");
            wssv.Start();
            Console.WriteLine("Presione cualquier tecla para salir");
            Console.ReadKey(true);

            wssv.Stop();

        }
    }
}
