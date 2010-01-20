using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Orc.SmartImage.UI
{
    public class Util
    {
        public static void ShowImage(Image img)
        {
            if (img != null)
            {
                Bitmap map = img as Bitmap;
                Image<Bgr, byte> image = new Image<Bgr, byte>(map);
                ImageViewer iv = new ImageViewer(image);
                iv.StartPosition = FormStartPosition.CenterParent;
                iv.ShowDialog();
            }
        }
    }
}
