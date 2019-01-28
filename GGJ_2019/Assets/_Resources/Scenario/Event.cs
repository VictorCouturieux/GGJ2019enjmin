using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string nameScript;
    public GameObject text;

//    public Transform target;

    private GameManager gameManager;

    private static bool shouldFoundDragon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
//        throw new System.NotImplementedException();*
        
        Debug.Log("pb");
        if (gameManager.CurrentWorldState == GameManager.WorldState.Knight)
        {
            Debug.Log("knight");
            if (nameScript == "tower")
            {
                if (other.gameObject.name == "Player")
                {
//                    text.SetActive(true);
                    Debug.Log("eventTower");
                    shouldFoundDragon = true;
                }
                
            }
            if (nameScript == "dragon")
            {
                if (other.gameObject.name == "Player" && shouldFoundDragon)
                {
                    
//                    text.SetActive(true);    
                    Debug.Log("eventDragon");
                }
            }
        }
        
        
        
    }
}
