//code is based on Wilmer Lin's Youtube tutorials : https://www.youtube.com/channel/UCWERX3S8tEGqNeLuQGCcJmw

using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MoveSystem : SystemBase
{
    //we use these to make the boids loop around their spawn point and not the origin
    float x = SpawnerCoordinate.getX();
    float y = SpawnerCoordinate.getY();
    float z = SpawnerCoordinate.getZ();

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        //all boids going forward
        Entities.WithAny<BoidTag>().ForEach((ref Translation pos, ref Rotation rot, in MoveSpeed moveSpeed) =>
        {
            float3 forwardDirection = math.forward(rot.Value);
            pos.Value += math.normalizesafe(forwardDirection) * moveSpeed.velocity * deltaTime;
            FaceDirection(ref rot, moveSpeed);

            //to make sure that the boid doesn't go forward to infinity but turns back when they go out of bounds
            if (pos.Value.x > x + moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.x = pos.Value.x - 2 * moveSpeed.radius;
            }

            else if (pos.Value.y > y + moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.y = pos.Value.y - 2 * moveSpeed.radius;
            }

            else if (pos.Value.z > z + moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.z = pos.Value.z - 2 * moveSpeed.radius;
            }

            else if (pos.Value.x < x - moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.x = pos.Value.x + 2 * moveSpeed.radius;
            }

            else if (pos.Value.y < y - moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.y = pos.Value.y + 2 * moveSpeed.radius;
            }

            else if (pos.Value.z < z - moveSpeed.radius)
            {
                //Boids loop in the radius
                pos.Value.z = pos.Value.z + 2 * moveSpeed.radius;
            }

        }).WithoutBurst().Run();
    }


    //compute boid's look direction
    private static void FaceDirection (ref Rotation rot, MoveSpeed moveSpeed) 
    {
        quaternion targetRotation = quaternion.LookRotationSafe(moveSpeed.direction, math.up());
        rot.Value = math.slerp(rot.Value, targetRotation, moveSpeed.turnSpeed);
    }
}
