using Assets._Project.Develop.Runtime.Gameplay.Infrastructer;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using Random = UnityEngine.Random;

namespace Assets._Project.Develop.Runtime.Gameplay.Utilites
{
    public class SequenceGenerator
    {
        private const string Numbers = "0123456789";
        private const string Chars = "qwertyuiopasdfghjklzxcvbnm";

        private const int MaxLenght = 10;
        private const int MinLenght = 3;

        public GameplayInputArgs InputGameplayArgs {  get; private set; }

        public void Initialize(GameplayInputArgs inputGameplayArgs) => InputGameplayArgs = inputGameplayArgs;

        public string GetSequenceBy(Sequences sequencesType)
        {
            switch (sequencesType)
            {
                case Sequences.Numbers:
                    return GenerateSequence(Numbers);

                case Sequences.Alphabet:
                    return GenerateSequence(Chars);

                default:
                    throw new InvalidOperationException($"{sequencesType} not exist");
            }
        }

        private string GenerateSequence(string symbols)
        {
            string sequence = "";
            int randomLenght = Random.Range(MinLenght, MaxLenght);

            while (randomLenght > 0)
            {
                sequence += symbols[Random.Range(0, symbols.Length)];
                randomLenght--;
            }

            return sequence;
        }
    }
}
