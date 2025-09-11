using Assets._Project.Develop.Runtime.Gameplay.Infrastructer;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Utilites
{
    public class SequenceGenerator
    {
        private const int MaxLenght = 10;
        private const int MinLenght = 3;

        public GameplayInputArgs InputGameplayArgs {  get; private set; }

        public void Initialize(GameplayInputArgs inputGameplayArgs)
        {
            InputGameplayArgs = inputGameplayArgs;
        }

        public string GenerateSequence()
        {
            string sequence = "";
            int randomLenght = Random.Range(MinLenght, MaxLenght);

            while (randomLenght > 0)
            {
                sequence += InputGameplayArgs.Symbols[Random.Range(0, InputGameplayArgs.Symbols.Length)];
                randomLenght--;
            }

            return sequence;
        }
    }
}
