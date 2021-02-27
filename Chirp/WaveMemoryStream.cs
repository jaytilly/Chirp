using System.IO;
using System.Linq;

namespace Chirp
{
    public class WaveMemoryStream
    {
        private readonly WavefmtSubChunk _fmt;
        private readonly WaveHeader _header;
        private WavedataSubChunk _data;

        public WaveMemoryStream(WaveFormat waveFormat)
            : this(waveFormat.Channels, waveFormat.BitsPerSample, waveFormat.SampleRate)
        {
        }

        public WaveMemoryStream(int channels, int bitsPerSample, int sampleRate)
        {
            _header = new WaveHeader();
            _fmt = new WavefmtSubChunk(channels, bitsPerSample, sampleRate);
        }

        public WaveMemoryStream(WavedataSubChunk data)
        {
            _data = data;
        }

        public int NumChannels => _fmt.NumChannels;

        public int BitsPerSample => _fmt.BitsPerSample;

        public void SetData(byte[] soundData, int numSamples)
        {
            _data = new WavedataSubChunk(numSamples, _fmt.NumChannels, _fmt.BitsPerSample, soundData);
        }

        public MemoryStream CreateStream()
        {
            var memStream = new MemoryStream();

            _header.SetChunkSize(_fmt.Size, _data.Size);

            _header.WriteHeader(memStream);
            _fmt.Writefmt(memStream);
            _data.WriteData(memStream);

            memStream.Seek(0, SeekOrigin.Begin);

            return memStream;
        }
    }
}