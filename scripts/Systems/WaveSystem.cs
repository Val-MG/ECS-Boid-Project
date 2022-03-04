
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class WaveSystem : ComponentSystem
{
    //script use to create a wave-like effect so that the boids dont move simply forward but up and down as well
    protected override void OnUpdate()
    {
        Entities.WithAny<BoidTag>().ForEach((ref Translation pos, ref MoveSpeed moveSpeed) =>
        {
            float defaultY = pos.Value.y;
            float yPosition = (float)0.01 * math.sin((float)Time.ElapsedTime * (float) 0.001 + pos.Value.x * (float)0.5 + pos.Value.z * (float)0.5);
            pos.Value.y = defaultY + yPosition;

        });
    }

}
