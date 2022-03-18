using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnhancingFoggyImages
{
    public static class Resources
    {
        public static Image[] images = new Image[]
        {
            Image.FromFile(@"../../Resources/Overcast.png"),
            Image.FromFile(@"../../Resources/Foggy.png")
        };
        public static PictureBox selectedImage;
        public static Form2 form;

        public static PictureBox[,] displayedImages;
        public static int m = 4;
        public static int n = 1 + images.Length / m;

        public static void Initialize(Form2 f)
        {
            form = f;
            displayedImages = new PictureBox[n, m];
            for (int i = 0; i < images.Length; i++)
            {
                displayedImages[i / m, i % m] = new PictureBox();
                PictureBox currentImage = displayedImages[i / m, i % m];
                currentImage.Image = images[i];
                currentImage.BackColor = Color.White;
                currentImage.Parent = form;
                currentImage.Location = new Point(5 + i % m * 155, 5 + i / m * 155);
                currentImage.Size = new Size(150, 150);
                currentImage.SizeMode = PictureBoxSizeMode.Zoom;
                currentImage.Click += PictureBox_Click;
            }
            SelectImage(displayedImages[0, 0]);
        }

        private static void PictureBox_Click(object sender, EventArgs e)
        {
            selectedImage.BorderStyle = BorderStyle.None;
            SelectImage(sender as PictureBox);
            Engine.ChangeMainDisplay(selectedImage.Image);
        }

        private static void SelectImage(PictureBox image)
        {
            selectedImage = image;
            image.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
