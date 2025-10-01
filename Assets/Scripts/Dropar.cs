using UnityEngine;

public class Dropar : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private int itemDropRate = 100; 
    [SerializeField] private int itemMinDrop = 1;
    [SerializeField] private int itemMaxDrop = 3;

    public void Drop()
    {
        int rand = Random.Range(1, 101);  
        Debug.Log("Chance sorteada: " + rand);

        if (rand <= itemDropRate)
        {
            int amount = Random.Range(itemMinDrop, itemMaxDrop + 1); 

            for (int i = 0; i < amount; i++)
            {
                Instantiate(item, transform.position, Quaternion.identity);
            }
        }
    }
}
