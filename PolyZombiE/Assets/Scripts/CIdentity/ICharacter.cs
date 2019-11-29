using UnityEngine;
using UnityEditor;

public interface ICharacter
{
    void OnChangeFaction(Identity newFaction);
    void OnDeath();
}
