# CS50 Games Final Project: BoxBot

This document will attempt to retrospectively detail my thought process and observations throughout the project and explain how it differs from the course materials.

## TL;DR
In the course we didn't cover:
- 3D puzzle games.
- the creation of levels that were curated to teach gameplay mechanics i.e not randomly generated.
- modular level design in 3D.
- structuring components to model behaviours.
- the mechanics around pushing boxes, pressure sensitive buttons and lasers.
- Cinemachine.
- ShaderGraph.
- persisting player progression across gameplay sessions.
- persisting data between scenes.
- creating a credits screen.

## Retrospective Brain-Dump
I didn't know what sort of game I wanted to make when I started this project so I skimmed through the free asset packs by Kenney for inspiration. I was instantly drawn to the crate models being a big fan of the Crash Bandicoot series. Since the crates were perfectly square I decided that the game should be grid based to make constructing the levels easier (I used ProGrids for this) and that the boxes should be pushable to be used in some form of puzzle solving mechanic.

I used a free rigged and animated robot asset as my main character and worked backwards to decide how and where I could use the existing animations in a game. Obviously the idle and walking animations would be used for movement, the head shaking animation would be used to let the player know that they couldn't perform an action, the death animation would be used if the player died and the wave animation I thought could be used randomly when idle but ended up using it on the title screen. Some animations were combined such as the head nod and thumbs up for level completion.

The DOTween Pro library was used in conjunction with the Animator in Unity to animate character movement as I thought it would be something I would use a lot throughout the project but I ended up only using it in a few places and only a very limited set of its functionality.

I had originally intended to make the game turn based but later decided against that idea. Allowing the player free movement would have required changing a lot of work that had already been done since everything was based on the grid to determine where the player could move or push crates to. As a result I made the decision to leave the player movement as it was rather than to change it. In hindsight I should not have restricted movement to the game grid as it limited player speed.

Initially I had used trigger parameters in the animation controller to control which state to transition into but found these very difficult to work with as they automatically clear themselves after triggering and have no way to poll their state. I saw suggested in a past Unite conference video that it was simpler to use bool parameters which made it a lot easier to manage and supported polling the state.

After movement I added various tiles that could be placed to form a level. Large colliders were placed around the outside of the level and were marked as obstructions that prevented the player from traversing them. This approach seemed more performant than to add colliders to each grid square but the latter approach lent itself to modular level design so much better that I quickly adopted it.

In order to determine if the player could traverse to the next grid square I decided to cast a ray from the character in the direction that the player instructed. This approach was later changed to cast rays downwards at the grid squares as the requirement to find out if a pushable crate was obstructed (either by the level boundaries or immovable crates and later spikes and laser turrets) arose. The new approach limited character movement to a single plane but as I had no plans to include verticality to the level design or mechanics it seemed to be the simplest solution to the problem.

I added the ability to mark tiles as hazards which would kill the player causing the robot to fall apart if they made contact with the collider. This was ultimately only used for the spikes but gave me a way to restrict the players movement whilst not completely blocking certain areas with crates. I would later hide some spikes in places that were partially obstructed by crates to trick the player into walking on them. The spikes also posed an interesting challenge; they were originally obstructions but changed to hazards which meant that crates could now be pushed over them. I implemented the concept of "obstructions to pushables" to solve this.

I spent quite a bit of time over-architecting the animations in an attempt to make them more robust and avoid potential runtime errors as a result of changed parameter names. I achieved this by using enums that mapped to the parameters in the animation controllers. I created state machine behaviours that would validate in the editor if there was a mismatch on either side however the whole process became convoluted, flaky and ultimately didn't work very well. A similar approach was used for referencing the scene build index for changing scenes however in hindsight I should have just used the scene names as they were less likely to change and cause issues due to the enum values being serialised.

I used the Cinemachine package to make the camera track the player movement and tween smoothly to another camera attached to the goal prefab on level completion.

I added the ability to trigger the lowering of a bridge asset via a button which I had intended to use more but only ended up using once due to its size and the length of time it took to cross it. The buttons were made to behave as if pressure sensitive so that both the player and pushable crates could trigger them.

I created a system where buttons could be wired up to any component that implemented the IToggleable interface to decouple the trigger from the action. I later used this same technique when implementing laser turrets that could be disabled. The turrets would shoot lasers (using ray casting and the line renderer component) at the player when they walked in front of them.

I upgraded the project to use the Universal Render Pipeline to take advantage of the ShaderGraph package which I used to make the goal glow and pulsate.

Although not part of the spec I wanted my game to be a complete experience with menus and credits so I implemented a custom menu system that tracks basic navigation and used a circular linked list to be able to infinitely scroll through menu items. In hindsight I should probably have used an existing library rather than create my own however I had already used a third party library (DOTween) in the project and didn't want to get marked down.

The credits are accessible through a menu item and also on completion of the final level detailing the licenses of the used assets. The save game is reset once the credits roll on game completion but not when viewing credits from the menu. There is a separate menu item to manually reset the save game if needed.

With all of the mechanics proven and modular level prefabs created I set out to create a few more levels to flesh the game out. Although there are only 5 in total I tried to teach the player a new mechanic in each level, ramping up in difficulty. There is a considerable increase in difficulty in the final level making use of all of the mechanics taught throughout the previous levels and teaching the player about toggleable turrets whilst requiring the player to backtrack and put themselves in harms way in order to complete the level.

An area that I had intended to look into for this project but didn't was automated testing. I have experience writing unit tests in C# but not in the context of Unity, I therefore, against my better judgement deferred looking into this area until I had a more concrete idea about the game I wanted to create. The lack of tests in this project required me to manually test numerous areas when making risky changes that could have been avoided and would have saved me a lot of time and effort in fixing regressions.

I hope having read this brain-dump of my experience working on this project that it is clear the end result is a complete and unique 3D puzzle solving game unlike any of the course projects.