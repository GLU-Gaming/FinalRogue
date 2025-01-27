using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Practise : MonoBehaviour
{
    public delegate void GoedeNaamEvent();
    public event GoedeNaamEvent DoEvent;
    [Header("Boom")]
    public UnityEvent DoUnityEvent;

    // Start is called before the first frame update
    void Start()
    {
        // c# event
        DoEvent += () => { };
        DoEvent += DoTestFunction;
        DoEvent.Invoke();

        // unity event
        DoUnityEvent.AddListener(DoTestFunction);
        DoUnityEvent.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DoTestFunction()
    {

    }
}
