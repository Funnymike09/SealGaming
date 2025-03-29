using UnityEngine;

public class SealInfo : MonoBehaviour
{
   
   public bool Malnourished, Injured, Sick;
   
   public float Health = 0.0f;
   
   [SerializeField] float healRate;

    void Awake()
    {
        if (Random.Range(1, 2) == 1) 
        {
            Malnourished = true;
        }
        
        if (Random.Range(1, 2) == 1) 
        {
            Injured = true;
        }
        
        if (Random.Range(1, 2) == 1) 
        {
            Sick = true;
        }
    }

    void Update()
    {
        if (Health >= 100.0f) 
        {
            EconomyManager.instance.AddMoney(100);
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("SealBuilding") && Input.GetMouseButtonDown(0) && other.gameObject.GetComponent<SealBuilding>().currentSeal == null) 
        {
            Camera.main.GetComponent<SelectObject>().currentObject = null;
            GetComponent<MeshRenderer>().enabled = false;
            InvokeRepeating(nameof(Heal), 0.1f, 1f);
            other.gameObject.GetComponent<SealBuilding>().currentSeal = this;
        } 
    }
    
    void Heal() 
    {
        Health += healRate;
    }

}
