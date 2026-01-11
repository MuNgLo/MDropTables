using Godot;

namespace MDropTables;

[Tool, GlobalClass]
public partial class DropTableResource : Resource
{
    [Export] public string[] supportedTypes = new string[]{"Resource"};
    [Export] public DropTableEntryResource[] entries;

}// EOF CLASS
