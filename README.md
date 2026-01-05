# MDropTables

v1.0

Editor tooling to create and edit and run drop tests on weighted drop tables.

![GitHub Logo](Example/ScreenShot01.png)

# Install

Place all repo files in /addons/MDropTables
Compile
Activate the plugin in project settings

# How to use
Create DropTableResource resource. It will hold the data needed to read and use the table in runtime.
The addon has adock that you then load the resource in and add/remove drop entries.
The weight is relative to the total weight in the table. So when anything changes the table is recalculated.
Making sure the % shown is accurate.

Use the Test button to test how the numbers stack up. It constructs a table and pulls 10k random drops and presents the result
in Output.

# ToDo
The runtime, project level bit is not made yet. So a few thigns might change.

