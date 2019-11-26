using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Wall_Generator : MonoBehaviour {
    // Use this for initialization
    void Start () {
        // Walls
        privAddCollider(CONSTANT.TAG_NAME_WALL, CONSTANT.LAYER_INDEX_WALL);

        // Low Walls
        privAddCollider(CONSTANT.TAG_NAME_LOWWALL, CONSTANT.LAYER_INDEX_LOWWALL);
    }

    /// <summary>
    /// Layer name should match tag name
    /// </summary>
    /// <param name="targetTag"></param>
    /// <param name="targetLayerIndex"></param>
    void privAddCollider(string targetTagName, int targetLayerIndex)
    {
        GameObject[] allWalls = GameObject.FindGameObjectsWithTag(targetTagName);
        for (int i = 0; i < allWalls.Length; i++)
        {
            GameObject obj = allWalls[i];
            Assert.IsNotNull(obj);
            Assert.IsTrue(obj.layer == targetLayerIndex);

            SpriteRenderer renderer = allWalls[i].GetComponent<SpriteRenderer>();
            Assert.IsNotNull(renderer);

            allWalls[i].AddComponent<PolygonCollider2D>();
        }
    }
}
