using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace Spring.Net.Example.Core
{
    /// <summary>
    /// QR二维码生成和解析
    /// </summary>
    public static class QRCodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GenerateQRCode(string serverPath,string strData)
        {
            string path = string.Empty;
            if(!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Image image = qrCodeEncoder.Encode(strData);
            string filenName = DateTime.Now.ToString("yyyyMMddHHmmssfff").ToString() + ".jpg";
            path = "/Upload/" + filenName;
            string filePath = serverPath+ filenName;
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();
            image.Dispose();
            return path;
        }
        /// <summary>
        /// 解析二维码图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string QRCodeDecode(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return string.Empty;
            Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }
    }
}
