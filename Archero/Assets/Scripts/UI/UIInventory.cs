using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] Text _textCoinInventory;
    public static int GameScore;
    public static int WinScore;

    private void Start()
    {
        LoadCoin(WinScore);
        WinScore = 0;
    }
    
    public void LoadCoin(int winScore)
    {
        GameScore += winScore;
        _textCoinInventory.text = GameScore.ToString();
    }
}

