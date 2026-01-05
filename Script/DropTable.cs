#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

namespace MDropTables;

public partial class DropTable
{
    private List<DropEntry> entries;
    private float totalWeight;

    public float TotalWeight => totalWeight;

    public int Count => entries.Count;
    public DropTable()
    {
        entries = new List<DropEntry>();
        totalWeight = 0.0f;
    }
    public void Add(DropEntry dropEntry, bool recalculateAfterAdd = true)
    {
        if (dropEntry is not null) { entries.Add(dropEntry); }
        if (recalculateAfterAdd) { RecalculateTable(); }
    }

    public void RecalculateTable()
    {
        totalWeight = 0;
        foreach (DropEntry entry in entries)
        {
            totalWeight += entry.Weight;
        }
        if (totalWeight > 0)
        {
            // Update percentages
            foreach (DropEntry entry in entries)
            {
                entry.Percentage = entry.Weight / totalWeight * 100;
            }
        }
    }

    internal DropEntry Get(StringName name)
    {
        if(entries.Exists(p=>p.Name == name))
        {
            return entries.Find(p=>p.Name == name);
        }
        return null;
    }

    internal DropEntry GetDrop()
    {
        float roll = (float)GD.RandRange(0.0f, totalWeight);
        float accWeight = 0.0f;
        foreach (DropEntry entry in entries)
        {
            accWeight += entry.Weight;
            if(roll <= accWeight)
            {
                return entry;
            }
        }
        return null;
    }
}// EOF CLASS
#endif