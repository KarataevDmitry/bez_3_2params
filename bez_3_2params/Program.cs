using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.CSV;


namespace bez_3_2params
{
    class Program
    {
        static void xi_finder(int n, out double ksi1, out double ksi2 )
        {
            double t_i, x_i;

            double h = 1d / n;
            double[ , ] A = new double[ 2, 2 ];
            double[ ] F = new double[ 2 ];
            double fun0 = fun( 0d );
            double fun1 = fun( 1d );
            for ( int i = 0 ; i < n ; i++ )
            {
                t_i = i * h; x_i = t_i;
                double tau = ( 1 - t_i );
                double tau2 = tau*tau;
                double t_i2 = t_i * t_i;
                double t_i3 = t_i2 * t_i;
                A[ 0, 0 ] += 3*t_i3 * tau2 * tau2; //3t^3*(1-t)^4
                A[ 0, 1 ] += 3*t_i3 * tau2 * tau; //3t^3*(1-t)^3;
                A[ 1, 1 ] = 3 * t_i3 * t_i2 * tau2; //3t^5*(1-t)^2;
                
                F[ 0 ] += -( fun0 * t_i * tau2 * tau2 * tau + fun1 * t_i2 * t_i2 * tau2 - fun( t_i ) * t_i* tau2 );
                F[ 1 ] += -( fun0 * t_i2 * tau2 * tau2 + fun1 * t_i3 * tau - fun( t_i ) * t_i2 * tau );
            }
            

            ksi2 = (( A[ 1, 1 ] * F[ 1 ] - A[ 0, 1 ] * F[ 0 ] ) / ( A[ 1, 1 ] * A[ 1, 1 ] - A[ 0, 1 ] * A[ 0, 1 ] ));
            ksi1 = ( F[ 0 ] - A[ 0, 1 ] * ksi2 ) / (A[ 1, 1 ]);

        }

        private static double fun( double x )
        {
            return Math.Sin(Math.PI*x);
        }

        const int N = 100;
        static void Main( string[ ] args )
        {
            CsvExport csv = new CsvExport();
            int n = N;
            double xi1, xi2;
            xi_finder( n, out xi1, out xi2 );
            Console.WriteLine( $" xi1 = {xi1}" );
            Console.WriteLine( $" xi2 = {xi2}" );
            integrate( xi1, xi2 );
            double t_i, x_i;
            double h = 1d / n;

            double delta;
            double z = 0;
            for ( int i = 0 ; i <= n  ; i++ ) 
            {
                t_i = i * h;
                x_i = t_i;
                csv.AddRow();
                csv[ "x" ] = x_i;
                csv[ "B(t)" ] = bez3_point( t_i, fun(0), xi1, xi2, fun(1) );
                csv[ "f(x)" ] = fun( x_i );
                delta = Math.Abs( bez3_point( t_i, fun(0), xi1, xi2, fun(1)) - fun( x_i ) );
                if ( delta > z )
                {
                    z = delta;
                }
            }
            Console.WriteLine( $" z = {z}" );
            csv.ExportToFile( "data.csv" );
            Console.WriteLine( $"int = {integrate( xi1, xi2 )}" );
            Console.ReadLine();
        }

        private static double integrate( double xi1, double xi2 )
        {
            var intg = 6*xi1/4+2*xi1+3*xi1/2+xi2-0.25 * fun( 0 );
            return intg;
        }

        private static double bez3_point( double t, double P0, double P1, double P2, double P3 )
        {
            double P0P1 = P0 + ( P1 - P0 ) * t;
            double P1P2 = P1 + ( P2 - P1 ) * t;
            double P2P3 = P2 + ( P3 - P2 ) * t;

            double P0P1P2 = P0P1 + ( P1P2 - P0P1 ) * t;
            double P1P2P3 = P1P2 + ( P2P3 - P1P2 ) * t;

            double P0P1P2P3 = P0P1P2 + ( P1P2P3 - P0P1P2 ) * t;

            return P0P1P2P3;
        }
    }
}
