using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Wall_Generator : MonoBehaviour {
    [SerializeField] private string WallTag;
    [SerializeField] private string LowWallTag;

    // Use this for initialization
    void Start () {
        Assert.IsNotNull(WallTag);
        GameObject[] allWalls = GameObject.FindGameObjectsWithTag(WallTag);
        for(int i = 0; i < allWalls.Length; i++)
        {
            if (allWalls[i].GetComponent<SpriteRenderer>() != null)
            {
                allWalls[i].AddComponent<PolygonCollider2D>();
            }
        }
	}
}
