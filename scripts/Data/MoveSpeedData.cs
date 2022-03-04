using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct MoveSpeed : IComponentData
{
    public float3 direction;
    public float velocity;
    public float minVelocity;
    public float maxVelocity;
    public float turnSpeed;
    public int radius;
}
