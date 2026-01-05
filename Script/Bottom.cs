#if TOOLS
using Godot;
using System;

namespace MDropTables;
[Tool]
public partial class Bottom : PanelContainer
{
	[Export] private DropTableDock tableDock;
	[Export] private TextureButton btnAddEntry;

    public override void _Ready()
	{
		btnAddEntry.Pressed += tableDock.WhenAddEntryBtnPressed;
	}
}//EOF CLASS
#endif