using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CheeseBlobSpawner : MonoBehaviour
{
    public Transform spawnHeight;
    public Vector2 spawnRangeZ = new Vector2(3.119f, 2.555f);

    public float spawnRangeX = 0.4f;
    public Vector2 scaleRange = new Vector2(0.5f, 1.3f);
    public GameObject cheeseBlob;
    public Transform cheeseBlobParent;

    void OnEnable()
    {
        //CheeseGratingManager.cheeseBlobSpawned += SpawnBlob;
    }

    void OnDisable()
    {
        //CheeseGratingManager.cheeseBlobSpawned -= SpawnBlob;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos1 = new Vector3(spawnHeight.position.x, spawnHeight.position.y, spawnRangeZ.x);
        Vector3 pos2 = new Vector3(spawnHeight.position.x, spawnHeight.position.y, spawnRangeZ.y);
        Gizmos.DrawLine(pos1,pos2);
    }

    [Button]
    public void SpawnBlob()
    {
        //Spawn cheese blob z range
        //Vector3 spawnPos = new Vector3(Random.Range(spawnHeight.transform.position.x + spawnRangeX, spawnHeight.transform.position.x - spawnRangeX), spawnHeight.position.y, Random.Range(spawnRangeZ.x, spawnRangeZ.y));
        Vector3 spawnPos = new Vector3(spawnHeight.position.x, spawnHeight.position.y, Random.Range(spawnRangeZ.x, spawnRangeZ.y));
        GameObject cheeseBlobInst = Instantiate(cheeseBlob, spawnPos, cheeseBlobParent.rotation, cheeseBlobParent);
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        cheeseBlobInst.transform.localScale = new Vector3(scale, scale, scale);
    }
}
