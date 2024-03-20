﻿using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

namespace TaijiRandomizer
{
    public class Randomizer : MelonMod
    {
        public const string kVersion = "0.1.0";

        private static Randomizer? _instance = null;

        public static Randomizer? Instance
        {
            get { return _instance; }
        }

        private Random? _rng = null;

        public Random? Rng
        {
            get { return _rng; }
        }

        private bool _shouldRandomize = false;

        public bool ShouldRandomize
        {
            get { return _shouldRandomize; }

            set { _shouldRandomize = value; }
        }

        private GameObject? _templateWhiteBlock = null;
        private GameObject? _templateBlackBlock = null;

        public GameObject? TemplateWhiteBlock { get { return _templateWhiteBlock; } }

        public GameObject? TemplateBlackBlock { get { return _templateBlackBlock; } }

        internal delegate void PuzzlePanelInitializer(PuzzlePanel panel);

        private Dictionary<uint, PuzzlePanelInitializer> _puzzlePanelInitializers = new();

        internal void SetPuzzlePanelInitializer(uint id, PuzzlePanelInitializer initializer)
        {
            _puzzlePanelInitializers[id] = initializer;
        }

        private Dictionary<string, GameObject> _gameObjectCache = new();

        public GameObject LookupGameObject(string path)
        {
            if (!_gameObjectCache.ContainsKey(path))
            {
                _gameObjectCache[path] = GameObject.Find(path);
            }

            return _gameObjectCache[path];
        }

        [HarmonyPatch(typeof(PuzzlePanel), nameof(PuzzlePanel.Update))]
        static class UpdatePanelPatch
        {
            public static void Postfix(PuzzlePanel __instance)
            {
                // Here we can run a handler right after a puzzle has been properly initialized.
                if (__instance.isInitialized && _instance != null && _instance._puzzlePanelInitializers.ContainsKey(__instance.id))
                {
                    _instance._puzzlePanelInitializers[__instance.id](__instance);
                    _instance._puzzlePanelInitializers.Remove(__instance.id);
                }
            }
        }

        [HarmonyPatch(typeof(PuzzlePanelStartTile), "ToggleTile")]
        static class ToggleTilePatch
        {
            public static void Prefix(PuzzlePanelStartTile __instance)
            {
                if (Randomizer.Instance != null)
                {
                    Randomizer.Instance?.LoggerInstance.Msg($"Panel {__instance.panelToControl.id} is {__instance.panelToControl.width}x{__instance.panelToControl.height}");
                }
            }
        }

        [HarmonyPatch(typeof(PuzzlePanel), "stepOn")]
        static class SolvePuzzlePatch
        {
            private static HashSet<uint> _checked = new();

            public static void Prefix(PuzzlePanel __instance)
            {
                if (Randomizer.Instance != null && !_checked.Contains(__instance.id))
                {
                    Randomizer.Instance?.LoggerInstance.Msg($"Panel {__instance.id} is {__instance.width}x{__instance.height}");
                    _checked.Add(__instance.id);
                }
            }
        }

        public override void OnInitializeMelon()
        {
            _instance = this;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (!_shouldRandomize)
            {
                return;
            }

            _gameObjectCache.Clear();

            SaveSystem.GenerateInstanceMap();

            GameObject.Find("AreaRoot_BonusPuzzles/GraphicsRoot/BonusArea_FadeGroup/PuzzlePanel (234)").active = false;

            GameObject basedOn = GameObject.Find("AreaRoot_BonusPuzzles/GraphicsRoot/BonusArea_FadeGroup/PuzzlePanel (232)");
            Vector3 v3 = basedOn.transform.position;
            v3.y = 29.53F;

            Puzzle.Instantiate(3000, PuzzlePanel.PanelTypes.Snake, v3, 3, 4);

            // Create template blocks for the tutorial-style puzzles.
            _templateWhiteBlock = GameObject.Instantiate(GameObject.Find("StartingArea_HintPillarBase (7)/StartingArea_HintBlocks_0 (20)"));
            _templateWhiteBlock.active = false;

            _templateBlackBlock = GameObject.Instantiate(GameObject.Find("StartingArea_HintPillarBase (7)/StartingArea_HintBlocks_0 (25)"));
            _templateBlackBlock.active = false;
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (!_shouldRandomize)
            {
                return;
            }

            SaveSystem.GenerateInstanceMap();

            GeneratePuzzles();
        }

        public void GeneratePuzzles()
        {







            LoggerInstance.Msg("Start generation...");

            int seed;

            if (Menu.SetSeed)
            {
                seed = Menu.SetSeedValue;
            }
            else
            {
                Random seedRng = new();
                seed = seedRng.Next(1, 10000000);
            }
            
            LoggerInstance.Msg($"Seed: {seed}");

            _rng = new Random(seed);






            Generator gen3000 = new(3000);
            gen3000.Add(Puzzle.Symbol.Diamond, Puzzle.Color.White, 4);
            gen3000.SetWildcardFlowers(4);
            gen3000.Generate();





            // Generate some puzzles.
            Puzzle hello = new();
            hello.Load(46);
            hello.SetSymbol(0, 0, Puzzle.Symbol.OnePetal, Puzzle.Color.Black);
            hello.SetSymbol(1, 0, Puzzle.Symbol.Diamond, Puzzle.Color.PetalPurple);
            hello.SetSymbol(3, 0, Puzzle.Symbol.OnePip, Puzzle.Color.Gray);
            hello.SetSymbol(1, 2, Puzzle.Symbol.Diamond, Puzzle.Color.Black);
            hello.SetSymbol(0, 3, Puzzle.Symbol.OneAntiPip, Puzzle.Color.Gray);
            hello.SetSymbol(3, 3, Puzzle.Symbol.Diamond, Puzzle.Color.Black);
            hello.Save(46);

            Generator gen97 = new(97);
            gen97.SetLocks(6);
            gen97.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Black, 3);
            gen97.Add(Puzzle.Symbol.Diamond, Puzzle.Color.White, 4);
            gen97.SetFlowers(2, 1);
            gen97.SetFlowers(4, 1);
            gen97.SetFlowers(0, 1);
            gen97.SetWildcardFlowers(1);
            //gen97.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 1);
            //gen97.Add(Puzzle.Symbol.Diamond, Puzzle.Color.PetalPurple, 1);
            gen97.Add(Puzzle.Symbol.Dice, Puzzle.Color.Black, 2);
            gen97.Generate();



            PanelList.Generate();

            // Scene currentScene = SceneManager.GetSceneByName("hi");

            LoggerInstance.Msg("Done!");
        }

        public override void OnDeinitializeMelon()
        {
            _instance = null;
        }
    }
}