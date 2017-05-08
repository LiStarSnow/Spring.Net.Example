using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Configuration;


namespace Infrastructure.Upload
{
    /// <summary>
    /// 图片上传类  作者：苗建龙
    /// </summary>
    public class UploadHelper
    {
        private UploadConfig uploadConfig = null;
        private static string vriualRootPath = null;

        public string VriualRootPath
        {
            get
            {
                if (vriualRootPath == null)
                {
                    //计算当前虚拟目录的相对路径  
                    string absRootPath = HttpContext.Current.Server.MapPath("/");
                    vriualRootPath = HttpContext.Current.Server.MapPath("~/");

                    vriualRootPath = vriualRootPath.Replace(absRootPath, "/");
                    vriualRootPath = vriualRootPath.Replace("\\", "/");// \ 变成 /   
                }
                return vriualRootPath;
            }
        }

        public UploadHelper(UploadConfig uploadConfig)
        {
            this.uploadConfig = uploadConfig;
        }

        /// <summary> 
        /// 根据GUID获取16位的唯一字符串 
        /// </summary> 
        /// <param name=\"guid\"></param> 
        /// <returns></returns> 
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static string GetVriualRootPath()
        {
            //计算当前虚拟目录的相对路径  
            string absRootPath = HttpContext.Current.Server.MapPath("/");
            string vriualRootPath = HttpContext.Current.Server.MapPath("~/");

            vriualRootPath = vriualRootPath.Replace(absRootPath, "/");
            vriualRootPath = vriualRootPath.Replace("\\", "/");// \ 变成 / 

            return vriualRootPath;
        }
        /// <summary>
        /// 上传
        /// </summary>
        public UploadResult UpLoad()
        {
            UploadResult rel = new UploadResult();

            var config = this.uploadConfig;
            var file = config.File;

            if (config.AllowFileEmpty)
            {
                if (file.ContentLength == 0)
                {
                    rel.Success = true;
                    return rel;
                }
            }
            else
            {
                if (file.ContentLength == 0)
                {
                    file = null;
                    rel.ErrMsg = "文件不能为空。";
                    return rel;
                }
            }

            //验证大小
            double fileSize = file.ContentLength;
            if (fileSize > config.AllowMaxLength)
            {
                file = null;
                rel.ErrMsg = string.Format("不允许上传超过{0}M的文件", config.AllowMaxLength / 1048576);
                return rel;
            }

            //if (!CheckFileExtension(file) && config.Chunk == 0)
            //{
            //    file = null;
            //    rel.ErrMsg = "不允许上传此格式的文件";
            //    return rel;
            //}

            try
            {
                //var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                rel.Size = file.ContentLength;
                rel.Type = file.ContentType;
                //rel.Original = parsedContentDisposition.FileName.Replace("\"", string.Empty);
                rel.Original = file.FileName;
                rel.Name = (string.IsNullOrEmpty(config.FileGuid) ? GuidTo16String() : config.FileGuid) + "."
                    + rel.Original;

                var subPath = Path.Combine(config.SavePath, config.DatePath);

                rel.Url = string.Format(VriualRootPath + "fmshared/file/GetFile?fileName={0}&outPutName={2}",
                   HttpUtility.UrlEncode(Path.Combine(config.SavePath, config.DatePath, rel.Name)),
                   config.SavePath,
                   HttpUtility.UrlEncode(rel.Original));

                //var savePath = config.AppFilePath + "/" + config.FileClass + subPath;
                var savePath = Path.Combine(config.AppFilePath, subPath);
                var filePath = Path.Combine(savePath, rel.Name);

                var rs = file.InputStream;
                // 计算写入文件的开始位置
                long startPosition = Convert.ToInt32(config.Chunk) * config.ChunkSize;
                // 定义一次接收字节长度
                int bytLen = 4096;
                byte[] nbytes = new byte[bytLen];
                int readSize = rs.Read(nbytes, 0, bytLen);

                // 如何路径不存在，就创建文件路径
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                using (FileStream fs = File.OpenWrite(filePath))
                {
                    if (fs.CanWrite)
                    {
                        fs.Seek(startPosition, SeekOrigin.Current);
                        while (readSize > 0)
                        {
                            fs.Write(nbytes, 0, readSize);
                            readSize = rs.Read(nbytes, 0, bytLen);
                        }
                        fs.Flush();
                        fs.Close();
                        rs.Dispose();
                        rs.Close();
                        nbytes = null;
                    }
                }
                rel.Success = true;
            }
            catch (Exception ex)
            {
                rel.ErrMsg = "上传异常";
            }
            finally
            {
                file = null;
            }
            return rel;
        }

        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool CheckFileExtension(HttpPostedFileBase file)
        {

            //转换成byte,读取图片MIME类型
            Stream stream;
            //int contentLength = file0.ContentLength; //文件长度
            byte[] fileByte = new byte[2];//contentLength，这里我们只读取文件长度的前两位用于判断就好了，这样速度比较快，剩下的也用不到。
            stream = file.InputStream;
            stream.Read(fileByte, 0, 2);//contentLength，还是取前两位
            //stream.Close();

            string fileFlag = "";
            if (fileByte != null && fileByte.Length > 0)//图片数据是否为空
            {
                fileFlag = fileByte[0].ToString() + fileByte[1].ToString();
            }
            string[] fileTypeStr = { "255216", "7173", "6677", "13780" };//对应的图片格式jpg,gif,bmp,png

            if (Enum.IsDefined(typeof(AllowFileExtension), int.Parse(fileFlag)))
            {
                return true;
            }
            //System.IO.File.Delete(filePath);

            return false;

            //var bx = string.Empty;
            //byte buffer;
            //using (var fs = file.InputStream)
            //{
            //    var r = new BinaryReader(fs);
            //    buffer = r.ReadByte();
            //    bx = buffer.ToString();
            //    buffer = r.ReadByte();
            //    bx += buffer.ToString();
            //    r.Close();
            //}

            //if (Enum.IsDefined(typeof(AllowFileExtension), int.Parse(bx)))
            //{
            //    return true;
            //}
            ////System.IO.File.Delete(filePath);

            //return false;
        }

        public void DelFile(string filePath)
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// 允许的文件扩展名
    /// </summary>
    public enum AllowFileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        RAR = 8297,
        DOCX = 8075,
        PSD = 5666,
        XLSX = 208207,
        PDF = 3780
    }
}
