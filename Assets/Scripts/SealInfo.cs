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
