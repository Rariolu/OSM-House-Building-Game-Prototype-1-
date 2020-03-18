using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour
{
    public Text lblExitState;
    // Start is called before the first frame update
    void Start()
    {
        EndGameUtil endUtil;
        if (EndGameUtil.InstanceAvailable(out endUtil) &&
            lblExitState != null)
        {
            lblExitState.text = endUtil.ExitState.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}