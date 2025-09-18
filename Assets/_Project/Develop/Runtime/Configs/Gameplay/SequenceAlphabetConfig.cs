using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/SequenceAlphabetConfig", fileName = "SequenceAlphabetConfig")]
    public class SequenceAlphabetConfig : ScriptableObject
    {
        [field: SerializeField] public Sequences SequenceType {  get; private set; }
        [field: SerializeField] public string Symbols { get; private set; }
    }
}