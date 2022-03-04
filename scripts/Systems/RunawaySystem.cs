using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

//This script allows the boid to run away from the player, if the player is to close to the boid
public class RunawaySystem : SystemBase
{
    [SerializeField] private float nearRadius = 20F;                    //the radius within which the fish will run away from the player
    GameObject[] player = GameObject.FindGameObjectsWithTag("Player");     //get all the players

    protected override void OnUpdate()
    {
        for (int i = 0; i<player.Length; i++)
        {
            Entities.WithAny<BoidTag>().ForEach((ref Translation pos, ref Rotation rot, ref MoveSpeed moveSpeed) =>
            {
                //if the player is within the radius, boids speeds up and change its direction
                if (((Vector3)pos.Value - player[i].transform.position).magnitude <= nearRadius)
                {
                    moveSpeed.velocity = moveSpeed.maxVelocity;
                    ChangeDirection(ref rot, moveSpeed, pos.Value, Time.DeltaTime);
                }
                //when the boid is out of danger, it goes back to its original speed
                else
                {
                    moveSpeed.velocity = moveSpeed.minVelocity;
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
