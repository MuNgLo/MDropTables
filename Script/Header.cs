#if TOOLS
using Godot;
using System;

namespace MDropTables;
[Tool]
public partial class Header : PanelContainer
{
	[Export] private Label lblTotalWeight;
	[Export] private Control resourceControl;
	[Export] private DropTableDock tableDock;

	private EditorResourcePicker resource;
	public float TotalWeight { set => lblTotalWeight.Text = value.ToString("0.00"); }

	public DropTableResource Table => resource.EditedResource as DropTableResource;

    public override void _Ready()
	{
		InsertResourcePicker();
	}

	private void InsertResourcePicker()
	{
		resource = new MyResPicker();
		resource.Editable = true;
		resource.ToggleMode = false;
		resource.BaseType = "DropTableResource";
		resource.ResourceChanged += (r) => tableDock.LoadResource(r as DropTableResource);
		resource.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		resource.SetSize(new Vector2(0,0));
 		resourceControl.AddChild(resource);
	}

	public partial class MyResPicker : EditorResourcePicker
	{
        public override void _SetCreateOptions(GodotObject menuNode)
        {
            base._SetCreateOptions(menuNode);
			//(menuNode as PopupMenu).RemoveItem(0);
        }
	}
}//EOF CLASS
#endif