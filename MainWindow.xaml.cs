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
        public int Mode { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LineSimulation.WahadloEuhler(LineSimulation.Krok, LineSimulation.MaxTime, LineSimulation.G, LineSimulation.L, LineSimulation.M, LineSimulation.K,
                ref LineSimulation.Fi0, ref LineSimulation.Fi1, ref LineSimulation.Fi2, ref LineSimulation.Times);

            SpringSimulation.WahadloEuhler(SpringSimulation.Krok, SpringSimulation.MaxTime, SpringSimulation.G, SpringSimulation.L, SpringSimulation.M, SpringSimulation.K,
                ref SpringSimulation.Fi0, ref SpringSimulation.Fi1, ref SpringSimulation.Fi2, ref SpringSimulation.R0, ref SpringSimulation.R1, ref SpringSimulation.R2, ref SpringSimulation.Times);

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
            
            MyLine.X2 = MyLine.X1;
            MyLine.Y2 = MyLine.Y1 + LineSimulation.L;
            Canvas.SetLeft(MyEllipse, MyLine.X2 - 5);
            Canvas.SetTop(MyEllipse, MyLine.Y2 - 5);
            SimulationPanel.Children.Add(MyLine);
            SimulationPanel.Children.Add(MyEllipse);
            Timer = new DispatcherTimer(DispatcherPriority.Render) { Interval = TimeSpan.Zero };
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1);
            switch (Mode)
            {
                case 0:
                    MyLine.X2 = LineSimulation.L * Math.Sin(LineSimulation.Fi0[Iterator]) + Width / 2;
                    MyLine.Y2 = LineSimulation.L * Math.Cos(LineSimulation.Fi0[Iterator]) + Height / 4;
                    Canvas.SetLeft(MyEllipse, MyLine.X2 - 5);
                    Canvas.SetTop(MyEllipse, MyLine.Y2 - 5);
                    Iterator++;
                    if (Iterator >= LineSimulation.Fi0.Count)
                    {
                        Timer.Stop();
                        LineSim.IsEnabled = true;
                        SpringSim.IsEnabled = true;
                    }
                    break;
                case 1:
                    double li = SpringSimulation.L + SpringSimulation.R0[Iterator];
                    MyLine.X2 = li * Math.Sin(SpringSimulation.Fi0[Iterator]) + Width / 2;
                    MyLine.Y2 = li * Math.Cos(SpringSimulation.Fi0[Iterator]) + Height / 4;
                    Canvas.SetLeft(MyEllipse, MyLine.X2 - 5);
                    Canvas.SetTop(MyEllipse, MyLine.Y2 - 5);
                    Iterator++;
                    if (Iterator >= SpringSimulation.R0.Count)
                    {
                        Timer.Stop();
                        LineSim.IsEnabled = true;
                        SpringSim.IsEnabled = true;
                    }
                    break;
            }
        }

        private void SpringSimButton_Click(object sender, RoutedEventArgs e)
        {
            Iterator = 0;
            Timer.Start();
            LineSim.IsEnabled = false;
            SpringSim.IsEnabled = false;
            Mode = 1;
        }

        private void LineSimButton_Click(object sender, RoutedEventArgs e)
        {
            Iterator = 0;
            Timer.Start();
            LineSim.IsEnabled = false;
            SpringSim.IsEnabled = false;
            Mode = 0;
        }
    }
}
