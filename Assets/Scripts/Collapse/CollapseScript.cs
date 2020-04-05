using UnityEngine;
using System.Collections;

public class CollapseScript : MonoBehaviour
{
    //public float collapseSpeed = 5f;
    public float delay = 5f;
    public float force = 1f;
    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }
    public void BeginCollapsing()
    {
        StartCoroutine(Collapse());
    }
    IEnumerator Collapse()
    {
        Floor.HideAll();
        //SnapPoint.DestroyAll();
        PrefabPlacedObject.AddRigidBodies(2f,force);
        yield return new WaitForSeconds(delay);
        Util.SetExitState(EXIT_STATE.LOSE,false);
    }
}