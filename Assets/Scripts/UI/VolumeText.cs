
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; // Sound: or Music:
    private TMP_Text tmpText; // Use TMP_Text instead of Text

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>(); // Get the TMP_Text component
    }


    public void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        tmpText.text = textIntro + volumeValue.ToString();
    }
}
