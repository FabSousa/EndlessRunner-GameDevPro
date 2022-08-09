using UnityEngine;

public class SaveGame : MonoBehaviour
{
    [SerializeField] GameMode gameMode;

    public void Save()
    {
        Cherries();
        Peanuts();
        LastScore();
        HighScore();
    }

    private void Cherries()
    {
        PlayerPrefs.SetInt(GameConsts.Cherries, PlayerPrefs.GetInt(GameConsts.Cherries) + GameMode.CherryCount);
    }

    private void Peanuts()
    {
        PlayerPrefs.SetInt(GameConsts.Peanuts, PlayerPrefs.GetInt(GameConsts.Peanuts) + GameMode.PeanutCount);
    }

    private void LastScore()
    {
        PlayerPrefs.SetInt(GameConsts.LastScore, gameMode.Score);
    }

    private void HighScore()
    {
        if (gameMode.Score <= PlayerPrefs.GetInt(GameConsts.HighScore)) return;
        PlayerPrefs.SetInt(GameConsts.HighScore, gameMode.Score);
    }

}
