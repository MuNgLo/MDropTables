#if TOOLS
using Godot;
using System;

namespace MDropTables;

public partial class DropEntry
{
    private string name;
    private float weight;
    private float percentage = 0.0f;
    public float Weight { get => weight; set => weight = value; }
    public float Percentage { get => percentage; set => percentage = value; }
    public string Name => name;
    
    public DropEntry(string name, float weight)
    {
        this.name = name;
        this.weight = weight;
    }
}// EOF CLASS
#endif



