using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field: SerializeField] public Transform HUDLayer {  get; private set; }
        [field: SerializeField] public Transform VFXOverPopusLayer{  get; private set; }
        [field: SerializeField] public Transform PopupsLayer {  get; private set; }
        [field: SerializeField] public Transform VFXUnderPopusLayer {  get; private set; }
    }
}