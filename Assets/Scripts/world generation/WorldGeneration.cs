using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    //GamePlay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    //Configs
    [SerializeField] private int firstChunkSpawnPosition = 5;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistance = 5.0f;

    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        ResetWorld();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (chunkPrefabs.Count == 0)
        {
            Debug.LogError("no chunks assign one");
            return;

        }

        if (!cameraTransform)
        {
            cameraTransform = Camera.main.transform;
            Debug.Log("done already");
        }
    }

    
    public void ScanPosition()
    {
        float cameraZ = cameraTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();


        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        int randomIndex = UnityEngine.Random.Range(0, chunkPrefabs.Count);

        Chunk chunk = chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefabs[randomIndex].name + "(Clone)"));

        if (!chunk)
        {
            GameObject go = Instantiate(chunkPrefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;

        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);

    }

    public void ResetWorld()
    {
        chunkSpawnZ = firstChunkSpawnPosition;

        for (int i = activeChunks.Count; i != 0; i--)
            DeleteLastChunk();

        for (int i = 0; i < chunkOnScreen; i++)
            SpawnNewChunk();

    }

}
