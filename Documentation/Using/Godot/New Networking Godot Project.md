## Visual Studio
1. Create a new blank solution where you want the project to be located
	- Create a new project > Blank Solution
2. Create the "commons/" folder
	- Right-click the Solution in the solution explorer > Add > New Solution Folder
	- Also create that folder in the file system
3. Create the UI project
	- Right-click the Solution in the solution explorer > Add > New Project … > C# Class Library
	- Name it **Exactly** `ProjectDefs.UI`
	- Put it under the commons folder
	- Once it's created, you can delete the default `Class1.cs` it added
4. In the UI Project add a folder named "ui/" where you'll later add the pages and layouts files
	- Also add that folder in the file system
	- Import the UI samples ( `*.layout`, `*.page`, and associated `*.cs`)  under their corresponding folder in `ProjectDefs.UI`

## Godot Launcher
Do this twice, once for the Client project and once for the Server project
1. Create a Godot project
	- Should be located in a subfolder alongside the commons folder
	- Version control metadata should be set to "None" (git will be initialized later on)
2. Once the project is created and opened, generate the solution (`.sln`)
	- Project > Tools > C# > Create C# Solution

## Solution Directory
1. Initialize git with `git init`
2. Copy `.gitignore` and `.gitattributes` from the Samples
3. Create a subfolder named "dependencies/FLG.Framework"
4. Copy all the `FLG.Framework` `*.dll`s inside the folder created at the previous step
	- TODO: Provide helper scripts

## Visual Studio
1. Add the Godot project (`.csproj`) to the global solution
	- Right-click the Solution in the solution explorer > Add > Existing Project …
2. Add the necessary project reference to the projects
	- Unfold a project, and right-click dependences > Add Project Reference … > Browse > Browse …
	- The main Godot Projects might need all of the `*.dll` from the framework
	- `ProjectDefs.UI` requires at least `FLG.Cs.Datamodel`, `FLG.Cs.Decorators`, `FLG.Cs.Math`, `FLG.Cs.Model`, and `FLG.Cs.ServiceLocator`
	- The main Godot Projects also requires a reference to `ProjectDefs.UI`

## Godot Projects
Do this twice, once for the Client project and once for the Server project
1. Import the `GameManager.cs.tmpl` and rename it without the `.tmpl` part
2. Create two new Root Node
	1. The first of type 3D Scene, this will be you main scene node.
		- Add the `GameManager` script to it
	2. The second of type User Interface (Control), this will be the UI node
		- Don't add anything in it. This node will be populated by the framework
		-  Add it as a child to the main scene node and note its path. You will need it for the next step
3. Follow the instructions in the file or read [[Initializing the Framework Within Your Game]] to changes the template values according to your needs

## Final Result

```
MyProject/
 ├─ .git/
 ├─ .vs/
 ├─ commons/
 │   └─ ProjectDefs.UI/
 │       ├─ bin/
 │       ├─ obj/
 │       ├─ ProjectDefs.UI.csproj
 │       ├─ Sample1.cs
 │       ├─ Sample2.cs
 │       ├─ *.layout
 │       └─ *.page
 ├─ dependencies/
 │   └─ FLG.Framework/
 │       ├─ FLG.Cs.*.dll
 │       └─ FLG.Godot.*.dll
 ├─ MyProjectClient/
 │   ├─ .godot/
 │   ├─ GameManager.cs
 │   ├─ icon.svg
 │   ├─ icon.svg.import
 │   ├─ main.tscn
 │   ├─ project.godot
 │   ├─ MyProjectClient.csproj
 │   ├─ MyProjectClient.sln
 │   ├─ ui.tscn
 │   └─ UIManager.cs
 ├─ MyProjectServer/
 │   ├─ .godot/
 │   ├─ GameManager.cs
 │   ├─ icon.svg
 │   ├─ icon.svg.import
 │   ├─ main.tscn
 │   ├─ project.godot
 │   ├─ MyProjectServer.csproj
 │   ├─ MyProjectServer.sln
 │   ├─ ui.tscn
 │   └─ UIManager.cs
 ├─ .gitattributes
 ├─ .gitignore
 └─ MyProject.sln
```
