using Assets._Project.Develop.Runtime.Utilities.AssetsManagment;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesLoader;

        private readonly Dictionary<string, string> _viewIDPath = new Dictionary<string, string>()
        {
            { ViewID.IconTextView, "UI/CoreViews/CurrencyView"},
            { ViewID.MainMenuScreenView, "UI/Meta/MainMenuScreenView"},
            { ViewID.TextView, "UI/CoreViews/TextView"},
            { ViewID.GameplayScreenView, "UI/Gameplay/GameplayScreenView" }
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;
        }

        public TView CreateView<TView>(string ID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDPath.ContainsKey(ID) == false)
                    throw new InvalidOperationException($"{nameof(ID)} not exisists");

            GameObject viewPrefab = _resourcesLoader.Load<GameObject>(_viewIDPath[ID]);
            GameObject viewOnScene = Object.Instantiate(viewPrefab, parent);

            TView view = viewOnScene.GetComponent<TView>();

            if (view == null)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view);
        }
    }
}
