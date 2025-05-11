using UnityEngine;

public class NPCCounter : MonoBehaviour
{
   private void OnDestroy()
    {
        if(NPCManager.instance != null)
        {
            NPCManager.instance.NPCDestroyed(gameObject);
        }
    }
}
