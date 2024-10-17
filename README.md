# Grapple

### Code Architecture:

The Project is constructed using the MVC (Model-View-Controller) architecture.

**_The model_** loads object atributes from outside (JSON) files, and keeps them stored, sorted and updated.
**_The view_** loads in textures and animations, calls the Draw() function for displaying/rendering drawable objects.
**_The controller_** has access to both _the view_ and _the model_, and handles all the logic and changes with are later sotred and rendered.

### Code structure:

- The program starts by running Program.cs, which calls Game1.Run();
- **_Game1_** initializes all the XNA base functions and calls Update() and Draw() on the GameController
- **_GameController_** has the logic of where in the game we should be (Menu, Levels, ...), initializes and calls coresponding controllers.
- **_LevelController_** takes the _LevelModel_ and _LevelView_ as atributes. 
	- Handles collisions (by calling _Physics_).
	- Handles user input and other gameplay logic.
	- Has Update() and Draw() functions taken from the GameController.
	- **_LevelModel_** stores the atributes (location, size, hp...) of objects
	- **_LevelView_** gets called from the Draw loop in the _LevelController_

