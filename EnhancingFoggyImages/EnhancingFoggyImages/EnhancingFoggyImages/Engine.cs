using System.Drawing;
using System.Windows.Forms;

namespace EnhancingFoggyImages
{
    public static class Engine
    {
        public static PictureBox mainDisplay;

        public static void Initialize(PictureBox pB1)
        {
            mainDisplay = pB1;
            ChangeMainDisplay(Resources.selectedImage.Image);
        }

        public static void ChangeMainDisplay(Image image)
        {
            mainDisplay.Image = RemoveFog(image);
        }

        public static Image RemoveFog(Image image)
        {
            return image;
        }
    }
}
