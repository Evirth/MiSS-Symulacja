using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MiSS_Symulacja
{
    public partial class MainWindow : Window
    {
        public Line MyLine { get; set; }
        public Ellipse MyEllipse { get; set; }
        public DispatcherTimer Timer { get; set; }
        public int Iterator { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Simulation.WahadloEuhler(Simulation.Krok, Simulation.MaxTime, Simulation.G, Simulation.L, Simulation.M, Simulation.K,
                ref Simulation.Fi0, ref Simulation.Fi1, ref Simulation.Fi2, ref Simulation.Times);

            MyLine = new Line
            {
                Stroke = Brushes.Black,
                Fill = Brushes.Black,
                X1 = Width / 2,
                Y1 = Height / 4
            };

            MyEllipse = new Ellipse
            {
                Fill = Brushes.BlueViolet,
                Width = 10,
                Height = 10,
            };

            MyLine.X2 = Simulation.L * Math.Sin(Simulation.Fi0[0]) + Width / 2;
            MyLine.Y2 = Simulation.L * Math.Cos(Simulation.Fi0[0]) + Height / 4;
            Canvas.SetLeft(MyEllipse, MyLine.X2 - 5);
            Canvas.SetTop(MyEllipse, MyLine.Y2 - 5);
            SimulationPanel.Children.Add(MyLine);
            SimulationPanel.Children.Add(MyEllipse);
            Timer = new DispatcherTimer(DispatcherPriority.Render) { Interval = TimeSpan.Zero };
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MyLine.X2 = Simulation.L * Math.Sin(Simulation.Fi0[Iterator]) + Width / 2;
            MyLine.Y2 = Simulation.L * Math.Cos(Simulation.Fi0[Iterator]) + Height / 4;
            Canvas.SetLeft(MyEllipse, MyLine.X2 - 5);
            Canvas.SetTop(MyEllipse, MyLine.Y2 - 5);
            Iterator++;
            //System.Threading.Thread.Sleep(1);
            if (Iterator >= Simulation.Fi0.Count)
            {
                Timer.Stop();
                StartButton.IsEnabled = true;
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Iterator = 0;
            Timer.Start();
            StartButton.IsEnabled = false;
        }
    }
}
