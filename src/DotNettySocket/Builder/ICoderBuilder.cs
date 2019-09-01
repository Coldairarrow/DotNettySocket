using DotNetty.Buffers;

namespace Coldairarrow.DotNettySocket
{
    /// <summary>
    /// 包含编码器和解码器的构建者
    /// </summary>
    /// <typeparam name="TBuilder">特定构建着</typeparam>
    public interface ICoderBuilder<TBuilder>
    {
        /// <summary>
        /// 设置基于长度的解码器,解决粘包与分包问题
        /// </summary>
        /// <param name="maxFrameLength">最大长度</param>
        /// <param name="lengthFieldOffset">长度字段偏移量</param>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <param name="lengthAdjustment">添加到长度字段的补偿值</param>
        /// <param name="initialBytesToStrip">从解码帧中开始去除的字节数</param>
        /// <param name="byteOrder">长度字节排序,默认为BigEndian,BigEndian:大端序,高位在前低位在后;LittleEndian:小端序,低位在前高位在后</param>
        /// <returns></returns>
        TBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip, ByteOrder byteOrder = ByteOrder.BigEndian);

        /// <summary>
        /// 设置基于长度的编码器,解决粘包与分包问题
        /// </summary>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <returns></returns>
        TBuilder SetLengthFieldEncoder(int lengthFieldLength);
    }
}
