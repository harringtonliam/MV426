using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titles : MonoBehaviour
{
    Text titleText;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GetComponent<Text>();
        titleText.text = string.Empty;

        StartCoroutine(DisplayTitle());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplayTitle()
    {

        yield return new WaitForSeconds(    1f);
        titleText.text = "MV462";

    }
}
