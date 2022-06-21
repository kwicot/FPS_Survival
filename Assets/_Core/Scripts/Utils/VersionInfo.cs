using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class VersionInfo : UnityEngine.MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Start()
        {
            text.text = Application.version;
        }
    }
}