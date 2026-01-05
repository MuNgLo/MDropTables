#if TOOLS
using Godot;
using System;
using System.Collections.Generic;

namespace MDropTables;

[Tool]
public partial class DropTableDock : Control
{
	[Export] private Button btnStartTest;
	[Export] private GridContainer entries;
	[Export] private Header header;
	[Export] private Bottom bottom;
	[Export] private Control footer;
	[Export] private PackedScene entryPrefab;

	private bool isTesting = false;
	public override void _Ready()
	{
		btnStartTest.Pressed += WhenStartTestPressed;
	}

	public void LoadResource(DropTableResource resource)
	{
		GD.Print($"DropTableDock::LoadResource() resource is null [{resource is null}]");
		ClearTable();
		if (resource is null) { return; }
		LoadTable();
		RecalculateTable();
	}

	private void ClearTable()
	{
		foreach (Node child in entries.GetChildren())
		{
			if (child is DropEntryNode entry)
			{
				child.QueueFree();
			}
		}
	}

	private void WhenStartTestPressed()
	{
		if (!isTesting)
		{
			StartTest(10000);
		}
	}

	private void StartTest(int size)
	{
		if (isTesting) { return; }
		isTesting = true;
		DropTable dTable = new();
		Dictionary<string, int> testResults = new Dictionary<string, int>();
		foreach (Node child in entries.GetChildren())
		{
			if (child is DropEntryNode entry)
			{
				dTable.Add(new DropEntry(entry.Name, entry.Weight), false);
				if (!testResults.ContainsKey(entry.Name)) { testResults[entry.Name] = 0; }
			}
		}
		dTable.RecalculateTable();
		if (dTable.Count < 1)
		{
			GD.PrintRich($"[color=blue]Drop table Test[/color] Aborted. Table has no items");
			isTesting = false;
			return;
		}
		for (int i = 0; i < size; i++)
		{
			DropEntry drop = dTable.GetDrop();
			if (drop is null)
			{
				GD.PrintErr($"DropTableDock::StartTest() Failed to get drop. Iteration [{i}] returned NULL!");
				continue;
			}
			testResults[drop.Name]++;
		}

		// Present results
		GD.Print("----------------------------------------------");
		GD.PrintRich($"[color=blue]Drop table Test[/color] Simulating {size} drops");
		GD.Print("----------------------------------------------");
		GD.PrintRich($"[color=yellow]CHANCE[/color] [Dropped] [Expected] [Name]");

		foreach (Node child in entries.GetChildren())
		{
			if (child is DropEntryNode entry)
			{
				DropEntry drop = dTable.Get(entry.Name);
				if (drop is null)
				{
					GD.PrintRich($"[color=red]{child.Name}[/color]  [{testResults[drop.Name].ToString("0000")}] DropTable.Get() returned NULL");
				}
				else
				{
					GD.PrintRich($"[color=yellow]{drop.Percentage.ToString("00.00")}%[/color]  [{testResults[drop.Name].ToString("0000")}]    [{(size * (drop.Weight / dTable.TotalWeight)).ToString("0000")}]   => {drop.Name}");
				}
			}
		}
		GD.Print("----------------------------------------------");
		isTesting = false;
	}

	private void RecalculateTable()
	{
		float totalWeight = 0;
		foreach (Node child in entries.GetChildren())
		{
			if (child is DropEntryNode entry)
			{
				totalWeight += entry.Weight;
			}
		}
		header.TotalWeight = totalWeight;
		if (totalWeight > 0)
		{
			// Update percentages
			foreach (Node child in entries.GetChildren())
			{
				if (child is DropEntryNode entry && entry.Weight > 0)
				{
					entry.Percentage = entry.Weight / totalWeight * 100;
				}
			}
		}
	}
	public void WhenWeightChange(DropEntryNode dropEntry)
	{
		RecalculateTable();
		SaveTable();
	}


	public void WhenAddEntryBtnPressed()
	{
		AddNewEntry();
		SaveTable();
	}
	public void WhenRemoveEntryBtnPressed(DropEntryNode entry)
	{
		ConfirmationDialog confirm = new ConfirmationDialog();
		confirm.Title = $"Remove {entry.Name}";
		confirm.DialogText = $"Are you sure you want to permanently remove {entry.Name} from the Drop Table?";

		confirm.Confirmed += () =>
		{
			RemoveEntry(entry);
			confirm.QueueFree();
		};
		confirm.Canceled += () =>
		{
			confirm.QueueFree();
		};
		AddChild(confirm);
		confirm.PopupCentered();
		confirm.Show();
	}

	private void RemoveEntry(DropEntryNode entry)
	{
		entries.RemoveChild(entry);
		entry.QueueFree();
		SaveTable();
	}

	private DropEntryNode AddNewEntry()
	{
		if(header.Table is null){ return null; }
		DropEntryNode newEntry = entryPrefab.Instantiate<DropEntryNode>();
		entries.RemoveChild(bottom);
		entries.RemoveChild(footer);
		entries.AddChild(newEntry, true);
		entries.AddChild(bottom);
		entries.AddChild(footer);
		SaveTable();
		return newEntry;
	}

	private void SaveTable()
	{
		List<DropEntryNode> dropNodes = new List<DropEntryNode>();

		foreach (Node child in entries.GetChildren())
		{
			if (child is DropEntryNode entry)
			{
				dropNodes.Add(entry);
			}
		}

		DropTableEntryResource[] entryResources = new DropTableEntryResource[dropNodes.Count];
		for (int i = 0; i < dropNodes.Count; i++)
		{
			entryResources[i] = new DropTableEntryResource()
			{
				weight = dropNodes[i].Weight,
				drop = dropNodes[i].DropResource
			};
		}
		if(header.Table is null){ return; }
		header.Table.entries = entryResources;
		ResourceSaver.Save(header.Table);
	}
	private void LoadTable()
	{
		if(header.Table is null || header.Table.entries is null){ return; }
		foreach (DropTableEntryResource entryResource in header.Table.entries)
		{
			DropEntryNode newNode = AddNewEntry();
			newNode.Weight = entryResource.weight;
			newNode.DropResource = entryResource.drop;
		}
	}


	internal void WhenEntryChanged(DropEntryNode dropEntryNode)
	{
		SaveTable();
	}
}// EOF CLASS
#endif