using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Responsible for managing team count;
/// </summary>
public class Team_Manager : MonoBehaviour {

    [SerializeField] private List<Team_Attribute> humanLeft;
    
    private static Team_Manager _singleton;

    // Use this for initialization
    void Awake () {
        _singleton = this;

        // Get all humans
        Team_Attribute[] allTeamMembers = FindObjectsOfType<Team_Attribute>();
        for(int i = 0; i < allTeamMembers.Length; i++)
        {
            if(allTeamMembers[i].gameObject.layer == CONSTANT.LAYER_INDEX_HUMAN)
            {
                Team_Attribute human = allTeamMembers[i];
                humanLeft.Add(human);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		if(humanLeft.Count == 0)
        {
            Debug.LogError("Zombie wins!");
        }

        
	}

    public static void oneLessHuman(Team_Attribute victim)
    {
        // Remove human from list
        Assert.IsTrue(_singleton.humanLeft.Contains(victim));
        _singleton.humanLeft.Remove(victim);
    }
}
