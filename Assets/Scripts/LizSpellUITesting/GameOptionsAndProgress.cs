using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionsAndProgress : MonoBehaviour
{
    public static int PlayerOneScore;
    public static int PlayerTwoScore;

    private const int WIN_SCENE = 5;
    private const int SPELL_SCENE = 1;

    [Header("Gameplay Information")]
    [SerializeField] [Range(3, 5)] private int _pointsToWin;
    [SerializeField] private float _winDelay;

    [Header("Player Information")]
    [SerializeField] private bool _playersRegenerateHP;

    public void IncreasePlayerScore(int player)
    {
        if (player == 1)
        {
            PlayerOneScore++;
            PlayerSpellManager.Instance.PrepareSpellSelectionState(PlayerSpellManager.SpellSelectionMode.PlayerOne);
        }
        else
        {
            PlayerTwoScore++;
            PlayerSpellManager.Instance.PrepareSpellSelectionState(PlayerSpellManager.SpellSelectionMode.PlayerTwo);
        }

        FindObjectOfType<GameplayMenu>().DisplayWin(player);

        StartCoroutine(ToNextGamePhaseProcess());
    }

    private IEnumerator ToNextGamePhaseProcess()
    {
        int nextScene;
        if (PlayerOneScore == _pointsToWin || PlayerTwoScore == _pointsToWin)
        {
            nextScene = WIN_SCENE;
        }
        else
        {
            nextScene = SPELL_SCENE;
        }

        yield return new WaitForSeconds(_winDelay);

        SceneTransitions.Instance.LoadSceneWithTransition(SceneTransitions.TransitionType.LeftRight, nextScene);
    }

    public int GetPointToWin()
    {
        return _pointsToWin;
    }

    public bool GetPlayersRegenerateHP()
    {
        return _playersRegenerateHP;
    }
}