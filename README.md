# TSoF-Capstone
Computer Game Science Major Capstone Project

Corrupted is a Dungeons and Dragons themed, beat-em-up style game that required three players 
to play the game because running through a Dungeons and Dragons campaign is more fun with friends.

This is the version of my Capstone Project with Team Staff of Fireballs prior to the addition
of the ReWired Plugin that was meant to fix most of our issues regarding controller support 
across various controller types.

1/19/2019
- Reworked some of the systems that managed a forced 3-players and allowed the game to be "runnable" with a single player
- Added an additional control scheme that uses keyboard and mouse to control the character and reticle
- Can load into the main game from character select with a single character selected, and does not load the other
  two character when loaded into the main scene
  
*Noteworthy Bugs*
- Room transitions do not push you into the next room if you do not hit the door trigger at full speed
- The rogue character can dash in between the colliders of the platform segments
- Platform dropping does not work at the moment so the only way to access rooms and areas lower than the starting room is
  to dash in between the platform collider segments
