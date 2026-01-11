#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

namespace MDropTables;

[Tool]
public partial class SupportedTypes : GridContainer
{
	[Export] private DropTableDock tableDock;
	[Export] private Button btnToggleVis;
	[Export] private TextureButton btnAddEntry;
	[Export] private PackedScene entryPrefab;
	[Export] private GridContainer entries;

	public DropTableDock Dock => tableDock;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnToggleVis.Pressed += ToggleVis;
		btnAddEntry.Pressed += WhenAddPressed;
		entries.Hide();
	}

	private void ToggleVis()
	{
		if (!tableDock.HasTableAssigned) { entries.Hide();return;}
		if (entries.Visible)
		{
			btnToggleVis.Text = "> Supported Types";
			entries.Hide();
		}
		else
		{
			btnToggleVis.Text = "^ Supported Types";
			entries.Show();
		}
	}

	private void WhenAddPressed()
	{
		entries.RemoveChild(btnAddEntry.GetParent());
		AddNewEntry("");
		entries.AddChild(btnAddEntry.GetParent());
	}

	private void AddNewEntry(string typeName)
	{
		SupportedTypeEntry newEntry = entryPrefab.Instantiate<SupportedTypeEntry>();
		newEntry.Setup(this, typeName);
		entries.AddChild(newEntry);

	}
	public void ClearEntries()
	{
		entries.RemoveChild(btnAddEntry.GetParent());
		foreach (Node child in entries.GetChildren())
		{
			child.QueueFree();
		}
		entries.AddChild(btnAddEntry.GetParent());
	}
	public void ReBuildEntries(string[] supportedTypes)
	{
		ClearEntries();
		entries.RemoveChild(btnAddEntry.GetParent());
		foreach (string typeName in supportedTypes)
		{
			AddNewEntry(typeName);
		}
		entries.AddChild(btnAddEntry.GetParent());
	}
}// EOF CLASS
#endif