using UnityEngine;
using UnityEngine.UIElements;

public class SealInfo : MonoBehaviour
{
   
   public bool Malnourished, Injured, Sick;
   
   public float Health = 1;
    [SerializeField]
    public ProgressbarUI progressbarUI;
    private float MaxHealth;
   [SerializeField] float healRate;
   
   private bool isInTrigger;
   private SealBuilding sealBuilding;

    void Awake()
    {
        if (Random.Range(0, 2) == 1) 
        {
            Malnourished = true;
        }
        
        if (Random.Range(0, 2) == 1) 
        {
            Injured = true;
        }
        
        if (Random.Range(0, 2) == 1) 
        {
            Sick = true;
        }
        MaxHealth = 100;
    }

    void Update()
    {
        if (Health >= 100.0f) 
        {
            EconomyManager.instance.AddMoney(100);
            Destroy(gameObject);
            Destroy(progressbarUI.gameObject);
        }
        
        if(isInTrigger && Input.GetMouseButtonDown(0) && sealBuilding.currentSeal == null) 
        {
            GetComponent<Tip>().ShowOver();
            Camera.main.GetComponent<SelectObject>().currentObject = null;
            GetComponent<MeshRenderer>().enabled = false;
            InvokeRepeating(nameof(Heal), 0.1f, 1f);
            sealBuilding.currentSeal = this;
            GameObject[] sealBuildings = GameObject.FindGameObjectsWithTag("SealBuilding");
            foreach(GameObject go in sealBuildings) 
            {
                Destroy(go.GetComponent<Outline>());
            }
        } 
    }

    // void OnTriggerStay(Collider other)
    // {
    //     if(other.gameObject.CompareTag("SealBuilding") && Input.GetMouseButtonDown(0) && other.gameObject.GetComponent<SealBuilding>().currentSeal == null) 
    //     {
    //         GetComponent<Tip>().ShowOver();
    //         Camera.main.GetComponent<SelectObject>().currentObject = null;
    //         GetComponent<MeshRenderer>().enabled = false;
    //         InvokeRepeating(nameof(Heal), 0.1f, 1f);
    //         other.gameObject.GetComponent<SealBuilding>().currentSeal = this;
    //     } 
    // }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("SealBuilding")) 
        {
            isInTrigger = true;
            sealBuilding = other.GetComponent<SealBuilding>();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("SealBuilding")) 
        {
            isInTrigger = false;
            sealBuilding = null;
        }
    }

    void Heal() 
    {
        Health += healRate;
        progressbarUI.SetProgress(Health / MaxHealth, 3);
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        progressbarUI.transform.SetParent(canvas.transform);
        if (progressbarUI.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }
}
