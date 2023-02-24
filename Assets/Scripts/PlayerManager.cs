using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    #region Singelton
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;
    public Slider HPBar;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
