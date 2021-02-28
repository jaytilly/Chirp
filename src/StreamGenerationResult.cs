using System.IO;

namespace JaybirdLabs.Chirp
{
    public struct StreamGenerationResult
    {
        public short[] Amplitudes { get; set; }

        public Stream WaveStream { get; set; }
    }
}
