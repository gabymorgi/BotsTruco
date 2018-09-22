using System;
using System.Collections.Generic;
using BotInterface;
using Utils;

namespace BotClass
{
    class RandomBot : Bot
    {
        public List<Carta> cartas;

        public override void iniciarMano(InicioMano data)
        {
            this.cartas = new List<Carta>();
            this.cartas.AddRange(data.cartas);
            Console.WriteLine("Cartas repartidas: [{0} {1}, {2} {3}, {4} {5}]",
                cartas[0].palo, cartas[0].numero,
                cartas[1].palo, cartas[1].numero,
                cartas[2].palo, cartas[2].numero
            );
        }

        public override Jugada jugar(PedidoJugada data)
        {
            int j;
            int c;
            Random r = new Random();
            j = r.Next(0, data.jugadasDisponibles.Length);

            if (data.jugadasDisponibles[j].mensaje == "carta")
            {

                c = r.Next(0, cartas.Count);
                data.jugadasDisponibles[j].carta = cartas[c];
                Console.WriteLine(">>>>>>>> Juego Carta: {0} de {1}", cartas[c].numero, cartas[c].palo);
                cartas.RemoveAt(c);

            }
            else
            {

                Console.WriteLine(">>>>>>>> ¡{0}!", data.jugadasDisponibles[j].mensaje);
            }
            return data.jugadasDisponibles[j];
        }

        public override void resultadoEnvido(ResultadoEnvido data)
        {
            Console.WriteLine("### Resultado del Envido -> {0} Tantos oponente: {1}", (data.ganado) ? "Ganado" : "Perdido", data.tantosOponente);
        }

        public override void resultadoMano(ResultadoMano data)
        {
            Console.WriteLine("### Resultado de la mano -> Puntos: {0} Puntos Oponente: {1}", data.puntos, data.puntosOponente);
        }

        public override void resultadoPartida(ResultadoPartida data)
        {
            Console.WriteLine("### Resultado de la Partida -> {0}", (data.ganada) ? "Ganada" : "Perdida");
        }
    }
}
