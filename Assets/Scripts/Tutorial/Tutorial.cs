using UnityEngine;
using System.Collections;

public delegate void TutorialClosed();

public class Tutorial : MonoBehaviour
{
    public TutorialClosed TutorialClosed;
    bool destroyed = false;
    // Use this for initialization
    void Start()
    {
        UIButton btnOk = GetComponentInChildren<UIButton>();
        btnOk.Click += (sender) =>
        {
            if (TutorialClosed != null)
            {
                TutorialClosed();
            }
            Destroy();
        };
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            destroyed = true;
            Destroy(gameObject);
        }
    }
}
