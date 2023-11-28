# Console RPG

Simple console rpg game base.

* **Game** is the main loop and UI class.
* **World** holds entities (enemies, rocks, trees), world map (right now only dots for grass) and player.
* **Actions** hold whatever happends when an entity wanna do something, move, attack and so on.
* **Entites** Used to hold different entities, but it was abstracted to just one class to rule them all (good or bad, who knows). Sci-fi or fantasy, maybe you want a tree that can move or a rock that can teleport and talk?

Use the commit log to see how the mess evolved.

###Things to think about:

-[] Figure out how many entities can be on the same spot (lists of entities per position is easy but, not easy. Who to attack for example? Default to first entity?)
-[] Decide if one entity to rule them all is the way to go. A rock has zero offensive strength, but a bandit a lot. A rock has a lot of defense, but no evasive skills. A tree could possible have a lot of defense if it has a lot of armor hanging from its branches.. Nothing in the world is stuck, its just different friction its stuck with...Behaviour could easily be divided into classes or delegates.
-[] What does dying mean? Maybe just a loss of possibility to do actions? A dead bandit and a rock is not much different. Inventory is mostly "stuff stuck to something else". You can put a backpack on a rock...