using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
public class playerController : MonoBehaviour, Characters
{
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public bool chasing { get; set; }
    public bool isWalking { get; set; }

    public void attack(GameObject character)
    {
        Characters _enemy = character.GetComponent<Characters>();
        _enemy.currentHealth -= 5;
    }

    public void death()
    {
        throw new NotImplementedException();
    }
}
