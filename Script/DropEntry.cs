#if TOOLS
using Godot;
using System;

namespace MDropTables;

public partial class DropEntry
{
    private float weight;
    private Resource resource;
    private float percentage = 0.0f;
    public float Weight { get => weight; set => weight = value; }
    public float Percentage { get => percentage; set => percentage = value; }
    public Resource Drop { get => resource; set => resource = value; }
    public string Name => resource.ResourceName;
    
    public DropEntry(float weight, Resource resource)
    {
        this.weight = weight;
        this.resource = resource;
    }
}// EOF CLASS
#endif



