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

    public void ReduceDifficulty()
    {
        FindObjectOfType<RunnerManager>().ReduceDifficulty();
    }

    public void DoubleScore()
    {
        FindObjectOfType<RunnerManager>().DoubleScore();
    }

}