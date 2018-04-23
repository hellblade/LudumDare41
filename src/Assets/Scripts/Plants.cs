using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Collections/Plants")]
public class Plants : ScriptableObject
{
    [SerializeField] Plant[] plants;

    public Plant GetPlant(int index)
    {
        return plants[index];
    }

    public int GetRandomPlantIndex()
    {
        return Random.Range(0, plants.Length);
    }
}