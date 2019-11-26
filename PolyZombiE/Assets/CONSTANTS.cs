using UnityEngine;

public class CONSTANT{
    // Layer & Tag Names
    public const string LAYER_NAME_WALL = "Wall";
    public static int LAYER_INDEX_WALL = LayerMask.NameToLayer(LAYER_NAME_WALL);
    public const string LAYER_NAME_LOWWALL = "LowWall";
    public static int LAYER_INDEX_LOWWALL = LayerMask.NameToLayer(LAYER_NAME_LOWWALL);
    public const string TAG_NAME_WALL = "Wall";
    public const string TAG_NAME_LOWWALL = "LowWall";


    public const string LAYER_NAME_ZOMBIE = "Zombie";
    public static int LAYER_INDEX_ZOMBIE = LayerMask.NameToLayer(LAYER_NAME_ZOMBIE);
    public const string LAYER_NAME_HUMAN = "Human";
    public static int LAYER_INDEX_HUMAN = LayerMask.NameToLayer(LAYER_NAME_HUMAN);

    public const float MINIMUM_INFECTIOUSNESS = 20.0f;
    public const float INFECTION_SPREAD_RATIO = 0.02f;
}