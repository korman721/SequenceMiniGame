using Assets._Project.Develop.Runtime.Gameplay.Utilites;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructer
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(Sequences sequenceType)
        {
            SequenceType = sequenceType;
        }

        public Sequences SequenceType {  get; private set; }
    }
}
