Overview:
* I wanted to create a reusable "thingA hit thingB" physics detection

Approach 1:
* I tried to update the related files to be generics and be able to pass in <PlayerTag,PickupTag,OutputTagWhenHit>
* But I couldn't get generics to play nice and run without crashing. Probably solveable
* This does not work!

Approach 2:
* The system now uses 3 custom tags
* then in a gamespecific system, one must check for the output tag and do the desired action
* This works!
