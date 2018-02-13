using System;
using System.Threading;

namespace ViscousFriction
{
    class MaterialPoint : CRKMethod
    {
        double g = 9.81, b, m;
        Thread T;

        public MaterialPoint(double m, double V0, double a0, double b) : base(4)
        {
            double[] Y0 = { 0, 0, V0 * Math.Sin(a0), V0 * Math.Cos(a0)};
            SetInit(0, Y0);

            this.m = m;
            this.b = b;

            StartAction();
        }

        public void StartAction() { T = new Thread(new ParameterizedThreadStart(Action)); T.Start(); }
        public void StopAction() {
            switch (T.ThreadState)
            {
                case ThreadState.Running: T.Abort(); break;
            } 
        }

        public void Action(object d) {
            while (Y[1] >= 0)
            {
                Thread.Sleep(1);
                NextStep(0.001);
                
                double x = Y[0];
                double y = Y[1];

                double Vx = Y[2];
                double Vy = Y[3];
                double V = Math.Sqrt(Math.Pow(Vx, 2) + Math.Pow(Vy, 2));

                double ax = FY[2];
                double ay = FY[3];
                double a = Math.Sqrt(Math.Pow(ax, 2) + Math.Pow(ay, 2));

                lock (Form.gr[0])
                {
                    Form.gr[0].AddGraphDot(x, y);
                }

                lock (Form.gr[1])
                {
                    Form.gr[1].AddGraphDot(t, Vx, System.Drawing.Color.Red);
                    Form.gr[1].AddGraphDot(t, Vy, System.Drawing.Color.Blue);
                    Form.gr[1].AddGraphDot(t, V, System.Drawing.Color.Green);
                }

                lock (Form.gr[2])
                {
                    Form.gr[2].AddGraphDot(t, ax, System.Drawing.Color.Red);
                    Form.gr[2].AddGraphDot(t, ay, System.Drawing.Color.Blue);
                    Form.gr[2].AddGraphDot(t, a , System.Drawing.Color.Green);
                }
            }
        }

        public override double[] F(double time, double[] coordinates)
        {
            // x - Y[0], y - Y[1], Vx - Y[2], Vy - Y[3]
            // dx/dt - FY[0] , dy/dt - FY[1] , dVx/dt - FY[2] , dVy/dt - FY[3]

            FY[0] = Y[2];
            FY[1] = Y[3];
            FY[2] = - b * Y[2] / m;
            FY[3] = -g - b * Y[3] / m;

            return FY;
        }
    }
}
