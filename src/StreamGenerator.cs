using System;
using System.IO;

namespace JaybirdLabs.Chirp
{
    public class StreamGenerator
    {
        private readonly SignalGenerator _signalGenerator;

        public StreamGenerator(int sampleRate = 44100, int channels = 2)
            : this(new SignalGenerator(sampleRate, channels))
        {
        }

        public StreamGenerator(SignalGenerator signalGenerator)
        {
            _signalGenerator = signalGenerator ?? throw new ArgumentNullException(nameof(signalGenerator));
        }

        public StreamGenerationResult GenerateStream(SignalGeneratorType signalType, int frequency, int durationInSeconds)
        {
            _signalGenerator.Frequency = frequency;
            _signalGenerator.Gain = 1;
            _signalGenerator.Type = signalType;

            var bufferSize = _signalGenerator.WaveFormat.AverageBytesPerSecond * durationInSeconds;
            var sampleCount = _signalGenerator.WaveFormat.SampleRate * durationInSeconds;

            var waveShortData = new short[bufferSize];
            _signalGenerator.Read(waveShortData, bufferSize);
            var waveByteData = waveShortData.ToByteArray();

            var waveStream = new WaveMemoryStream(_signalGenerator.WaveFormat);
            waveStream.SetData(waveByteData, sampleCount);

            return new StreamGenerationResult
            {
                Amplitudes = waveShortData,
                WaveStream = waveStream.CreateStream()
            };
        }
    }
}