namespace SythesizerLibrary.Tuning;

public class TuningBase
{
    public double OctaveRatio { get; }
    public List<double> Ratios { get; }

    private readonly List<double> _semitones;

    protected TuningBase(List<double> semitones, double octaveRatio = 2)
    {
        _semitones = semitones;
        OctaveRatio = octaveRatio;
        Ratios = new List<double>();

        var tuningLength = _semitones.Count;
        for (var i = 0; i < tuningLength; i++)
        {
            Ratios.Add(Math.Pow(2, _semitones[i] / tuningLength));
        }
    }
}