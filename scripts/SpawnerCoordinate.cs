using UnityEngine;

public class SpawnerCoordinate
{
    public static GameObject spawner = GameObject.Find("BoidSpawner");

    public static float getX()
    {
        return spawner.transform.position.x;        
    }

    public static float getY()
    {
        return spawner.transform.position.y;
    }

    public static float getZ()
    {
        return spawner.transform.position.z;

    }
}
