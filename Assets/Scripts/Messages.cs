using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messages : MonoBehaviour
{
    [Tooltip("In Seconds")] [SerializeField] float messageDisplayTime = 5f;

    Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        messageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayMessage(string message)
    {
        if (messageText == null)
        {
            return;
        }
        StartCoroutine(DisplayMessageForTime(message));
    }

    private IEnumerator DisplayMessageForTime(string message)
    {
        messageText.text = message;
        yield return new WaitForSeconds(messageDisplayTime);
        messageText.text = string.Empty;
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}
