using SythesizerLibrary.Tuning;

namespace SythesizerLibrary.Scale;

public class Scale
{
    private readonly List<int> _degrees;

    protected Scale(List<int> degrees)
    {
        _degrees = degrees;
    }

    public double GetFrequency(int degree, double rootFrequency, int baseNoteIndex, int octave)
    {
        var frequency = rootFrequency;
        var interval = _degrees[degree];

        var noteIndex = (interval + (octave * 12)) - baseNoteIndex;

        var twelfthRootOf2 = Math.Pow(2, 1.0 / 12.0);
        var calculatedFrequency = rootFrequency * Math.Pow(twelfthRootOf2, noteIndex);
        return Math.Round(calculatedFrequency, 2);
    }
}