using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.LoadingScreen
{
    public class StandartLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public bool IsShown => gameObject.activeSelf;

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}
