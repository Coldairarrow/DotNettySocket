using DotNetty.Buffers;
using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Coldairarrow.DotNettySocket
{
    static class Extention
    {
        /// <summary>
        /// 转为网络终结点IPEndPoint
        /// </summary>=
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static IPEndPoint ToIPEndPoint(this string str)
        {
            IPEndPoint iPEndPoint = null;
            try
            {
                string[] strArray = str.Split(':').ToArray();
                string addr = strArray[0];
                int port = Convert.ToInt32(strArray[1]);
                iPEndPoint = new IPEndPoint(IPAddress.Parse(addr), port);
            }
            catch
            {
                iPEndPoint = null;
            }

            return iPEndPoint;
        }

        /// <summary>
        /// 获取IByteBuffer中的byte[]
        /// </summary>
        /// <param name="byteBuffer">IByteBuffer</param>
        /// <returns></returns>
        public static byte[] ToArray(this IByteBuffer byteBuffer)
        {
            int readableBytes = byteBuffer.ReadableBytes;
            if (readableBytes == 0)
            {
                return ArrayExtensions.ZeroBytes;
            }

            if (byteBuffer.HasArray)
            {
                return byteBuffer.Array.Slice(byteBuffer.ArrayOffset + byteBuffer.ReaderIndex, readableBytes);
            }

            var bytes = new byte[readableBytes];
            byteBuffer.GetBytes(byteBuffer.ReaderIndex, bytes);
            return bytes;
        }

    }
}
