using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Text_Helper : MonoBehaviour {
    [SerializeField] private Text textSegment;
	// Use this for initialization
	void Awake () {
        //textSegment = GetComponent<Text>();
        Assert.IsNotNull(textSegment);
    }

    public void setString(int value)
    {
        textSegment.text = value.ToString();
    }
}
