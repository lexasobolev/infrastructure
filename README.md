## Infrastructure
This repository contains a collection of _infrastructure_ projects that should be added as a _submodule_ inside of another repository.
## Setup
* Go do a Git repository where you'd like to add these _infrastructure_ projects.
* Add this repository as a submodule:
  * `git add submodule https://github.com/bcjobs/infrastructure.git Infrastructure`
* Open Solution in Visual Studio.
* Right-click solution name and select:
  * `Add` --> `New Solution Folder`
* Name the solution folder `Infrastructure`
* One by one, add all of the projects in the submodule into the solution.
* If you encounter any problems with Entity Framework dependencies while trying to build one of these added projects, uninstall and re-install Entity Framework Nuget packages.
 

