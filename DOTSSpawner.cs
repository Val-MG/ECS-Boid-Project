//code is based on Wilmer Lin's Youtube tutorials : https://www.youtube.com/channel/UCWERX3S8tEGqNeLuQGCcJmw

using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class DOTSSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private int numberOfEntity;
    [SerializeField] private GameObject GOPrefab;


    private Entity entityPrefab;
    private World defaultWorld;
    private EntityManager manager;

    // Start is called before the first frame update
    void Start()
    {
        defaultWorld = World.DefaultGameObjectInjectionWorld;
        manager = defaultWorld.EntityManager;

        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(defaultWorld, null);      //default game object settings
        entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(GOPrefab, settings);               //convert game object prefab to an entity

        for (int i = 0; i < numberOfEntity; i++)
        {
            InstantiateEntityPrefab();
        }
    }

    private void InstantiateEntityPrefab()
    {
        Entity newEntity = manager.Instantiate(entityPrefab);       //create the new entity from the prefab
        //assign a position
        manager.SetComponentData(newEntity, new Translation         
        {
            Value = RandomPosition()                                
        });
        //assign a rotation
        manager.SetComponentData(newEntity, new Rotation
        {
            Value = RandomRotation()                                
        });
    }

    private float3 RandomPosition()
    {
        //create a random position within the given radius
        return new float3(
            UnityEngine.Random.Range(-spawnRadius - SpawnerCoordinate.getX(), spawnRadius + SpawnerCoordinate.getX()),
            UnityEngine.Random.Range(-spawnRadius - SpawnerCoordinate.getY(), spawnRadius + SpawnerCoordinate.getY()),
            UnityEngine.Random.Range(-spawnRadius - SpawnerCoordinate.getZ(), spawnRadius + SpawnerCoordinate.getZ())
        );
    }
    private quaternion RandomRotation()
    {  
        //create a random rotation
        return quaternion.Euler(
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f),
            UnityEngine.Random.Range(-360f, 360f)
        );
    }
}
