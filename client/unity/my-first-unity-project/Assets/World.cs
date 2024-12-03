using UnityEngine;

/// <summary>
/// A trivial class responsible for keeping track of the game's world state.
/// </summary>
public class World : MonoBehaviour
{
    /// <summary>
    /// Prefab for a single block in the world.
    /// </summary>
    public GameObject BlockPrefab;

    // Start is called before the first frame update
    void Start()
    {



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
