Made using Unity Engine 2018.2.0f2

Controls:
---------

-	Main Menu and Level Select:
o	Use the right hand to point at Quit, Play, Level 1 or Level 2.
o	Select an item by using A or Right Index Trigger.

-	Inside the level:
o	Move around using the left analog stick.
o	Change look direction by tapping left or right on the right analog stick.
o	To bring up the turret inventory, hold down Left-Hand Trigger. 
o	While the turret inventory is active, use the Right-Hand Trigger to grab a turret.
o	While grabbing a turret, point at a node with your index finger and release the Right-Hand trigger to place the held turret.
o	Use X to access the pause menu.

-	Game Over, Complete Level or Pause menu:
o	Grab a turret and point at the desired menu item. (This is a bug, not a feature).
o	While grabbing and pointing at the menu item, use A or Right Index Trigger to select.


Known Bugs/Issues and Troubleshooting:
-------------------------------------
-	Sometimes turrets don’t place. Point at another node and back at the desired node to potentially fix. 
-	Turrets can be placed multiple times on a single node.
-	If the user grabs a turret but doesn’t place, it saves that turret for the next built turret. If the user grabs a different type of turret it will place two turrets down on the node. One of the previous type, and one of the desired type. 
-	The menu button for the complete level screen is supposed to be below the continue button, it is in the editor but not during gameplay. 
-	The wave spawner can break sometimes when the user goes to back into the main menu then back into another level. Only fix is to relaunch the game.
-	The wave spawner always breaks when the user hits retry in the Game Over or Pause menu. 
-	During in game menus (Game over, level complete or pause), the right hand should function the same as the main menu and level select. Grab a turret and point to enable the ray. 
-	Due to the way the audio has been built, some turrets may not spawn with audio or some current turrets will stop playing audio. This is because Wwise runs out of allocated memory.
