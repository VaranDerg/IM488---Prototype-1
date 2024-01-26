using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: Liz
/// Description: Allows for easy scene loading with transitions. Nice and pretty!
/// </summary>
public class SceneTransitions : MonoBehaviour
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

    [SerializeField] private int[] _arenaScenes;
    [Space]
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

    /// <summary>
    /// Function to be called externally in order to load a scene.
    /// </summary>
    /// <param name="typeOfTransition">A TransitionType enum for the type of transition you want to play between scenes.</param>
    /// <param name="sceneToLoad">The scene build index to load.</param>
    public void LoadSceneWithTransition(TransitionType typeOfTransition, int sceneToLoad)
    {
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
    }

    /// <summary>
    /// Gets the build index of the active scene.
    /// </summary>
    /// <returns>The active scene's build index.</returns>
    public int GetBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// Used for loading a random Arena.
    /// </summary>
    /// <returns>The index of a random arena scene.</returns>
    public int GetRandomArenaScene()
    {
        return _arenaScenes[Random.Range(0, _arenaScenes.Length)];
    }

    /// <summary>
    /// Used for loading a random Arena.
    /// </summary>
    /// <param name="excludeScene">A scene you want to exclude from selection. Pass in current build index to prevent repeated arenas.</param>
    /// <returns>The index of a random arena scene. The excludeScene is not possible.</returns>
    public int GetRandomArenaScene(int excludeScene)
    {
        List<int> possibleArenaScenes = new List<int>();
        foreach (int scene in _arenaScenes)
        {
            if (scene != excludeScene)
            {
                possibleArenaScenes.Add(scene);
            }
        }

        return possibleArenaScenes[Random.Range(0, _arenaScenes.Length)];
    }

    /// <summary>
    /// Calculates the animation time of an animation.
    /// </summary>
    /// <param name="anims">The animator to search through.</param>
    /// <param name="animationName">The name of the animation to find.</param>
    /// <returns>The animation time if an animation is found. 0 otherwise, and flags a warning.</returns>
    public float GetAnimationTime(Animator anims, string animationName)
    {
        AnimationClip[] clips = anims.runtimeAnimatorController.animationClips;
        foreach (AnimationClip c in clips)
        {
            if (c.name == animationName)
            {
                return c.length;
            }
        }

        Debug.LogWarning($"Could not find animation named {animationName} in {anims.name}");
        return 0;
    }
}
