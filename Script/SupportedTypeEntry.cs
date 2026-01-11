#if TOOLS
using Godot;
using System;

namespace MDropTables;
[Tool]
public partial class SupportedTypeEntry : HFlowContainer
{
	[Export] private LineEdit lineEdit;
	[Export] private TextureButton btnDelete;

	private SupportedTypes supportedTypes;

	public void Setup(SupportedTypes supportedTypes, string typeName)
	{
		this.supportedTypes = supportedTypes;
		lineEdit.Text = typeName;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		btnDelete.Pressed += () => supportedTypes.Dock.WhenDeleteTypePressed(lineEdit.Text, this);
		lineEdit.TextSubmitted += SubmitType; 
	}

    private void SubmitType(string newText)
    {
        if(!supportedTypes.Dock.WhenTypeSubmitted(newText, this))
		{
			lineEdit.Text = string.Empty;
		}
    }
}// EOF CLASS
#endif