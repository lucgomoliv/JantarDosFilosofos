using System;
using System.Threading;

namespace JantarDosFilosofos
{
    class Program
    {
        static void Main(string[] args)
        {
            int idFilosofo = 0;
            Thread[] filosofos = new Thread[5];
            int[] garfos = new int[] { 1,1,1,1,1};
            for(int i = 0; i < filosofos.Length; i++)
            {
                filosofos[i] = new Thread(new ThreadStart(() => comecar(idFilosofo++, garfos)));
                filosofos[i].Start();
            }
        }

        static void pensar(int idFilosofo)
        {
            Random rnd = new Random();
            Console.WriteLine("Filosofo " + (idFilosofo + 1) + " pensando...");
            Thread.Sleep(rnd.Next(100, 10000));
        }

        static void comer(int idFilosofo, int[] garfos)
        {
            Random rnd = new Random();
            int posGarfoEsquerdo = idFilosofo;
            int posGarfoDireito = (idFilosofo + 1) % garfos.Length;
            Console.WriteLine("Filosofo " + (idFilosofo + 1) + " verificando os garfos!");
            if (verficarGarfos(garfos, posGarfoEsquerdo, posGarfoDireito))
            {
                Console.WriteLine("Filosofo " + (idFilosofo + 1) + " comendo...");
                Thread.Sleep(rnd.Next(100, 10000));
                garfos[posGarfoEsquerdo] = 1;
                garfos[posGarfoDireito] = 1;
                Console.WriteLine("Filosofo " + (idFilosofo + 1) + " terminou de comer!");
            }
        }

        static void comecar(int idFilosofo, int[] garfos)
        {
            while (true)
            {
                pensar(idFilosofo);
                comer(idFilosofo, garfos);
            }
        }

        static bool verficarGarfos(int[] garfos, int posGarfoEsquerdo, int posGarfoDireito)
        {
            lock (garfos)
            {
                if (garfos[posGarfoEsquerdo] == 1 && garfos[posGarfoDireito] == 1)
                {
                    garfos[posGarfoEsquerdo] = 0;
                    garfos[posGarfoDireito] = 0;
                    return true;
                }
                else return false;
            }
        }
    }
}
