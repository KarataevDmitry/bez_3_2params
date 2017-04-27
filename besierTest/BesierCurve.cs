using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace besierTest
{
    class BesierCurve
    {
        private double fun( double x )
        {
            return Math.Sin( Math.PI * x );
        }
        public void xi_finder( int n, out double xi1, out double xi2 )
        {
            double t_i, x_i, tau;

            double h = 1d / n;
            double A = 0;
            double B = 0;
            double C = 0;
            double D = 0;
            double E = 0;
            double F = 0;
            for ( int i = 1 ; i < n - 1 ; i++ )
            {
                t_i = i * h;
                x_i = t_i;
                tau = t_i * ( 1 - t_i );
                A += t_i * Math.Pow( 1 - t_i, 4 );
                B += Math.Pow( t_i, 2 ) * Math.Pow( 1 - t_i, 3 );
                D += Math.Pow( tau, 3 );
                E += Math.Pow( t_i, 4 ) * Math.Pow( 1 - t_i, 2 );

                C += fun( t_i ) * tau * ( 1 - t_i );
                F += fun( t_i ) * tau * t_i;
            }
            xi2 = ( F * A - D * C ) / ( 3 * ( B * D + E * A ) );
            xi1 = ( C - 3 * xi2 * B ) / ( 3 * A );

        }
        public double bez3_point( double t, double A, double B, double C, double D )
        {
            double AB = A + ( B - A ) * t;
            double BC = B + ( C - B ) * t;
            double CD = C + ( D - C ) * t;

            double ABC = AB + ( BC - AB ) * t;
            double BCD = BC + ( CD - BC ) * t;

            double ABCD = ABC + ( BCD - ABC ) * t;

            return ABCD;
        }
    }
}
