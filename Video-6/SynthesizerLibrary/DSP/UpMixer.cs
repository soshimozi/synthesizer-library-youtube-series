﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SythesizerLibrary.Core.Audio;
using SythesizerLibrary.Core.Audio.Interface;

namespace SythesizerLibrary.DSP;

public class UpMixer : AudioNode
{
    public UpMixer(IAudioProvider provider) : base(provider, 1, 1)
    {
        Outputs[0].Channels = provider.Channels;
    }

    protected override void GenerateMix()
    {
        var input = Inputs[0];
        var output = Outputs[0];

        var numberOfInputSamples = input.Samples.Count;
        var numberOfOutputSamples = output.Samples.Count;

        if (numberOfInputSamples == numberOfOutputSamples)
        {
            output.Samples.Clear();
            output.Samples.AddRange(input.Samples);
        }
        else
        {
            for (var i = 0; i < numberOfOutputSamples; i++)
            {
                if (numberOfInputSamples == 0)
                {
                    output.Samples[i] = 0;
                }
                else
                {
                    output.Samples[i] = input.Samples[i % numberOfInputSamples];
                }
            }
        }
    }
}