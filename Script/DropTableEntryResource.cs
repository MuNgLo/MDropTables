using Godot;

namespace MDropTables;

[Tool, GlobalClass]
public partial class DropTableEntryResource : Resource
{
    [Export] public float weight = 0.0f;
    [Export] public Resource drop;
}// EOF CLASS