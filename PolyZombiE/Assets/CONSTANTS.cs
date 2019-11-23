using UnityEngine;

public class CONSTANT{

    public const string ZOMBIE_LAYER_NAME = "Zombie";
    public static int ZOMBIE_LAYER_INDEX = LayerMask.NameToLayer(ZOMBIE_LAYER_NAME);
    public const string HUMAN_LAYER_NAME = "Human";
    public static int HUMAN_LAYER_INDEX = LayerMask.NameToLayer(HUMAN_LAYER_NAME);

    public const float MINIMUM_INFECTIOUSNESS = 20.0f;
    public const float INFECTION_SPREAD_RATIO = 0.02f;
}