using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace core.Utils
{
    public class Imagems
    {
        public static void SaveByteArrayAsImage(string fullOutputPath, byte[] bytes, string extension)
        {
            
            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                switch (extension)
                {
                    case "image/png":
                        image.Save(fullOutputPath + ".png",ImageFormat.Png);
                        break;
                    case "image/jpeg":
                        image.Save(fullOutputPath + ".jpg", ImageFormat.Jpeg);
                        break;
                    case "image/bmp":
                        image.Save(fullOutputPath + ".bmp", ImageFormat.Bmp);
                        break;
                    default:
                        break;
                }
            }
            
        }

        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

    }
}

