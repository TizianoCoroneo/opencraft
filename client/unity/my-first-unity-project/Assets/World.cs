using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Opencraft.NetCode;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class World : MonoBehaviour
{
    public GameObject BlockPrefab;
    public GameObject GrassPrefab;
    public GameObject Player;
    public Networking Net;

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
    
    private List<Pos2> _loadedChunks = new List<Pos2> { new Pos2 { X = 0, Z = 0 } };

    // Update is called once per frame
    void Update()
    {
        var centerChunkX = (int)(Player.transform.position.x / 16);
        var centerChunkZ = (int)(Player.transform.position.z / 16);
        
        for (var i = -5; i < 5; i++)
        {
            for (var j = -5; j < 5; j++)
            {
                var pos = new Pos2 { X = centerChunkX + i * 16, Z = centerChunkZ + j * 16 };
                if (_loadedChunks.Contains(pos))
                    continue;
        
                _loadedChunks.Add(pos);
                Net.RequestColumn(pos);
                goto skip; // load at most one chunk per frame
            }
        }
        
        skip: return;
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
            var pos = new Vector3(x + position.x, y, z + position.y);
            switch (v)
            {
                case 1: Instantiate(BlockPrefab, pos, Quaternion.identity); break; 
                default: Instantiate(GrassPrefab, pos, Quaternion.identity); break;
            }
        }
    }
}
