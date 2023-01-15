using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5.0F;

    private GameManager gameManager;
    private float firstTouchX;

    public int coffeeCount = 0;
    public List<GameObject> coffees;

    void Start()
    {
        coffees = new List<GameObject>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.currentGameState != GameState.Start)
        {
            return;
        }

        for(int i = 0; i < coffees.Count; i++)
        {
            coffees[i].transform.position = new Vector3(Mathf.Lerp(transform.position.x, coffees[i].transform.position.x, 0.01F * Time.deltaTime), coffees[i].transform.position.y, coffees[i].transform.position.z);
        }

        float diff = 0;

        Vector3 forwardVector = new Vector3(x: 0, y: 0, z: speed * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            firstTouchX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButton(0))
        {
            float lastTouchX = Input.mousePosition.x;
            diff = lastTouchX - firstTouchX;  
            forwardVector += new Vector3(x: diff * Time.deltaTime, y: 0, z: 0);
            firstTouchX = lastTouchX; // or i can multiple forwardVectos's x axis with movementMultipier.
        }
        
        transform.position += forwardVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectable"))
        {
            other.transform.SetParent(transform); // Set object's parent as player.
            coffees.Add(other.gameObject);
            coffeeCount++;
        }
        else if(other.CompareTag("Finish"))
        {            
            if(coffeeCount == 0)
            {
                gameManager.EndGame();
            }
            else
            {
                coffees[coffeeCount - 1].SetActive(false);
                coffees.RemoveAt(coffees.Count -1);
                coffeeCount--;
            }            
        }
    }
}