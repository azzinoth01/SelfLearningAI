This is a small project I made in order to understand neural networks. 
I am creating an AI that is capable of learning a small race track by only geting the distance value to the walls in all directions as input.
The AI does not know how the track looks like.

The selflearning works by giving the AI an score based on how far it got on the track. The AIs with the highest score get repopulated with mutation into the next generation unitl the learning is stoped. 
Only AIs who did better than their last best generation are saved.

To define the training settings use change the values of the Scriptable object the Resources folder.
To start the training go into playmode and go to the "AICreator" gameobject and make an rightclick on the Create Ai Player component and use the option "Start AI spawning" 

In the "brainCreator" gameobject you can set the base brain which will be used as the starting AI for all created AIs to evelove from.
