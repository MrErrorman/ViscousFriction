using System;
using System.Threading;

namespace ViscousFriction
{
    class MaterialPoint : CRKMethod
    {
        double g = 9.81, b, b0, m;
        Thread T;

        public MaterialPoint(double m, double V0, double a0, double b) : base(4)
        {
            a0 = a0 * Math.PI / 180.0;
            double[] Y0 = { 0, 0, V0 * Math.Cos(a0), V0 * Math.Sin(a0)};
            SetInit(0, Y0);

            this.m = m;
            this.b = b;
            this.b0 = b;
        }

        public override double[] F(double time, double[] coordinates)
        {
            // x - Y[0], y - Y[1], Vx - Y[2], Vy - Y[3]
            // dx/dt - FY[0] , dy/dt - FY[1] , dVx/dt - FY[2] , dVy/dt - FY[3]

            b = b0 * Math.Exp(Y[1] / 8000);

            FY[0] = Y[2];
            FY[1] = Y[3];
            FY[2] = - b * Y[2] / m;
            FY[3] = -g - b * Y[3] / m;

            return FY;
        }

        public void Step()
        {
            NextStep(0.01);
            CGraph.Add2All(t, Y[0], Y[1], Y[2], Y[3], FY[2], FY[3]);
        }
    }
}
