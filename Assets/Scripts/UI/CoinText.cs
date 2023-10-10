
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class CoinText : MonoBehaviour
{
    [SerializeField] private string textCoins; // Sound: or Music:
    private TMP_Text tmpText; // Use TMP_Text instead of Text
    private int numberOfCoins;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>(); // Get the TMP_Text component
    }

    public void UpdateNumberOfCoins()
    {
        numberOfCoins += 1;

        if (numberOfCoins < 10) {
            tmpText.text = "0" + numberOfCoins.ToString();
        } else {
            tmpText.text = numberOfCoins.ToString();
        }
        
    }
}
