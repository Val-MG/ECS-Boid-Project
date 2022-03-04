using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

//This script makes the boids avoid walls, to not go into them
public class AvoiCollisionSystem : SystemBase
{
    [SerializeField] private float nearRadius = 15F;                    //the radius within which the fish will run away from the player
    Collider[] walls = GameObject.FindObjectsOfType<Collider>();        //get all the walls
    
    protected override void OnUpdate()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            Entities.WithAny<BoidTag>().ForEach((ref Translation pos, ref Rotation rot, ref MoveSpeed moveSpeed) =>
            {
                //if the player is within the radius of a wall, the boid change its direction
                if (((Vector3)pos.Value - walls[i].transform.position).magnitude <= nearRadius)
                {
                    ChangeDirection(ref rot, moveSpeed, pos.Value, Time.DeltaTime);
                }

            }).WithoutBurst().Run();
        }
    }

    //make the boids turn around
    private static void ChangeDirection(ref Rotation rot, MoveSpeed moveSpeed, float3 position, float deltaTime)
    {
        float3 newDirection = -position * moveSpeed.direction * deltaTime;
        quaternion targetRotation = quaternion.LookRotationSafe(newDirection, math.up());
        rot.Value = math.slerp(rot.Value, targetRotation, moveSpeed.turnSpeed * 5);
    }
}
