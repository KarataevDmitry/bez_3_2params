using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace besierTest
{
    public partial class Form1 : Form
    {
        private const int N = 100;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            BesierCurve bc = new BesierCurve();
            PointF[ ] SinGraph = new PointF[ 100 ];
            PointF[ ] BesierGraph = new PointF[ 100 ];
            for ( int i = 0 ; i < N ; i++ )
            {
                float h = 1f / N;
                float x_i = i * h;
                SinGraph[ i ] = new PointF( x_i, (float) Math.Sin( Math.PI * x_i ) );
                var g = pbGraph.CreateGraphics();
                g.DrawLines(Pens.Red, SinGraph);
            }

        }
    }
}
