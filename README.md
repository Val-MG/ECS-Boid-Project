# ECS-Boid-Project
Valentin MIGDAL                                                                          
valentin.mig@gmail.com


# BOIDS DOCUMENTATION
_________________________________________________________________________


## Objective

The objective of this project is to recreate a flocking algorithm (also know as boid) using the new ECS (Entity Component System) provided by Unity. ECS will enable a better performance of the program, allowing to create more boids, but will require to recreate completely the algorithm, since the algorithm will work differently using this technology.
The boids should have real life-like behavior: moving randomly in a space, avoiding obstacles, running away from humans (players), and merging together when they go nearby another boid / boid group.


## How to use

### Preparing the prefab

Get your Game Object, add the script “ConvertToEntity” from unity.entities (Conversion mode should be on “Convert And Destroy”). Add the MoveSpeedData Script, set up the 3-dimensional direction, velocity, turn speed and the size of the radius within which the boid will stay. Add the BoidTag script for it to be recognized as a boid entity.

### Preparing the spawner

Create an empty GameObject named “BoidSpawner” and place it in the world. The boids will loop around this point according to the previously setted up radius. Enter the radius within which the boids will spawn, the number of boids and the previously made prefab.

Be sure to add a “Player” tag to the player so that the boids run away from him if he gets too close.

Because of how DOTS works, you don’t need to add any more scripts to the Game Objects. While they are in the project they will affect the boids. Press play and everything should run. You will be able to see the entities in the Entities tab (Window => DOTS => Entities),not the hierarchy, when play is on.

The player is here for the example and can be deleted.

_________________________________________________________________________

# Project Files Description

The project contains multiple files. We will focus on the ones in Asset => BoidTest. The shader contains a custom shader that makes the boids wiggle from left to right. PlayerMovement is only here for the example and can be deleted later. Fishs contains the prefabs, materials, models and textures. BoidTest contains all the scripts concerning the boids.

## BoidTest - Scripts

All the scripts to create Boids with Entities. You will use GameObjects as prefabs, but need to attach the “ConvertToEntity” script, from the unity.entities package, to them.

# DOTS Spawner

This script is attached to the BoidSpawner GameObject. You need to enter the prefab as a GameObject, the radius within which the boids will spawn and the number of Boids to create. Give them a random position and orientation within the radius with the two according methods.

# SpawnerCoordinates

A class that returns the x, y and z positions of the BoidSpawner Game Object. We need them to make the boids loop around their spawn point and not the origin.

# Data - MoveSpeedData

Script attached to the DOTS boid prefab. IComponentData with a float3 direction, velocity, turn speed and radius (in which the boid will be contained). The values can be changed directly on the prefab.

# System - MoveSystem

Creates movement for the boids. They will go forward according to a set direction. When they go out of the predefined radius, they will turn back.

# System - WaveSystem

Create a wave-style effect, Boids move up and down and not just forward. For a more natural look.

# System - RunawaySystem

Script applied to all boids, when they get near an object with the player tag, they run away from him by speeding up and trying to avoid him.

# System - AvoidCollisionSystem

Script applied to all boids, to make them avoid walls by turning around them.

# Tag - BoidTag

Attached to the boid prefab. Creates a Tag to assign a GameObject as a Boid, so that he can get the Boid Behavior.

