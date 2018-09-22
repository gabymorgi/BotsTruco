using Utils;

namespace BotInterface
{
    public abstract class Bot
    {
        public abstract void iniciarMano(InicioMano data);
        public abstract Jugada jugar(PedidoJugada data);
        public abstract void resultadoMano(ResultadoMano data);
        public abstract void resultadoPartida(ResultadoPartida data);
        public abstract void resultadoEnvido(ResultadoEnvido data);
    }
}