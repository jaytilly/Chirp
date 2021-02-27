using System;
using System.IO;
using System.Linq;
using System.Text;

//from https://github.com/ghoofman/WaveLibrary
namespace Chirp
{
    public class WavefmtSubChunk
    {
        private readonly int _audioFormat = 1; //For no compression
        private readonly int _blockAlign;
        private readonly int _byteRate;
        private readonly int _sampleRate;
        private readonly string _subChunk1ID = "fmt ";

        public WavefmtSubChunk(int channels, int bitsPerSamples, int sampleRate)
        {
            BitsPerSample = bitsPerSamples;
            NumChannels = channels;

            _sampleRate = sampleRate;
            _byteRate = _sampleRate * NumChannels * (BitsPerSample / 8);
            _blockAlign = NumChannels * (BitsPerSample / 8);
        }

        public int NumChannels { get; } //1 For Mono, 2 For Stereo
        public int BitsPerSample { get; }

        public int Size { get; } = 16;

        public void Writefmt(Stream stream)
        {
            //Chunk ID
            var _subchunk1IDData = Encoding.ASCII.GetBytes(_subChunk1ID);
            stream.Write(_subchunk1IDData, 0, _subchunk1IDData.Length);

            //Chunk Size
            var _subchunk1SizeData = BitConverter.GetBytes(Size);
            stream.Write(_subchunk1SizeData, 0, _subchunk1SizeData.Length);

            //Audio Format (PCM)
            var _audioFormatData = BitConverter.GetBytes(_audioFormat);
            stream.Write(_audioFormatData, 0, 2);

            //Number of Channels (1 or 2)
            var _numChannelsData = BitConverter.GetBytes(NumChannels);
            stream.Write(_numChannelsData, 0, 2);

            //Sample Rate
            var _sampleRateData = BitConverter.GetBytes(_sampleRate);
            stream.Write(_sampleRateData, 0, _sampleRateData.Length);

            //Byte Rate
            var _byteRateData = BitConverter.GetBytes(_byteRate);
            stream.Write(_byteRateData, 0, _byteRateData.Length);

            //Block Align
            var _blockAlignData = BitConverter.GetBytes(_blockAlign);
            stream.Write(_blockAlignData, 0, 2);

            //Bits Per Sample
            var _bitsPerSampleData = BitConverter.GetBytes(BitsPerSample);
            stream.Write(_bitsPerSampleData, 0, 2);
        }
    }
}