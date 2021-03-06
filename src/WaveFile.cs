﻿using System.IO;
using System.Linq;

//From https://github.com/ghoofman/WaveLibrary
namespace JaybirdLabs.Chirp
{
    public class WaveFile
    {
        private readonly WavefmtSubChunk _fmt;
        private readonly WaveHeader _header;
        private WavedataSubChunk _data;

        public WaveFile(WavedataSubChunk data)
        {
            _data = data;
        }

        public WaveFile(int channels, int bitsPerSample, int sampleRate)
        {
            _header = new WaveHeader();
            _fmt = new WavefmtSubChunk(channels, bitsPerSample, sampleRate);
        }

        public int NumChannels => _fmt.NumChannels;
        public int BitsPerSample => _fmt.BitsPerSample;

        public void SetData(byte[] SoundData, int numSamples)
        {
            _data = new WavedataSubChunk(numSamples, _fmt.NumChannels, _fmt.BitsPerSample, SoundData);
        }

        public void WriteFile(string file)
        {
            var fs = File.Create(file);

            //Set the total file chunk size
            //Has to be set here because we might not know what the actual Data size was until now
            _header.SetChunkSize(_fmt.Size, _data.Size);

            _header.WriteHeader(fs);
            _fmt.Writefmt(fs);
            _data.WriteData(fs);

            fs.Close();
            fs.Dispose();
        }
    }
}