#if TOOLS

using Godot;

namespace MDropTables;

[Tool]
public partial class MDropTables : EditorPlugin
{
    private Control dock;
    public override void _EnterTree()
    {
        
        dock = ResourceLoader.Load<PackedScene>("res://addons/MDropTables/Scenes/Dock.tscn").Instantiate<Control>();
        AddControlToDock(DockSlot.RightBl, dock);
    }
    public override void _ExitTree()
    {
        RemoveControlFromDocks(dock);
        dock.QueueFree();
    }

    public override bool _HasMainScreen()
    {
        return false;
    }
    public override string _GetPluginName()
    {
        return "MuNgLo's Drop Tables";
    }
    //public override Texture2D _GetPluginIcon()
    //{
    //    Texture2D icon = ResourceLoader.Load("res://addons/MDunGen/Icons/AddonIcon.png") as Texture2D;
    //    return icon;
    //}
    public override void _MakeVisible(bool visible)
    {
        if(dock is not null){ dock.Visible = visible; }
    }
}// EOF CLASS
#endif