using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private PlayerPartsRandomiser skinRandomiser;
    private Health myHealths;
    [SerializeField] public Transform defaultSpawnPosition;

    private void Awake() 
    {
        skinRandomiser = GetComponentInChildren<PlayerPartsRandomiser>();
        myHealths = GetComponentInChildren<Health>();
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        skinRandomiser.RandomizeParts();
        transform.position = SaveSystem.LoadPlayerPosition();
    }

    public void SavePlayerPosition()
    {
        SaveSystem.SavePlayerPosition(transform.position);
    }
    public void LoadPlayerPosition()
    {
        if (SaveSystem.LoadPlayerPosition() == null)
            return;
        transform.position = SaveSystem.LoadPlayerPosition();
    }
}
