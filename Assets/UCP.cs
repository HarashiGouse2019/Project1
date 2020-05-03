using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;
public class UCP : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField CommandPrompt;
    string command = "set";
    string subCommand = "flag";
    string[] predetermindFlags = { "flight" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
