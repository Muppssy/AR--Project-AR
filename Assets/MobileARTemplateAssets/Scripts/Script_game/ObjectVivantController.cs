using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class ObjectVivantController : MonoBehaviour

{
    [Header("Layers")]
    public LayerMask layerSol;
    public LayerMask layerVivant;

   

    public VivantConfig configuration;
    public MeshRenderer renderer;
    public Rigidbody rb;

       private Vector3 _target;
    private float _targetTimer;
    private float _jumpTimer;
    
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float random = Random.value;
        float randomizeSize = Mathf.Lerp(configuration.tailleRandom.x, configuration.tailleRandom.y, random);
        transform.localScale = Vector3.one * randomizeSize;
        renderer.sharedMaterial = configuration.materiauxRandom[Random.Range(0, configuration.materiauxRandom.Count)];
        _target=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
          _targetTimer -= Time.deltaTime;
        if (_targetTimer <= 0f)
        {
          if (TryPickTarget(out _target))
          {
            
              _targetTimer = Random.Range(configuration.tempsAttente.x, configuration.tempsAttente.y);
          }
            else
            {
                _targetTimer = 0.1f;
            }
        }
        
        _jumpTimer -= Time.deltaTime;
    }
    
      bool TryPickTarget(out Vector3 t)
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3 p = transform.position + new Vector3(
                Random.Range(-configuration.rayonMouvement, configuration.rayonMouvement),
                2f,
                Random.Range(-configuration.rayonMouvement, configuration.rayonMouvement)
            );

          
            if (Physics.Raycast(p, Vector3.down, out var hit2, 5f, layerSol) == false)
            {
                continue;
            }

            if (Physics.SphereCast(p, 0.5f, Vector3.down, out hit2, 10f, layerVivant))
            {
                continue;
            }

            t=new Vector3(hit2.point.x, transform.position.y, hit2.point.z);
            return true;
        }

        t=Vector3.zero;
        return false;
    }

    private void FixedUpdate()  
    {
       var to = (_target - rb.position);
       to.y = 0f;
       rb.AddForce(to.normalized * configuration.acceleration, ForceMode.Acceleration);
       rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, configuration.vitesseMax);

       if (_jumpTimer <= 0f)
       {
           rb.AddForce(Vector3.up * configuration.puissanceSaut, ForceMode.Acceleration);
           _jumpTimer = Random.Range(configuration.tempsSaut.x, configuration.tempsSaut.y);
       }
    }
    
}
