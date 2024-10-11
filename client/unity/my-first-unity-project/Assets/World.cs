using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject BlockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // for (var i = 0; i < 100; i++)
        // {
        //     for (var j = 0; j < 100; j++)
        //     {
        //         Instantiate(BlockPrefab, new Vector3(i, 0, j), Quaternion.identity);
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateChunk(Vector2Int position, byte[] blockData)
    {
        for (var i = 0; i < blockData.Length; i++)
        {
            var v = blockData[i];
            if (v == 0)
                continue;
            var x = i % 16;
            var z = i % (16 * 16) / 16;
            var y = i / (16 * 16);
            var pos = new Vector3(x, y, z);
            Instantiate(BlockPrefab, pos, Quaternion.identity);
        }
    }
}
