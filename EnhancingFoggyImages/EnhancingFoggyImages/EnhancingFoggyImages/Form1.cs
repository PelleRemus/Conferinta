using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnhancingFoggyImages
{
    public partial class Form1 : Form
    {
        Form2 chooseImageForm = new Form2();

        public Form1()
        {
            InitializeComponent();
            Resources.Initialize(chooseImageForm);
            Engine.Initialize(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chooseImageForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(pictureBox1.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Size = new Size(999, 600);
            } 
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            }
            pictureBox1.Location = new Point(button1.Left-360, button1.Bottom);
        }
    }
}
