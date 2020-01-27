using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {
    public static bool textAnimatorRunning;

    // Start is called before the first frame update
    void Start () {

    }

    public static IEnumerator TextAnimator (float delay, Text textBox, string message) {
        textAnimatorRunning = true;
        textBox.text = "";
        for (int i = 0; i < message.Length; i++) {
            yield return new WaitForSeconds(delay);
            textBox.text += message[i];
        }
        textAnimatorRunning = false;
    }
}
