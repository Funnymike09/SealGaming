using UnityEngine;

public class AmbientPlaya : MonoBehaviour
{
    /*
    [SerializeField]
    AudioClip AmbientThunder;
    [SerializeField]
    AudioClip Shore1;
    [SerializeField] AudioClip Shore2;
    [SerializeField] AudioClip Shore3;
    [SerializeField]
    AudioClip AmbientWindy;
    */
    [SerializeField]
    AudioClip AmbientClear;

    private void Awake()
    {
        //AudioManager.singleton.PlaySound(AmbientClear);// THIS IS JUST FOR ALPHA PLS MAKE A PROPER AUDIO SYSTEM, cheers

    }
    private void Start()
    {
        AudioManager.singleton.PlaySound(AmbientClear);
    }
}
