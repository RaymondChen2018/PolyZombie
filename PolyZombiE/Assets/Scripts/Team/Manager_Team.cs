using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

/// <summary>
/// Responsible for managing team count;
/// </summary>
public class Manager_Team : MonoBehaviour {

    //[SerializeField] private List<Team_Attribute> humanLeft;
    [SerializeField] private int humanCount = 0;
    [SerializeField] private int zombieCount = 0;
    [SerializeField] private UnityEvent OnHumanExtinctOnce;

    // Use this for initialization
    void Awake () {

        // Get all humans
        Team_Attribute[] allTeamMembers = FindObjectsOfType<Team_Attribute>();
        for (int i = 0; i < allTeamMembers.Length; i++)
        {
            if (allTeamMembers[i].gameObject.layer == CONSTANT.LAYER_INDEX_HUMAN)
            {
                //Team_Attribute human = allTeamMembers[i];
                //humanLeft.Add(human);
                humanCount++;
            }
        }


    }

    // Update is called once per frame
    void Update () {
		if(humanCount == 0)
        {
            OnHumanExtinctOnce.Invoke();
            OnHumanExtinctOnce.RemoveAllListeners();
        }
	}

    //public static void oneLessHuman(Team_Attribute victim)
    //{
    //    // Remove human from list
    //    Assert.IsTrue(_singleton.humanLeft.Contains(victim));
    //    _singleton.humanLeft.Remove(victim);
    //}

    public void SubtractHuman()
    {
        humanCount--;
    }
    public void SubtractZombie()
    {
        zombieCount--;
    }
}
