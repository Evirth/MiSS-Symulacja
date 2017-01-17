using System;
using System.Collections.Generic;

namespace MiSS_Symulacja
{
    public class Simulation
    {
        // Parametry modelu
        public static double Krok = 0.01, MaxTime = 100.0;
        public static double G = 10.0, L = 100.0, M = 10, K = 1.0;

        //---------METODA I
        // Listy przechowujące informacje o zmiannach położenia wahadła w czasie
        // Kąt musi być podany w radianach
        public static List<double> Fi0 = new List<double>(new[] { 90.0 * Math.PI / 180 });
        public static List<double> Fi1 = new List<double>(new[] { 0.0 });
        // Ze wzoru na 2 pochodną
        public static List<double> Fi2 = new List<double>(new[] { -Math.Sin(Fi0[0]) * G / L - Fi1[0] * K / M });
        public static List<double> Times = new List<double>(new[] { 0.0 });

        // Metoda Euhlera - niski czas obliczeń, niska dokładność
        public static void WahadloEuhler(double krok, double maxTime, double g, double l, double m, double k,
            ref List<double> fi0, ref List<double> fi1, ref List<double> fi2, ref List<double> times)
        {
            int i = 0;
            for (double time = times[0] + krok; time <= maxTime; time += krok, i += 1)
            {
                fi0.Add(fi0[i] + fi1[i] * krok);
                fi1.Add(fi1[i] + fi2[i] * krok);
                // Ze wzoru na 2 pochodną
                fi2.Add(-Math.Sin(fi0[i + 1]) * g / l - fi1[i + 1] * k / m);
                times.Add(time);
            }
        }
    }
}