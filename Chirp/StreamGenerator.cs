using System.IO;

namespace Chirp
{
    public class StreamGenerator
    {
        private readonly SignalGenerator _signalGenerator;

        public StreamGenerator(int sampleRate = 44100, int channels =2)
            :this(new SignalGenerator(sampleRate,channels))
        {

        }

        public StreamGenerator(SignalGenerator signalGenerator)
        {
            _signalGenerator = signalGenerator ?? throw new System.ArgumentNullException(nameof(signalGenerator));
        }

        public Stream GenerateStream(SignalGeneratorType signalType, int frequency, int durationInSeconds)
        {
            _signalGenerator.Frequency = frequency;
            _signalGenerator.Gain = 1;
            _signalGenerator.Type = signalType;

            int bufferSize = _signalGenerator.WaveFormat.AverageBytesPerSecond * durationInSeconds;
            int sampleCount = (_signalGenerator.WaveFormat.SampleRate * durationInSeconds;

            short[] waveShortData = new short[bufferSize];
            _signalGenerator.Read(waveShortData, bufferSize);
            byte[] waveByteData = waveShortData.ToByteArray();

            var waveStream = new WaveMemoryStream(_signalGenerator.WaveFormat);
            waveStream.SetData(waveByteData, sampleCount);

            return waveStream.CreateStream();
        }
    }
}
