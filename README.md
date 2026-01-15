# MDropTables ( Written in C#. Needs the Mono version of Godot to run)

v1.3

Editor tooling to create and edit and run drop tests on weighted drop tables.

# Features
Simple creation and editing of drop tables.
Easily loaded and used in game.
Supports assigning any Resource as the dropped item so could be used for texture variation and more.

![GitHub Logo](Example/ScreenShot01.png)  ![GitHub Logo](Example/ScreenShot02.png) 

# Install 

Place all repo files in /addons/MDropTables
Compile
Activate the plugin in project settings

# How to use in Editor
Create DropTableResource resource. It will hold the data needed to read and use the table in runtime.
The addon has a dock that you then load the resource in and add/remove drop entries.
To filter what kind of resources the drop entry can hold the DropTableResource holds a string array of the Type names
The weight is relative to the total weight in the table. So when anything changes the table is recalculated.
Making sure the % shown is accurate.

Use the Test button to test how the numbers stack up. It constructs a table and pulls 10k random drops and presents the result
in Output.

# How to use in Runtime
In runtime create an instance of DropTable. Passing the DropTableResource you want to use as the parameter.
Then ask the DropTable instance for drop. Easy as that.


```cs
        dropTable = new DropTable(dropTableResource);
		DropEntry loot = dropTable.GetDrop();
        // loot.Drop is the resource
```


# Changelog
1.3 Fixed name property resulted in null exception
