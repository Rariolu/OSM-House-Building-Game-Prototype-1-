#pragma warning disable IDE0017
#pragma warning disable IDE0018
#pragma warning disable IDE0039
#pragma warning disable IDE0044
#pragma warning disable IDE1005

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using SysRand = System.Random;

public static class Util
{
    static bool isPaused = false;
    /// <summary>
    /// A boolean which represents whether or not the game is paused
    /// (and adjusts the time scale accordingly).
    /// </summary>
    public static bool IsPaused
    {
        get
        {
            return isPaused;
        }
        set
        {
            isPaused = value;
            Time.timeScale = isPaused ? 0f : 1f;
            Timer timer;
            if (SingletonUtil.InstanceAvailable(out timer))
            {
                if (isPaused)
                {
                    timer.StopTimer();
                }
                else
                {
                    timer.StartTimer();
                }
            }
        }
    }

    public static SysRand rand = new SysRand();

    /// <summary>
    /// The scenes that have been loaded, used to preserve scene continuity
    /// so that the "previous" scene can be easily identified.
    /// </summary>
    static ViewableStack<SCENE> sceneStack = new ViewableStack<SCENE>();

    /// <summary>
    /// The amount of scenes on the stact.
    /// </summary>
    public static int StackCount
    {
        get
        {
            return sceneStack.Count;
        }
    }

    /// <summary>
    /// Open the fixings minigame to edit a particular intersection.
    /// </summary>
    /// <param name="intersection"></param>
    public static void ApplyFixturesToIntersection(Intersection intersection)
    {
        FixingsUtil.ApplyFixingsToIntersection(intersection);
        LoadScene(SCENE.Fixings_MiniGame, LoadSceneMode.Additive);
        SceneObjectScript gameScene;
        if (SceneObjectScript.InstanceExists(SCENE.InGame, out gameScene))
        {
            gameScene.SetActive(false);
        }
    }

    /// <summary>
    /// Returns true if the gameobject attached to the given component has the given tag.
    /// </summary>
    /// <typeparam name="T">The generic component type (e.g. BoxCollider).</typeparam>
    /// <param name="component"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static bool ComponentHasTag<T>(this T component, TAG tag) where T : Component
    {
        return component.gameObject.GameObjectHasTag(tag);
    }

    public static Array GetEnumValues<T>() where T : struct
    {
        Type type = typeof(T);
        if (!type.IsEnum)
        {
            return null;
        }
        return Enum.GetValues(type);
    }

