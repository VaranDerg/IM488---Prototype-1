using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Liz
/// Description: Allows for easy scene loading with transitions. Nice and pretty!
/// </summary>
public class SceneTransitions : BaseUIElement
{
    public static SceneTransitions Instance;

    private const string LEFTRIGHT_ANIMATION = "LeftRight";
    private const string FADE_ANIMATION = "Fade";
    private const string ANIM_ENTER_SUFFIX = "Start";
    private const string ANIM_EXIT_SUFFIX = "End";

    public enum TransitionType
    {
        LeftRight,
        Fade
    }

    public static bool TransitionActive;

    [SerializeField] private Animator _animator;

    /// <summary>
    /// Singleton pattern.
    /// </summary>
    private void Awake()
    {
        if (Instance == null && Instance != this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    /// <summary>
    /// Runs whenever the scene is loaded.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ManagerParent.Instance.Audio.PlaySceneMusic();
    }

    /// <summary>
    /// Use this to load the SpellSelectScene.
    /// </summary>
    /// <param name="losingPlayer">The player who lost the last round. Alternatively, pass -1 to let both players choose a spell.</param>
    /// 
    public void LoadSpellSelectScene(SpellManager.SpellSelectionMode mode)
    {
        ManagerParent.Instance.Spells.PrepareSpellSelectionState(mode);

        LoadSceneWithTransition(TransitionType.LeftRight, ManagerParent.Instance.Game.GetSpellSelectScene());
    }

    /// <summary>
    /// Function to be called externally in order to load a scene.
    /// </summary>
    /// <param name="typeOfTransition">A TransitionType enum for the type of transition you want to play between scenes.</param>
    /// <param name="sceneToLoad">The scene build index to load.</param>
    public void LoadSceneWithTransition(TransitionType typeOfTransition, int sceneToLoad)
    {
        // Enable controls for both players
        ControllerInputManager.Instance.EnableMNK();
        ControllerInputManager.Instance.EnableGamepad();

        StartCoroutine(SceneTransition(TransitionNameFromTransitionType(typeOfTransition), sceneToLoad));
    }

    /// <summary>
    /// Gets the animation name from a TransitionType enum. Exists as typo prevention.
    /// </summary>
    /// <param name="typeOfTransition">The type of transition you are trying to find.</param>
    /// <returns>The string name of that transition.</returns>
    public string TransitionNameFromTransitionType(TransitionType typeOfTransition)
    {
        string parameter = "";

        switch (typeOfTransition)
        {
            case TransitionType.Fade:
                parameter = FADE_ANIMATION;
                break;
            case TransitionType.LeftRight:
                parameter = LEFTRIGHT_ANIMATION;
                break;
        }

        return parameter;
    }

    /// <summary>
    /// Loads a new scene with a transition. Sets "TransitionActive" to true while running.
    /// </summary>
    /// <param name="transitionName">The name of the transition to use.</param>
    /// <param name="sceneToLoad">The buildindex of the scene to load.</param>
    private IEnumerator SceneTransition(string transitionName, int sceneToLoad)
    {
        string enterAnimName = transitionName + ANIM_ENTER_SUFFIX;
        string exitAnimName = transitionName + ANIM_EXIT_SUFFIX;
        float enterTime = GetAnimationTime(_animator, enterAnimName);
        float exitTime = GetAnimationTime(_animator, exitAnimName);

        TransitionActive = true;
        _animator.Play(enterAnimName);

        yield return new WaitForSecondsRealtime(enterTime);

        SceneManager.LoadScene(sceneToLoad);
        _animator.Play(exitAnimName);

        yield return new WaitForSecondsRealtime(exitTime);

        TransitionActive = false;

        //SceneManager.LoadScene(aScene.n);
    }

    /// <summary>
    /// Gets the build index of the active scene.
    /// </summary>
    /// <returns>The active scene's build index.</returns>
    public int GetBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
