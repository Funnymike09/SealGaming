using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SealInfo : MonoBehaviour
{
   
   public bool Malnourished, Injured, Sick;
   
   public float Health = 1;
    [SerializeField]
    public ProgressbarUI progressbarUI;
    [SerializeField] private float MaxHealth;
   [SerializeField] float healRate;
   [SerializeField] 
    AudioClip Spawn;
    [SerializeField]
    AudioClip DoneHeal;
    [SerializeField]
    AudioClip HealProcess;
    [SerializeField]
    AudioResource moner;

    [SerializeField] private int maxLifeSpan;
    private float currentLife;
    
    private bool isSealBeingHealed;


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
       // AudioManager.singleton.PlaySound(Spawn);
        AudioManager.singleton.PlaySoundListSpatialClip(gameObject, Spawn);
    }

    void Update()
    {
    
        if(!Camera.main.GetComponent<SelectObject>().isSealBeingMoved && !isSealBeingHealed) 
        {
            currentLife += Time.deltaTime;
        }
        
        if (currentLife > maxLifeSpan) 
        {
            Destroy(gameObject);
        }
    
        if (Health >= 100.0f) 
        {
            EconomyManager.instance.AddMoney(sealBuilding.moneyProduced);
            if (QuestManager.instance.currentQuest != null) 
            {
                if (QuestManager.instance.currentQuest.type == QuestDataSO.QUEST_TYPE.SEAL) QuestManager.instance.IncrementQuest();
            }
            AudioManager.singleton.PlaySoundListOnce(gameObject, moner);
            Destroy(gameObject);
            Destroy(progressbarUI.gameObject);
        }
        
        if(isInTrigger && Input.GetMouseButtonDown(0) && sealBuilding.currentSeal == null) 
        {
            GetComponent<Tip>().ShowOver();
            Camera.main.GetComponent<SelectObject>().currentObject = null;
            Camera.main.GetComponent<SelectObject>().isSealBeingMoved = false;
            GetComponent<MeshRenderer>().enabled = false;
            InvokeRepeating(nameof(Heal), 0.1f, 1f);
            sealBuilding.currentSeal = this;
            GameObject[] sealBuildings = GameObject.FindGameObjectsWithTag("SealBuilding");
            foreach(GameObject go in sealBuildings) 
            {
        AudioManager.singleton.PlaySoundListSpatialClip(gameObject, HealProcess);
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
        isSealBeingHealed = true;
        Health += sealBuilding.healRate;
        progressbarUI.SetProgress(Health / 100, 3);
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
