using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class SubtaskCheckbox : MonoBehaviour
{
    public Sprite noneSprite;
    public Sprite failedSprite;
    public Sprite succeededSprite;

    Image image;
    Image Image
    {
        get
        {
            return image ?? (image = GetComponent<Image>());
        }
    }

    public enum COMPLETION_STATE
    {
        NONE, FAILED, SUCCEEDED
    }

    COMPLETION_STATE completionState;
    public COMPLETION_STATE CompletionState
    {
        get
        {
            return completionState;
        }
        set
        {
            completionState = value;
            Sprite sprite = completionState == COMPLETION_STATE.NONE ? noneSprite :
            (
                completionState == COMPLETION_STATE.FAILED ? failedSprite : succeededSprite
            );
            Image.sprite = sprite;
            Logger.Log("Completion state changed {0}.", completionState);
        }
    }

    private void Awake()
    {
        CompletionState = COMPLETION_STATE.NONE;
    }
}