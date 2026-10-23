using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement PlayerMovement;
    public BoxCollider2D topCollider;
    private Text interactUI ;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI= GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && PlayerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            //descendre de l'ehelle
            PlayerMovement.isClimbing = false;
            topCollider.isTrigger = false ;
            return ;

        }
        if (isInRange && Input.GetKeyDown(KeyCode.E)){
            PlayerMovement.isClimbing = true ;
            topCollider.isTrigger = true; 
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            interactUI.enabled = true; 
            isInRange = true ;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false ;
            PlayerMovement.isClimbing = false ;
            topCollider.isTrigger = false ; 
            interactUI.enabled = false; 
        }
    }
}
