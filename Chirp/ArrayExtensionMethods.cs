using System;

namespace Chirp
{
    public static class ArrayExtensionMethods
    {
        public static byte[] ToByteArray(this short[] shortArray)
        {
            byte[] bufferBytes = new byte[shortArray.Length * 2];
            Buffer.BlockCopy(shortArray, 0, bufferBytes, 0,
                bufferBytes.Length);

            return bufferBytes;
        }
    }
}
