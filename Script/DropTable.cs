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

    public DropTable(DropTableResource dropTableResource)
    {
        entries = new List<DropEntry>();
        totalWeight = 0.0f;
        foreach (DropTableEntryResource entry in dropTableResource.entries)
        {
            Add(new DropEntry(entry.weight, entry.drop), false);
        }
        RecalculateTotalWeight();
    }

    public void Add(DropEntry dropEntry, bool recalculateAfterAdd = true)
    {
        if (dropEntry is not null) { entries.Add(dropEntry); }
        if (recalculateAfterAdd) { RecalculateTotalWeight(); RecalculatePercentage(); }
    }


    public void RecalculateTotalWeight()
    {
        totalWeight = 0;
        foreach (DropEntry entry in entries)
        {
            totalWeight += entry.Weight;
        }
    }
    /// <summary>
    /// This is only calculating the percentage, which is<br/>
    /// only needed and used for debugging and visualization of the drop percentage.<br/>
    /// In runtime this should be avoided.
    /// </summary>
    public void RecalculatePercentage()
    {

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
        if (entries.Exists(p => p.Name == name))
        {
            return entries.Find(p => p.Name == name);
        }
        return null;
    }
    /// <summary>
    /// Might return NULL if the random roll generation is off<br/>
    /// Will print a warning if it does<br/>
    /// But it really shouldn't happen, ever
    /// </summary>
    /// <returns></returns>
    internal DropEntry GetDrop()
    {
        float roll = (float)GD.RandRange(0.0f, totalWeight);
        float accWeight = 0.0f;
        foreach (DropEntry entry in entries)
        {
            accWeight += entry.Weight;
            if (roll <= accWeight)
            {
                return entry;
            }
        }
        GD.PushWarning($"DropTable::GetDrop() roll[{roll}] totalWEight[{totalWeight}] Roll failed to be under the total accumulated weight!");
        return null;
    }
}// EOF CLASS
#endif