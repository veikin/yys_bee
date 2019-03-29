using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace YYS_Bee.Imp
{
    public  class ImageTools
    {
        /// <summary>
        /// 存储图片到本地
        /// </summary>
        /// <param name="path"></param>
        /// <param name="image"></param>
        public static string SaveImage(string path,string fileName,Bitmap image)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string imagePath = Path.Combine(path, fileName);
            image.Save(imagePath, ImageFormat.Png);

            return imagePath;
        }
    }
}
