﻿using SythesizerLibrary.Core.Audio.Interface;

namespace SythesizerLibrary.Core.Audio;

public class Device : AudioNode
{
    private List<IAudioNode> _nodes;
    private int _writePosition = 0;

    public Device(IAudioProvider provider) : base(provider, 1, 0)
    {
        _nodes = new List<IAudioNode>();
    }

    public void Read(float[] buffer, int offset, int count)
    {
        var input = Inputs[0];

        for (var i = 0; i < count; i += AudioProvider.Channels)
        {
            if (NeedsTraverse)
            {
                _nodes = Traverse(new List<IAudioNode>());
                NeedsTraverse = false;
            }

            for (var j = _nodes.Count - 1; j > 0; j--)
            {
                _nodes[j].Tick();
            }

            MigrateInputSamples();


            for (var j = 0; j < AudioProvider.Channels; j++)
            {
                buffer[offset + i + j] = (float)input.Samples[j];
            }

            _writePosition++;
        }
    }

    public int GetWriteTime()
    {
        return _writePosition;
    }
}