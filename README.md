# DungeonGenerator
 Here I made a custom procedural dungeon generator that generates by going through lists of classes and procedurally generating one of each type of class being: 
 Room, Exit and Corridor going through a for loop of one each time.

Rooms
Generates a room of a random size within limits and generates a grass class to generate a background on the tilemap 
which also generates enemies of random numbers and types.

Exits
Takes room size information when created and chooses a random space within the rooms borders to spawn 
while limiting itself to avoid corners and checks whether it intersects with other corners.
This then spawns a corridor at its exit if the area is available.

Corridors
After being spawned from an exit every frame a check is done to tell how many times its gone in the same direction 
and if it has exceeded the limit then it can randomly change direction it could also end up staying in the same direction.
There is also a random chance that the corridor generates a randomly sized room and checks if the area is available generating it if available and
continuing on if unavailable. While the corridor generates it also checks before placing the set of corridor tiles and generates a stopping tile if unavailable 
or if the room num limit is exceeded.
