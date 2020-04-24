using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public UIButton buildButton;
    public UIButton wallButton;
    public UIButton brochureButton;
    public UIButton subtaskButton;

    public Tutorial introTutorial;
    public Tutorial swipeTutorial;
    public Tutorial buildTutorial;
    public Tutorial prefabTutorial;
    public Tutorial prefab2Tutorial;
    public Tutorial destroyTutorial;
    public Tutorial brochureTutorial;
    public Tutorial turnTutorial;
    public Tutorial subtaskTutorial;
    public Tutorial fixingsTutorial;

    private void Awake()
    {
        SingletonUtil.SetInstance(this);
    }

    public void BeginTutorial()
    {
        introTutorial.Activate();
        introTutorial.TutorialClosed += IntroClosed;
    }

    void IntroClosed()
    {
        swipeTutorial.Activate();
        swipeTutorial.TutorialClosed += SwipeClosed;
    }

    bool swiped = false;
    void SwipeClosed()
    {
        CameraMovementScript cam;
        if (SingletonUtil.InstanceAvailable(out cam))
        {
            cam.CameraMoved += (sender) =>
            {
                if (!swiped)
                {
                    buildTutorial.Activate();
                    buildTutorial.TutorialClosed += BuildClosed;
                    swiped = true;
                }
            };
        }
    }


    bool buildClick = false;
    bool wallClick = false;
    void BuildClosed()
    {
        buildButton.Click += (sender) =>
        {
            if (!buildClick)
            {
                prefabTutorial.Activate();
                wallButton.Click += (s) =>
                {
                    if (!wallClick)
                    {
                        prefabTutorial.Destroy();
                        prefab2Tutorial.Activate();
                        PrefabCounter counter;
                        if (SingletonUtil.InstanceAvailable(out counter))
                        {
                            counter.NewPrefabSelected += PrefabSelected;
                        }
                        wallClick = true;
                    }
                };
                buildClick = true;
            }
        };
    }

    bool placed = false;
    bool destroyed = false;
    bool selected = false;
    void PrefabSelected()
    {
        if (!selected)
        {
            prefab2Tutorial.Destroy();
            InGameSceneScript gameScene;
            if (SingletonUtil.InstanceAvailable(out gameScene))
            {
                gameScene.PrefabPlaced += () =>
                {
                    if (!placed)
                    {
                        destroyTutorial.Activate();
                        gameScene.PrefabDestroyed += () =>
                        {
                            if (!destroyed)
                            {
                                destroyTutorial.Destroy();
                                brochureTutorial.Activate();
                                brochureButton.Click += BrochureButton_Click;
                                destroyed = true;
                            }
                        };
                        placed = true;
                    }
                };
            }
            selected = true;
        }
    }

    bool brochureClick = false;

    void BrochureButton_Click(UIButton sender)
    {
        if (!brochureClick)
        {
            brochureTutorial.Destroy();
            turnTutorial.Activate();
            turnTutorial.TutorialClosed += TurnClosed;
            brochureClick = true;
        }
    }
    bool intersectionSpawn = false;
    bool intersectionClick = false;
    void TurnClosed()
    {
        subtaskTutorial.Activate();
        subtaskButton.Click += (sender) =>
        {
            subtaskTutorial.Destroy();
            InGameSceneScript gameScene;
            if (SingletonUtil.InstanceAvailable(out gameScene))
            {
                gameScene.IntersectionSpawned += () =>
                {
                    if (!intersectionSpawn)
                    {
                        fixingsTutorial.Activate();
                        gameScene.IntersectionClicked += () =>
                        {
                            if (!intersectionClick)
                            {
                                fixingsTutorial.Destroy();
                            }
                        };
                        intersectionSpawn = true;
                    }
                };
            }
        };
    }

    // Use this for initialization
    void Start()
    {
        BeginTutorial();
    }
}