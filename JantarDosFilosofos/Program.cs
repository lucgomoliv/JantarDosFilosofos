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
            int[] garfos = new int[5];
            for(int i = 0; i < filosofos.Length; i++)
            {
                filosofos[i] = new Thread(new ThreadStart(() => comecar(idFilosofo++, garfos)));
            }
            foreach(Thread filosofo in filosofos)
            {
                filosofo.Start();
            }
        }

        static void pensar(int idFilosofo)
        {
            Random rnd = new Random();
            Console.WriteLine("Filosofo " + (idFilosofo + 1) + " pensando...");
            Thread.Sleep(rnd.Next() % 5000);
        }

        static void comer(int idFilosofo, int[] garfos)
        {
            Random rnd = new Random();
            int garfoEsquerdo = garfos[idFilosofo];
            int garfoDireito = garfos[idFilosofo % 5];

            lock (garfos)
            {
                Console.WriteLine("Filosofo " + (idFilosofo + 1) + " comendo...");
                Thread.Sleep(rnd.Next() % 10000);
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
    }
}
