using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   

public class ShipGameMode : MonoBehaviour
{
    public PlayerActions player;
    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI gameOverDisplay;
    public Slider healthSlider;
    public bool gameOver = false;
    public int itemsCollected = 0;
    public bool itemPickup = false;
    public TextMeshProUGUI itemCollectedText;
    public GameObject eneymyShipTemplate;
    public static bool enemiesExists=true;
    public static int enemiesCount=5;
    public static int numofDesEnemyShips=0;
    public Slider toWinSlider;
    public TextMeshProUGUI gameWinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         this is where i added feature 3 and 4. Player will have to destroy 20 enemies to win. a message will be displayed when he win.
         further collisions with the player ship will be disabled after this. A slider will show the amount of enemy ships player have 
         destroyed.
         */
        toWinSlider.value = numofDesEnemyShips;
        if (numofDesEnemyShips >= 20)
        {
            gameWinDisplay.enabled = true;
            player.GetComponent<Collider>().enabled = false;
        }

        if (!enemiesExists)
        {
            int randomInt = Random.Range(0, 3);
            enemiesExists = true;
            StartCoroutine(spawnEnemies(randomInt));
        }
        healthDisplay.text = "Health: " + player.health;
        gameOverDisplay.enabled = gameOver;//gameOverDisplay.gameObject.SetActive(gameOver);
        healthSlider.value=player.health;
    }
    /*This is where I added my feature 1 and 2. 
     FEATURE 1: Enemies must spawn in a wave-based system that waits until 
    all enemies of a wave are destroyed before commencing the new wave.

    FEATURE 2: Enemy spawn points and behaviour must be varied and 
    unique between waves (not just random spawn points).

    Here I have made three patterns. A pattern will be selected reandomly.
    -in the first patter enemies will spawn in a horizontal line. number of enemies will be selected randomly. speed of these 
     enemies will be twice the speed of other enemies.
    -in the second pattern enemies will spawn in a triangular shaped manner. number of enemies will be selected randomly.
    -in the third pattern enemy spawn points will be random and the number of enemies will be selected randomly.
    */
    IEnumerator spawnEnemies(int randomInt)
    {
        yield return new WaitForSeconds(1.0f);
        if (randomInt==0)
        {
            int numofEnemies = Random.Range(5, 9);
            enemiesCount += numofEnemies;
            float xCoordinate = -6.0f;
            for (int i = 0; i < numofEnemies; i++)
            {
                GameObject enemyShipObject = Instantiate(eneymyShipTemplate,new Vector3(xCoordinate, 10f, 0.0f), Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f)));
                EnemyShip enemyShip= enemyShipObject.GetComponent<EnemyShip>();
                enemyShip.speed*=2;
                xCoordinate += 2.0f;
            }
        }
        if(randomInt==1)
        {
            int numofEnemies = Random.Range(5, 7)*2-1;
            enemiesCount += numofEnemies;
            float xCoordinate = 0.0f;
            GameObject enemyShipObject = Instantiate(eneymyShipTemplate, new Vector3(xCoordinate, 10f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            xCoordinate += 0.5f;
            yield return new WaitForSeconds(1.0f);
            for (int i = 0; i < numofEnemies/2; i++)
            {
                enemyShipObject = Instantiate(eneymyShipTemplate, new Vector3(xCoordinate, 10f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                enemyShipObject = Instantiate(eneymyShipTemplate, new Vector3(-xCoordinate, 10f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                xCoordinate += 2.0f;
                yield return new WaitForSeconds(1.0f);
            }
        }
        if (randomInt==2) 
        {
            int numofEnemies = Random.Range(5, 7);
            enemiesCount += numofEnemies;
            for (int i = 0; i < numofEnemies ; i++)
            {
                GameObject enemyShipObject = Instantiate(eneymyShipTemplate, new Vector3(Random.Range(-3, 3), 8.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    public void itemPickupOff()
    {
        itemPickup = false;
        StartCoroutine(delayAndSetVisibility());
    }

    public void itemPickupOn()
    {

        Debug.Log("hhhh");
        itemPickup = true;
        itemCollectedText.enabled = itemPickup;
        itemPickupOff();
    }
    
    IEnumerator delayAndSetVisibility()
    {
        yield return new WaitForSeconds(2.0f);
        itemCollectedText.enabled = itemPickup;
    }
}
