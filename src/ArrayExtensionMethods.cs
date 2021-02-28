using System;
using System.Linq;

namespace Chirp
{
    public static class ArrayExtensionMethods
    {
        public static byte[] ToByteArray(this short[] shortArray)
        {
            var bufferBytes = new byte[shortArray.Length * 2];
            Buffer.BlockCopy(shortArray, 0, bufferBytes, 0,
                bufferBytes.Length);

            return bufferBytes;
        }
    }
}