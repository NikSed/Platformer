using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        QualitySettings.vSyncCount = 0;
    }

}