    /// <summary>
    /// Attempts to parse a given string to find an enum value of a given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="text"></param>
    /// <param name="value">Either a valid match value or the default value of T.</param>
    /// <returns>True if a valid match is found, false if not</returns>
    public static bool EnumTryParse<T>(this string text, out T value) where T : struct
    {
        //
        value = default(T);
        Type type = typeof(T);
        if (!type.IsEnum || text == null)
        {
            return false;
        }
        foreach(T eVal in GetEnumValues<T>())
        {
            if (eVal.NormaliseString() == text.NormaliseString())
            {
                value = eVal;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Place given values into a predetermined format.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string FormatText(this string text, params object[] args)
    {
        return string.Format(text, args);
    }

    /// <summary>
    /// Returns true if the gameobject has a specified tag.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static bool GameObjectHasTag(this GameObject gameObject, TAG tag)
    {
        return gameObject.tag.StringEquals(tag.ToString());
    }

    /// <summary>
    /// Returns a string with the name of the collection on the top line and each
    /// subsequent line being filled by a value from the collection.
    /// </summary>
    /// <typeparam name="T">The type of the values in the collection.</typeparam>
    /// <param name="collection"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetCollectionString<T>(this IEnumerable<T> collection, string name)
    {
        if (collection == null)
        {
            return name;
        }
        if (collection.Count() < 1)
        {
            return name;
        }
        StringBuilderPro sbp = new StringBuilderPro();
        sbp.AppendLine(name);
        foreach(T val in collection)
        {
            sbp.AppendLine(val.ToString());
        }
        return sbp.ToString();
    }

    /// <summary>
    /// Directs a ray in the direction of the cursor
    /// and determines where, if anywhere, the ray hits.
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="hitpoint"></param>
    /// <returns></returns>
    public static bool GetMouseHit(Camera cam, out Vector3 hitpoint)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            hitpoint = hit.point;
            return true;
        }
        hitpoint = new Vector3();
        return false;
    }

    /// <summary>
    /// Get a rectangle which represents the occupied space that the game
    /// takes on the screen.
    /// </summary>
    /// <param name="cam"></param>
    /// <returns></returns>
    public static Rect GetScreenRect(Camera cam)
    {
        Vector3 bottomLeftCorner = cam.ViewportToScreenPoint(new Vector3());
        return new Rect(bottomLeftCorner.x, bottomLeftCorner.y, bottomLeftCorner.x + Screen.width, bottomLeftCorner.y + Screen.height);
    }

    public static string PadNumber(this int number, int digits)
    {
        return number.ToString().PadLeft(digits, '0');
    }

    /// <summary>
    /// Returns true if a prefab has been destroyed in the current game.
    /// </summary>
    /// <returns></returns>
    public static bool PrefabHasBeenDestroyed()
    {
        //Finds construction util and uses that to determine if
        //a prefab has been destroyed.
        ConstructionUtil util;
        if (SingletonUtil.InstanceAvailable(out util))
        {
            return util.Destroyed > 0;
        }
        //This shouldn't be possible if the game has begun,
        //but if a util isn't available then it's presumed
        //that a prefab hasn't been destroyed.
        return false;
    }

    /// <summary>
    /// Open the UI scene over the current one.
    /// </summary>
    //public static void LoadGameUI()
    //{
    //    LoadScene(SCENE.InGameUI, LoadSceneMode.Additive);
    //}


    /// <summary>
    /// Load a given scene either to replace the current one or to be open alongside it.
    /// </summary>
    /// <param name="scene">The name of the scene to be loaded</param>
    /// <param name="loadSceneMode">The scene mode, determines whether the scene replaces the current one (Single) or opens alongside it (Additive).</param>
    public static void LoadScene(SCENE scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool appendToStack = true)
    {
        if (appendToStack)
        {
            PushScene(scene);
        }
        SceneManager.LoadScene(scene.ToString(),loadSceneMode);

    }

    /// <summary>
    /// Returns the point exactly halfway between two other points.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector3 MidPoint(Vector3 a, Vector3 b)
    {
        return (a + b) / 2f;
    }

    /// <summary>
    /// Trims text and sets it to a uniform case for easy comparison.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string NormaliseString(this object obj)
    {
        return obj.ToString().Trim().ToLower();
    }

    /// <summary>
    /// Prevent objects of the two given layers from colliding
    /// </summary>
    /// <param name="layer1"></param>
    /// <param name="layer2"></param>
    public static void PreventCollisions(LAYER layer1, LAYER layer2)
    {
        Physics.IgnoreLayerCollision((int)layer1, (int)layer2, true);
    }

    /// <summary>
    /// The previous significant scene to be loaded.
    /// </summary>
    /// <returns></returns>
    public static SCENE PrevMainScene()
    {
        if (sceneStack.Count > 1)
        {
            return sceneStack.Previous();//.mainScene;
        }
        Logger.Log("Not enough elements on Stack");
        return 0;
    }

    /// <summary>
    /// Add the given scene to the stack.
    /// </summary>
    /// <param name="scene"></param>
    public static void PushScene(SCENE scene)
    {
        sceneStack.Push(scene);
    }

    /// <summary>
    /// Close the game: stops the playthrough if
    /// running in the editor and stops the executable
    /// file if the build is running.
    /// </summary>
    public static void Quit()
    {
        //Uses preprocessor logic.
        //If program is running in the Unity Editor.
        #if UNITY_EDITOR
            //Stop the editor (set isPlaying to false which terminates play).
            UnityEditor.EditorApplication.isPlaying = false;
        //Otherwise (i.e. if program in running inside an executable in our case).
        #else
            //Close the executable.
            Application.Quit();
        #endif
    }

    /// <summary>
    /// Return to the previously open scene (the second-to-top scene on the stack).
    /// Reactivate its game objects if the scene is still open, or load the scene
    /// if it isn't.
    /// </summary>
    public static void ReturnToPreviousScene()
    {
        if (sceneStack.Count > 0)
        {
            SCENE prevSceneEnum = sceneStack.Previous();
            SceneObjectScript prevScene;
            if (SceneObjectScript.InstanceExists(prevSceneEnum, out prevScene))
            {
                prevScene.SetActive(true);
            }
            else
            {
                Util.LoadScene(prevSceneEnum,LoadSceneMode.Single,false);
            }
            SCENE currentScene = sceneStack.Pop();
            UnloadScene(currentScene);
        }
    }

    /// <summary>
    /// Returns a float which is rounded to the nearest multiple of 
    /// a given number.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="multiple"></param>
    /// <returns></returns>
    public static float RoundToNearestMultiple(this float num, float multiple)
    {
        float mod = num % multiple;
        bool roundUp = mod >= multiple/2f;
        return roundUp ? (num + multiple) - mod : (num - mod);
    }

    /// <summary>
    /// Returns a Vector3 which rounds all of the given vec3's values
    /// to the nearest multiple of a given number.
    /// </summary>
    /// <param name="vec3"></param>
    /// <param name="multiple"></param>
    /// <returns></returns>
    public static Vector3 RoundToNearestMultiple(this Vector3 vec3, float multiple)
    {
        return new Vector3(vec3.x.RoundToNearestMultiple(multiple), vec3.y.RoundToNearestMultiple(multiple), vec3.z.RoundToNearestMultiple(multiple));
    }

    /// <summary>
    /// Returns a Vector3 which rounds all of the given vec3's values
    /// to the nearest multiple of a given number, before applying
    /// an offset to each value.
    /// </summary>
    /// <param name="vec3"></param>
    /// <param name="multiple"></param>
    /// <returns></returns>
    public static Vector3 RoundToNearestMultiple(this Vector3 vec3, float multiple, float offset)
    {
        Vector3 ret = vec3.RoundToNearestMultiple(multiple);
        ret.x += offset;
        ret.z += offset;
        return ret;
    }

    /// <summary>
    /// Returns a Vector3 with all its values rounded to the nearest whole number.
    /// </summary>
    /// <param name="vec3"></param>
    /// <returns></returns>
    public static Vector3 RoundVec3(this Vector3 vec3)
    {
        return new Vector3(Mathf.Round(vec3.x), Mathf.Round(vec3.y), Mathf.Round(vec3.z));
    }

    /// <summary>
    /// Return true if the requested scene has been loaded.
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    public static bool SceneLoaded(SCENE scene)
    {
        Scene sceneInst = SceneManager.GetSceneByName(scene.ToString());
        return sceneInst.isLoaded;
    }
    
    /// <summary>
    /// Sets the current contract that is being looked
    /// at and loads the main game scene.
    /// </summary>
    /// <param name="contract"></param>
    public static void SetContract(Contract contract)
    {
        ConstructionUtil.SetContract(contract);
        LoadScene(SCENE.InGame);
    }

    /// <summary>
    /// Sets the consequence of a given event (defaults to "PointerClick" as this
    /// is the most often used).
    /// </summary>
    /// <param name="gObject">The affected game object.</param>
    /// <param name="action"></param>
    /// <param name="triggerType"></param>
    public static void SetEvent(this GameObject gObject, UnityAction<BaseEventData> action, EventTriggerType triggerType = EventTriggerType.PointerClick)
    {
        //Gets the "EventTrigger" component, adds one if there isn't one already attached.
        EventTrigger eventTrigger = gObject.GetComponent<EventTrigger>() ?? gObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener(action);
        eventTrigger.triggers.Add(entry);
    }


    public static void SetExitState(EXIT_STATE exitState)
    {
        EndGameUtil.SetExitState(exitState);
        Util.LoadScene(SCENE.EndScene);
    }

    /// <summary>
    /// Load the house view scene with a particular house.
    /// </summary>
    /// <param name="house"></param>
    public static void ShowHouse(House house)
    {
        HouseViewSingle.ShowHouse(house);
        LoadScene(SCENE.HouseViewing,LoadSceneMode.Additive);
    }

    /// <summary>
    /// Load the test scene which shows an estimate of the FPS onscreen.
    /// </summary>
    public static void ShowTest()
    {
        SceneManager.LoadScene(SCENE.SpecTestScene.ToString(),LoadSceneMode.Additive);
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public static void StartGame()
    {
        LoadScene(SCENE.ContractSelection);
    }

    /// <summary>
    /// Compares two string values after they've been normalised.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static bool StringEquals(this string s1, string s2)
    {
        return s1.NormaliseString() == s2.NormaliseString();
    }

    /// <summary>
    /// Close the requested scene.
    /// </summary>
    /// <param name="scene"></param>
    public static void UnloadScene(SCENE scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }
}