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
    /// <summary>
    /// Prefab for a single block in the world.
    /// </summary>
    public GameObject BlockPrefab;
    /// <summary>
    /// Prefab for a grass block in the world.
    /// </summary>
    public GameObject GrassPrefab;
    /// <summary>
    /// Reference to the player GameObject.
    /// </summary>
    public GameObject Player;
    /// <summary>
    /// Reference to the Networking component.
    /// </summary>
    public Networking Net;
    /// <summary>
    /// Dictionary of loaded chunks.
    /// </summary>
    private readonly Dictionary<Vector2Int, List<GameObject>> _loadedChunksDict = new();

    /// <summary>
    /// Clears the dictionary of loaded chunks.
    /// </summary>
    public void ClearLoadedChunks()
    {
        _loadedChunksDict.Clear();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        LoadChunk();
    }


    /// <summary>
    /// Loads the chunk the player is currently in and unloads far chunks.
    /// </summary>
    private void LoadChunk()
    {
        var loadNextNChunks = 1;
        var chunkSpacingFactor = 16;

        // Quantized to column multiples
        var playerX = Mathf.FloorToInt(Player.transform.position.x / 16) * 16;
        var playerZ = Mathf.FloorToInt(Player.transform.position.z / 16) * 16;

        for (var i = -loadNextNChunks; i <= loadNextNChunks; i++)
        {
            for (var j = -loadNextNChunks; j <= loadNextNChunks; j++)
            {
                var pos = new Vector2Int { x = playerX + i * chunkSpacingFactor, y = playerZ + j * chunkSpacingFactor };

                if (_loadedChunksDict.ContainsKey(pos))
                    continue;

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
        var farChunkKeys = (farPairs.Select(pair => pair.Key)).ToList();

        foreach (var chunk in farChunkGameObjects)
        {
            Destroy(chunk);
        }

        foreach (var key in farChunkKeys)
        {
            Debug.Log($"Removed chuck at position {key}");
            _loadedChunksDict.Remove(key);
        }
    }

    /// <summary>
    /// Instantiates a chunk at the given position with the provided block data.
    /// </summary>
    /// <param name="position">The position of the chunk.</param>
    /// <param name="blockData">The block data for the chunk.</param>
    public void InstantiateChunk(Vector2Int position, byte[] blockData)
    {
        Debug.Log($"Instantiating chuck at position {position}");

        if (_loadedChunksDict.ContainsKey(position) && _loadedChunksDict[position].Count > 0)
        {
            Debug.Log($"Chunk already exists at position {position}, probably a duplicate request.");
            return;
        }

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

            if (_loadedChunksDict.TryGetValue(position, out List<GameObject> loadedChunkGameObjects))
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
