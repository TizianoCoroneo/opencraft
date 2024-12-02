using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using Opencraft.NetCode;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

/// <summary>
/// A trivial class responsible for keeping track of the game's world state.
/// </summary>
public class World : MonoBehaviour
{
    public GameObject BlockPrefab;
    public GameObject GrassPrefab;
    public GameObject Player;
    public Networking Net;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    private Dictionary<Vector2Int, List<GameObject>> _loadedChunksDict = new Dictionary<Vector2Int, List<GameObject>>();

    // Update is called once per frame
    void Update()
    {
        var loadNextNChunks = 1;
        var chunkSpacingFactor = 16;
        
        // Quantized to column multiples
        var playerX = (int)(Player.transform.position.x / 16) * 16;
        var playerZ = (int)(Player.transform.position.z / 16) * 16;

        for (var i = -loadNextNChunks; i <= loadNextNChunks; i++)
        {
            for (var j = -loadNextNChunks; j <= loadNextNChunks; j++)
            {
                var pos = new Vector2Int { x = playerX + i * chunkSpacingFactor, y = playerZ + j * chunkSpacingFactor };
                if (_loadedChunksDict.ContainsKey(pos)) continue;
                
                _loadedChunksDict.Add(pos, new List<GameObject>());
                Debug.Log($"Added chunk key {pos}");
                
                var networkPos = new Pos2 { X = pos.x, Z = pos.y };
                Net.RequestColumn(networkPos);
                goto skipLoadOtherChunk; // avoid loading more than one chunk per frame.
            }
        }
        
        skipLoadOtherChunk:
        Func<Vector2Int, bool> isFarChunk = pos => math.abs(pos.x - playerX) > (loadNextNChunks * 2) * 16
                                                   || math.abs(pos.y - playerZ) > (loadNextNChunks * 2) * 16;

        var farPairs = _loadedChunksDict.Where(pair => isFarChunk(pair.Key));
        var farChunkGameObjects = farPairs.SelectMany(pair => pair.Value);

        foreach (var chunk in farChunkGameObjects) {
            Destroy(chunk);
        }
        
        var farChunkKeys = farPairs.Select(pair => pair.Key);

        foreach (var key in farChunkKeys)
        {
            Debug.Log($"Removed chuck at position {key}");
            _loadedChunksDict.Remove(key);
        }
    }

    public void InstantiateChunk(Vector2Int position, byte[] blockData)
    {
        Debug.Log($"Instantiating chuck at position {position}");
        
        for (var i = 0; i < blockData.Length; i++)
        {
            var v = blockData[i];
            if (v == 0)
                continue;
            var x = i % 16;
            var z = i % (16 * 16) / 16;
            var y = i / (16 * 16);
            var pos = new Vector3(x + position.x, y, z + position.y);

            GameObject blockToInstantiate = BlockPrefab;
            switch (v)
            {
                case 2: blockToInstantiate = GrassPrefab; break; 
                default: break;
            }
            
            var gameObject = Instantiate(blockToInstantiate, pos, Quaternion.identity);
            
            List<GameObject> loadedChunkGameObjects;
            if (_loadedChunksDict.TryGetValue(position, out loadedChunkGameObjects))
            {
                loadedChunkGameObjects.Add(gameObject);
                _loadedChunksDict[position] = loadedChunkGameObjects;
            }
            else
            {
                _loadedChunksDict.Add(position, new List<GameObject> { gameObject });    
            }
        }
    }
}
