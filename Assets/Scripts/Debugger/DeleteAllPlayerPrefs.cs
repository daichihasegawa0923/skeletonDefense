using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diamond.SkeletonDefense.Debugger
{ 
    public class DeleteAllPlayerPrefs : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}