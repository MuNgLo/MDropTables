#if TOOLS
using Godot;
using System;

namespace MDropTables;

[Tool]
public partial class DropEntryNode : PanelContainer
{
	[Export] private TextureButton btnRemove;
	[Export] private SpinBox spnWeight;
	[Export] private RichTextLabel rtxDropChance;
	[Export] private Label dropEntryName;

	[Export] private Control resourceControl;
	private EditorResourcePicker resource;
	public Resource DropResource
	{
		get => resource.EditedResource;
		set
		{
			resource.EditedResource = value;
			if (value is not null)
			{
				Name = value.ResourceName;
				dropEntryName.Text = value.ResourceName;
			}
		}
	}

	public float Weight { get => (float)spnWeight.Value; set => spnWeight.SetValueNoSignal(value); }
	public float Percentage
	{
		set
		{
			if (value < 1 || value > 99)
			{
				rtxDropChance.Text = $"[color=red]{value.ToString("0.00")}%[/color]";
			}
			else
			{
				rtxDropChance.Text = value.ToString("0.00") + "%";
			}
		}
	}

	private DropTableDock tableDock;

	public override void _Ready()
	{
		//GD.Print("DropEntry::_Ready()");
		tableDock = GetParent().GetParent<DropTableDock>();
		btnRemove.Pressed += () => tableDock.WhenRemoveEntryBtnPressed(this);
		spnWeight.ValueChanged += (value) => tableDock.WhenWeightChange(this);
		InsertResourcePicker();
	}

	private void InsertResourcePicker()
	{
		resource = new EditorResourcePicker();
		resource.Editable = true;
		resource.ToggleMode = true;
		resource.BaseType = tableDock.SupportedTypes;
		resource.ResourceChanged += WhenResourceChanged;
		resource.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		resource.SetSize(new Vector2(0, 0));
		resourceControl.AddChild(resource);
	}

	private void WhenResourceChanged(Resource resource)
	{
		Name = resource.ResourceName;
		dropEntryName.Text = Name;
		tableDock.WhenEntryChanged(this);
	}
}// EOF CLASS
#endif