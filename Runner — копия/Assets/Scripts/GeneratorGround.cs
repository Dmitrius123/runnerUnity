using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorGround : MonoBehaviour

{
    public GameObject[] groundPrefabs;
    private List<GameObject> activeGrounds = new List<GameObject>();
    private float spawnPos = 0;
    private float groundLenght = 100;

    [SerializeField] private Transform player;
    private int startGrounds = 6;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startGrounds; i++)
        {
            SpawnGround(Random.Range(0, groundPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z - 60> spawnPos - (startGrounds * groundLenght))
        {
            SpawnGround(Random.Range(0, groundPrefabs.Length));
            DeleteGround();
        }
            
    }

    private void SpawnGround(int groundIndex)
    {
        GameObject nextGround = Instantiate(groundPrefabs[groundIndex], transform.forward * spawnPos, transform.rotation);
        activeGrounds.Add(nextGround);
        spawnPos += groundLenght;
    }
    private void DeleteGround()
    {
        Destroy(activeGrounds[0]);
        activeGrounds.RemoveAt(0);
    }
}
