// See https://aka.ms/new-console-template for more information


using NAudio.Wave;
using SythesizerLibrary.Core.Audio;
using SythesizerLibrary.DSP;
using SythesizerLibrary.Operators;using SythesizerLibrary.Scale;

Console.WriteLine("Hello, World!");


var audioProvider = new WasapiAudioProvider();

//var mainOscillator = new Oscillator(audioProvider, waveShape: WaveShape.Triangle);

//var fmOscillator = new Oscillator(audioProvider, frequency: 2);

//var multiplierAddder = new MulAdd(audioProvider, 20, 440);

//fmOscillator.Connect(multiplierAddder);

//multiplierAddder.Connect(mainOscillator);

//audioProvider.ConnectToOutput(mainOscillator);

const double TuningBaseFrequency = 440d;

var scale = new MajorScale();

var baseNote = 3;
var octave = 3;

var frequency1 = (float)scale.GetFrequency(baseNote, TuningBaseFrequency, 57,  octave);
var frequency2 = baseNote < 5
    ? (float)scale.GetFrequency(12 + (baseNote - 5), TuningBaseFrequency, 57, octave - 1)
    : (float)scale.GetFrequency(baseNote - 5, TuningBaseFrequency, 57, octave);

var osc1 = new Oscillator(audioProvider, frequency1);
var osc2 = new Oscillator(audioProvider, frequency2, WaveShape.Triangle);

var filter = new LP12Filter(audioProvider, frequency1);

osc1.Connect(filter);
osc2.Connect(filter);

var filterLFO = new Oscillator(audioProvider, 4f);
var filterMA = new MulAdd(audioProvider, 350, frequency1);

filterLFO.Connect(filterMA);
filterMA.Connect(filter, 0, 1);

audioProvider.ConnectToOutput(filter);

//var osc1 = new Oscillator(audioProvider, )

audioProvider.Play();


while (true)
{
    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
    {
        break;
    }
}
//Thread.Sleep(2000);
