using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite brokenHeart;
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();

        //Checking if the player health is equal to the number os hearts
        if(player.health < numberOfHearts){
            player.health = numberOfHearts;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //Loop for the UI Lifes
        for (int i = 0; i < hearts.Length; i++){
            if(i < numberOfHearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }

            if(i< player.health){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = brokenHeart;
            }
        }
    }
}
