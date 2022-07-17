using System;
using System.Linq;
using BaiyiShowcase.Animals;
using BaiyiShowcase.GameDesign.Terrains;
using BaiyiUtilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BaiyiShowcase.GameDesign
{
    [Serializable]
    public class Animals_Design
    {
        [Range(0f, 0.1f)]
        public float density;

        public TileSO[] tilesCantGenerateAnimals;

        [SerializeField] private string idle = "Idle";
        [SerializeField] private string walk = "Walk";
        [SerializeField] private string attack = "Attack";
        [SerializeField] private string death = "Death";

        public int IdleCode => Animator.StringToHash(idle);
        public int WalkCode => Animator.StringToHash(walk);
        public int AttackCode => Animator.StringToHash(attack);
        public int DeathCode => Animator.StringToHash(death);

        [SerializeField] [FolderPath] [InlineButton("RefreshAnimalSOs", "Refresh")]
        private string animalSOsPath;

        private void RefreshAnimalSOs()
        {
            animalSOs = Assistant.GetAssetsByFolderPath<AnimalSO>(animalSOsPath).ToArray();
        }

        [Space(30f)]
        [InlineEditor]
        [ListDrawerSettings(ShowIndexLabels = true, Expanded = true, NumberOfItemsPerPage = 10)]
        public AnimalSO[] animalSOs;
    }
}