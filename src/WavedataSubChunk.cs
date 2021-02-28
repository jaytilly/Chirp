using System;
using System.IO;
using System.Linq;
using System.Text;

//Modified from https://github.com/ghoofman/WaveLibrary
namespace Chirp
{
    public class WavedataSubChunk
    {
        private readonly byte[] _soundData;
        private readonly string _subChunk2ID = "data";

        public WavedataSubChunk(int NumSamples, int NumChannels, int BitsPerSample, byte[] SoundData)
        {
            Size = NumSamples * NumChannels * (BitsPerSample / 8);
            _soundData = SoundData;
        }

        public int Size { get; }

        public void WriteData(Stream stream)
        {
            //Chunk ID
            var _subChunk2IDData = Encoding.ASCII.GetBytes(_subChunk2ID);
            stream.Write(_subChunk2IDData, 0, _subChunk2IDData.Length);

            //Chunk Size
            var _subChunk2SizeData = BitConverter.GetBytes(Size);
            stream.Write(_subChunk2SizeData, 0, _subChunk2SizeData.Length);

            //Wave Sound Data
            stream.Write(_soundData, 0, _soundData.Length);
        }
    }
}