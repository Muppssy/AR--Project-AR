using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="ScriptableObjects/VivantConfig")]

public class VivantConfig : ScriptableObject
{

    [Header("Apparition")]
    public Vector2 tailleRandom;
    public Vector2 masseRandom;
    public Vector2 tempsAttente;
    public List<Material> materiauxRandom = new();
    
   

     [Header("Mouvements")]
    public float rayonMouvement;
    
    [Header("Vitesses")]
    public float acceleration;
    public float vitesseMax;
    
    [Header("Saut")]
    public Vector2 tempsSaut;
    public float puissanceSaut;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
