using System;
using System.Collections.Generic;

namespace MiSS_Symulacja
{
    public class Simulation
    {
        // Parametry modelu
        public static double Krok = 0.01, MaxTime = 100.0;
        public static double G = 9.81, L = 100.0, M = 10, K = 5.0;

        // Listy przechowujące informacje o zmiannach położenia wahadła w czasie
        public static List<double> Fi0 = new List<double>(new[] { 0.1 });
        public static List<double> Fi1 = new List<double>(new[] { 0.1 });
        public static List<double> R0 = new List<double>(new[] { 3.0 });
        public static List<double> R1 = new List<double>(new[] { 1.0 });

        public static List<double> Fi2 = new List<double>(new[] { -(2 * R1[0] * Fi1[0] / (L + R0[0])) - G * Math.Sin(Fi0[0]) / (L + R0[0]) });
        public static List<double> R2 = new List<double>(new[] { (L + R0[0]) * Math.Pow(Fi1[0], 2) + G * Math.Cos(Fi0[0]) - K * R0[0] / M });
        public static List<double> Times = new List<double>(new[] { 0.0 });

        // Metoda Euhlera - niski czas obliczeń, niska dokładność
        public static void WahadloEuhler(double krok, double maxTime, double g, double l, double m, double k,
            ref List<double> fi0, ref List<double> fi1, ref List<double> fi2, ref List<double> r0, ref List<double> r1, ref List<double> r2, ref List<double> times)
        {
            int i = 0;
            for (double time = times[0] + krok; time <= maxTime; time += krok, i += 1)
            {
                fi0.Add(fi0[i] + fi1[i] * krok);
                fi1.Add(fi1[i] + fi2[i] * krok);
                r0.Add(r0[i] + r1[i] * krok);
                r1.Add(r1[i] + r2[i] * krok);
                fi2.Add(-(2 * r1[i + 1] * fi1[i + 1] / (l + R0[i + 1])) - g * Math.Sin(fi0[i + 1]) / (l + r0[i + 1]));
                r2.Add((l + r0[i + 1]) * Math.Pow(fi1[i + 1], 2) + g * Math.Cos(fi0[i + 1]) - k * r0[i + 1] / m);
                times.Add(time);
            }
        }
    }
}