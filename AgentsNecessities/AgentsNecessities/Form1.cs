using System;
using System.Windows.Forms;

namespace AgentsNecessities
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Init(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.Tick();
        }
    }
}
