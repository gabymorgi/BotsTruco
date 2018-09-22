using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class Palos
    {
        public const string espada = "espada";
        public const string oro = "oro";
        public const string basto = "basto";
        public const string copa = "copa";
    }

    public static class Mensaje
    {
        public const string carta = "carta";
        public const string truco = "truco";
        public const string retruco = "retruco";
        public const string vale4 = "vale 4";
        public const string envido = "envido";
        public const string realEnvido = "real envido";
        public const string faltaEnvido = "falta envido";
        public const string quiero = "quiero";
        public const string noQuiero = "no quiero";
        public const string irseAlMazo = "irse al mazo";
    }

    public static class Puntos
    {
        //TODO write de values of the songs
    }

    public enum CartaType { inutil, baja, media, alta }
    public enum TrucoSongs { NoSung, truco, retruco, vale4 }
    public enum EnvidoSongs { NoSung, envido, dobleEnvido, realEnvido, FaltaEnvido }

    public class Carta
    {
        public int numero { get; }
        public string palo { get; }
        public CartaType type { get; }
        public int value { get; }

        [JsonConstructor]
        public Carta(int numero, string palo)
        {
            this.numero = numero;
            this.palo = palo;
            this.value = CartaToInt(numero, palo);
            if (value < 5) this.type = CartaType.inutil;
            else if (value < 8) this.type = CartaType.baja;
            else if (value < 11) this.type = CartaType.media;
            else this.type = CartaType.alta;
        }

        private Carta(int numero, string palo, int value, CartaType type)
        {
            this.numero = numero;
            this.palo = palo;
            this.value = value;
            this.type = type;
        }

        public Carta clone()
        {
            return new Carta(this.numero, this.palo, this.value, this.type);
        }

        private int CartaToInt(int numero, string palo)
        {
            switch (numero)
            {
                case 1:
                    switch (palo)
                    {
                        case Palos.oro: case Palos.copa: return 8;
                        case Palos.basto: return 13;
                        case Palos.espada: return 14;
                        default: return -1;
                    }
                case 2: return 9;
                case 3: return 10;
                case 4: return 1;
                case 5: return 2;
                case 6: return 3;
                case 7:
                    switch (palo)
                    {
                        case Palos.basto: case Palos.copa: return 4;
                        case Palos.oro: return 11;
                        case Palos.espada: return 12;
                        default: return -1;
                    }
                case 10: return 5;
                case 11: return 6;
                case 12: return 7;
                default: return -1;
            }
        }
    }

    public class Jugada
    {
        public string mensaje { get; set; }
        public Carta carta { get; set; }

        [JsonConstructor]
        public Jugada(string m, Carta c)
        {
            mensaje = m;
            carta = c;
        }
        public Jugada(string m)
        {
            mensaje = m;
        }
        public Jugada(EnvidoSongs e)
        {
            switch (e)
            {
                case EnvidoSongs.envido:
                case EnvidoSongs.dobleEnvido: mensaje = Mensaje.envido; break;
                case EnvidoSongs.realEnvido: mensaje = Mensaje.realEnvido; break;
                case EnvidoSongs.FaltaEnvido: mensaje = Mensaje.faltaEnvido; break;
            }
        }
        public Jugada(TrucoSongs t)
        {
            switch (t)
            {
                case TrucoSongs.truco: mensaje = Mensaje.truco; break;
                case TrucoSongs.retruco: mensaje = Mensaje.retruco; break;
                case TrucoSongs.vale4: mensaje = Mensaje.vale4; break;
            }
        }
        public Jugada(Carta c)
        {
            mensaje = Mensaje.carta;
            carta = c;
        }
    }

    public class InicioMano
    {
        public Carta[] cartas { get; set; }
        public bool esMano { get; set; }

        public InicioMano(GrossData gd)
        {
            cartas = gd.cartas;
            esMano = gd.esMano;
        }
    }

    public class PedidoJugada
    {
        public Carta[] cartasEnMesa { get; set; }
        public Carta[] cartasEnMesaOponente { get; set; }
        public Jugada jugadaAnterior;
        public Jugada[] jugadasDisponibles;

        public PedidoJugada(GrossData gd)
        {
            cartasEnMesa = gd.cartasEnMesa;
            cartasEnMesaOponente = gd.cartasEnMesaOponente;
            jugadaAnterior = gd.jugadaAnterior;
            jugadasDisponibles = gd.jugadasDisponibles;
        }

    }

    public class ResultadoEnvido
    {
        public bool ganado { get; set; }
        public int tantosOponente;

        public ResultadoEnvido(GrossData gd)
        {
            ganado = gd.ganado;
            tantosOponente = gd.tantosOponente;
        }
    }

    public class ResultadoMano
    {
        public Jugada jugadaAnterior;
        public int puntos;
        public int puntosOponente;

        public ResultadoMano(GrossData gd)
        {
            jugadaAnterior = gd.jugadaAnterior;
            puntos = gd.puntos;
            puntosOponente = gd.puntosOponente;
        }
    }

    public class ResultadoPartida
    {
        public bool ganada { get; set; }

        public ResultadoPartida(GrossData gd)
        {
            ganada = gd.ganada;
        }
    }

    public class GrossData
    {
        public string mensaje { get; set; }
        public Carta[] cartas { get; set; }
        public Carta[] cartasEnMesa { get; set; }
        public Carta[] cartasEnMesaOponente { get; set; }
        public bool esMano { get; set; }
        public bool ganado { get; set; }
        public bool ganada { get; set; }
        public Jugada jugadaAnterior;
        public Jugada[] jugadasDisponibles;
        public int tantosOponente;
        public int puntos;
        public int puntosOponente;
    }
}
