using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [Header("石頭消失")]
   public GameObject[] stones;
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.tag == "石頭")
        {

            stones[0].SetActive(false);
           // stones[1].SetActive(false);
           //stones[2].SetActive(false);
        }
    }
}
