﻿using UnityEngine;

namespace TaijiRandomizer
{
    internal class PanelList
    {
        private static Sprite? _graveyardMetapuzzleZero = null;
        private static Sprite? _graveyardMetapuzzleOne = null;

        public static void Initialize()
        {
            GameObject graveZeroStone = GameObject.Find("AreaRoot_Graveyard/GraphicsRoot/Graveyard_GraveyardRoot/Graveyard_Nongameplay_GravestoneRoot/Graveyard_Nongameplay_Gravestone (23)");
            if (graveZeroStone != null)
            {
                _graveyardMetapuzzleZero = graveZeroStone.GetComponent<SpriteRenderer>().sprite;
            }

            GameObject graveOneStone = GameObject.Find("AreaRoot_Graveyard/GraphicsRoot/Graveyard_GraveyardRoot/Graveyard_Nongameplay_GravestoneRoot/Graveyard_Nongameplay_Gravestone (29)");
            if (graveOneStone != null)
            {
                _graveyardMetapuzzleOne = graveOneStone.GetComponent<SpriteRenderer>().sprite;
            }

            TutorialGenerator.Initialize();
            GraveyardGenerator.Initialize();
        }

        public static void Generate()
        {
            Generator generator = new(26);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Teal, 2);
            generator.Generate();

            generator = new(255);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Teal, 2);
            generator.SetLocks(3);
            generator.Generate();

            generator = new(107);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Teal, 4);
            generator.SetLocks(2);
            generator.Generate();

            generator = new(81);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Teal, 4);
            generator.SetLocks(2);
            generator.Generate();

            generator = new(59);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 6);
            generator.SetLocks(2);
            generator.Generate();

            generator = new(62);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 6);
            generator.Generate();

            generator = new(253);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 6);
            generator.SetLocks(3);
            generator.Generate();

            generator = new(347);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 8);
            generator.Generate();

            generator = new(110);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Black, 10);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.White, 4);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 2);
            generator.SetLocks(7);
            generator.Generate();




            generator = new(468);
            generator.Add(Puzzle.Symbol.Bar, Puzzle.Color.Black, 3);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Black, 3);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.White, 2);
            generator.SetWildcardFlowers(1);
            generator.SetLocks(4);
            generator.Generate();


            // These two are broken.
            generator = new(19);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 16);
            generator.SetLocks(4);
            generator.Generate();

            generator = new(128);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.Gold, 16);
            generator.SetLocks(4);
            generator.Generate();



            generator = new(61);
            generator.Add(Puzzle.Symbol.Diamond, Puzzle.Color.White, 4);
            generator.SetWildcardFlowers(2);
            generator.Generate();




            GenerateTutorial();

            GenerateGraveyard();

            GenerateGraveyardMetapuzzle();
        }

        private static void GenerateTutorial()
        {
            TutorialGenerator tutorialGenerator;

            // First puzzle. The alternate solution remains the same. Ensure that the normal solution is not the alt.
            tutorialGenerator = new(408, 0, 0, "2ndIsland/GraphicsRoot/StartingArea_HintPillarBase", (float)-0.5, 1);
            tutorialGenerator.ForceCell(0, Randomizer.Instance?.Rng?.Next(2) ?? 0, false);
            tutorialGenerator.Generate();

            // Second floating island.
            tutorialGenerator = new(415, 1, 1, "3rdIsland/GraphicsRoot/StartingArea_HintPillarBase (1)", (float)-0.5, 1);
            tutorialGenerator.Generate();

            // Third floating island.
            tutorialGenerator = new(419, 2, 3, "4thIsland/NewGraphicsRoot/StartingArea_HintPillarBase (2)", (float)-0.5, 1);
            tutorialGenerator.Generate();

            // Fourth floating island.
            tutorialGenerator = new(428, 3, 4, "5thIsland/NewGraphicsRoot/StartingArea_HintPillarBase (3)", (float)-0.5, 1);
            tutorialGenerator.Generate();

            // First mainland.
            tutorialGenerator = new(380, 4, 5, "Mainland/GraphicsRoot/StartingArea_HintPillarBase (4)", (float)-1.5, 1);
            tutorialGenerator.Generate();

            // Second mainland.
            tutorialGenerator = new(381, 5, 6, "Mainland/GraphicsRoot/StartingArea_HintPillarBase (11)", (float)-1.5, 1);
            tutorialGenerator.Generate();

            // Optional mainland.
            tutorialGenerator = new(397, 8, 9, "Mainland/GraphicsRoot/StartingArea_HintPillarBase (7)", (float)-1, 1);
            tutorialGenerator.Generate();

            // The final four.
            uint[] tutorialEndIds = { 163, 436, 442, 449 };
            List<uint> orderedIds = tutorialEndIds.OrderBy(_ => Randomizer.Instance?.Rng?.Next()).ToList();

            List<Puzzle.Coord> coords = new();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    coords.Add(new(x, y));
                }
            }

            Puzzle.Coord coord1 = coords[Randomizer.Instance?.Rng?.Next(coords.Count) ?? 0];
            coords.Remove(coord1);
            Puzzle.Coord coord2 = coords[Randomizer.Instance?.Rng?.Next(coords.Count) ?? 0];
            coords.Remove(coord2);
            Puzzle.Coord coord3 = coords[Randomizer.Instance?.Rng?.Next(coords.Count) ?? 0];
            coords.Remove(coord3);
            Puzzle.Coord coord4 = coords[Randomizer.Instance?.Rng?.Next(coords.Count) ?? 0];

            List<TutorialGenerator> generators = new() {
                new(orderedIds[0], 3, 5, "StartingArea_AboveLowerWorldPath_FadeGroup/StartingArea_HintPillarBase (9)", (float)-1, 1),
                new(orderedIds[1], 3, 5, "StartingArea_AboveLowerWorldPath_FadeGroup/StartingArea_HintPillarBase (7)", (float)-1, 1),
                new(orderedIds[2], 3, 5, "StartingArea_AboveLowerWorldPath_FadeGroup/StartingArea_HintPillarBase (8)", (float)-1, 1),
                new(orderedIds[3], 3, 5, "StartingArea_AboveLowerWorldPath_FadeGroup/StartingArea_HintPillarBase (10)", (float)-1, 1),
            };

            List<TutorialGenerator> orderedGenerators = generators.OrderBy(_ => Randomizer.Instance?.Rng?.Next()).ToList();

            bool cell1 = Randomizer.Instance?.Rng?.Next(2) == 0;
            bool cell2 = Randomizer.Instance?.Rng?.Next(2) == 0;
            bool cell3 = Randomizer.Instance?.Rng?.Next(2) == 0;
            bool cell4 = Randomizer.Instance?.Rng?.Next(2) == 0;

            orderedGenerators[0].LockCell(coord1.x, coord1.y, cell1);
            orderedGenerators[0].LockCell(coord2.x, coord2.y, !cell2);
            orderedGenerators[0].Generate();

            orderedGenerators[1].LockCell(coord2.x, coord2.y, cell2);
            orderedGenerators[1].LockCell(coord3.x, coord3.y, !cell3);
            orderedGenerators[1].Generate();

            orderedGenerators[2].LockCell(coord3.x, coord3.y, cell3);
            orderedGenerators[2].LockCell(coord4.x, coord4.y, !cell4);
            orderedGenerators[2].Generate();

            orderedGenerators[3].LockCell(coord4.x, coord4.y, cell4);
            orderedGenerators[3].LockCell(coord1.x, coord1.y, !cell1);
            orderedGenerators[3].Generate();
        }

        private static void GenerateGraveyard()
        {
            GraveyardGenerator generator;

            // First exterior.
            generator = new(158);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones");
            generator.Generate();

            // Second exterior.
            generator = new(349);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (4)");
            generator.Generate();

            // Third exterior.
            generator = new(249);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (5)/Graveyard_Gravestone_Head");
            generator.Generate();

            // Fourth exterior.
            generator = new(13);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (6)");
            generator.Generate();

            // Fifth exterior.
            generator = new(7);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (7)");
            generator.Generate();

            // Sixth exterior.
            generator = new(156);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (9)");
            generator.Generate();

            // Seventh exterior.
            generator = new(201);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (10)");
            generator.Generate();

            // Eighth exterior.
            generator = new(28);
            generator.LoadExistingStrings("Graveyard_GraveyardRoot/Graveyard_Gravestones (8)");
            generator.Generate();

            // Final exterior.
            generator = new(331);
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot/Graveyard_TombFadeGroup0",
                new Color(0.6274F, 0.6196F, 0.5215F, 1),
                new Vector3(4.1609F, 288.0486F, 5),
                new Vector2(3.5F, 0.6F));
            generator.AddString(new(-1.45F, 0, 0));
            generator.AddString(new(-1.45F, 0, 0));
            generator.Generate();

            // First interior.
            generator = new(520);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(-2.8F, 296.2F, 5),
                new Vector3(6.4F, 0.55F, 1));
            generator.AddString(new(-2.95F, 0, 0), true);
            generator.AddString(new(0.55F, 0, 0));
            generator.AddDot(Vector3.zero);
            generator.Generate();

            // Second interior.
            generator = new(519);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(-11.2F, 296.2F, 5),
                new Vector3(6.4F, 0.55F, 1));
            generator.AddString(new(-2.95F, 0, 0), true);
            generator.AddString(new(0.55F, 0, 0));
            generator.AddDot(Vector3.zero);
            generator.Generate();

            // Third interior.
            generator = new(518);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(-6.9F, 290.3F, 5),
                new Vector3(3, 0.8F, 1));
            generator.AddString(new(-1.15F, 0.2F, 0));
            generator.AddString(new(-1.15F, -0.2F, 0), true);
            generator.Generate();

            // Fourth interior.
            generator = new(523);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(14.9F, 290.3F, 5),
                new Vector3(3, 0.8F, 1));
            generator.AddString(new(-1.15F, 0.05F, 0));
            generator.AddString(new(-1.15F, -0.05F, 0), true);
            generator.Generate();

            // Fifth interior.
            generator = new(521);
            generator.Inside = true;
            generator.Palindrome = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(19F, 296.2F, 5),
                new Vector3(3.5F, 0.6F, 1));
            generator.AddString(new(-1.35F, 0.05F, 0));
            generator.Generate();

            // Sixth interior.
            generator = new(524);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(10.9F, 296.2F, 5),
                new Vector3(6, 0.6F, 1));
            generator.AddString(new(-2.75F, 0.05F, 0));
            generator.AddString(new(-0.25F, 0.05F, 0), true);
            generator.Generate();

            // Flame puzzle.
            // TODO: This is too tough. Please nerf.
            generator = new(522);
            generator.Inside = true;
            generator.CreateBackground(
                "AreaRoot_Graveyard/GraphicsRoot/Graveyard_MountainPathRoot/Graveyard_TombRoot",
                new Color(0.6352F, 0.6274F, 0.5215F, 1),
                new Vector3(4, 304.5F, 5),
                new Vector3(3.5F, 0.55F, 1));
            generator.AddString(new(-1.45F, 0.05F, 0));
            generator.AddString(new(-1.45F, 0.05F, 0), true);
            generator.Generate();

            // Bonus entrance.
            generator = new(186);
            generator.LoadExistingStrings("Graveyard_BonusPuzzlesSectionRoot/Graveyard_Gravestones (13)");
            generator.Generate();

            // Bonus left.
            generator = new(187);
            generator.LoadExistingStrings("Graveyard_BonusPuzzlesSectionRoot/Graveyard_Gravestones (14)");
            generator.Generate();

            // Bonus middle.
            generator = new(69);
            generator.LoadExistingStrings("Graveyard_BonusPuzzlesSectionRoot/Graveyard_Gravestones (12)");
            generator.Generate();

            // Bonus right.
            generator = new(188);
            generator.LoadExistingStrings("Graveyard_BonusPuzzlesSectionRoot/Graveyard_Gravestones (15)");
            generator.Generate();
        }

        private static void GenerateGraveyardMetapuzzle()
        {
            if (_graveyardMetapuzzleZero == null || _graveyardMetapuzzleOne == null)
            {
                Randomizer.Instance?.LoggerInstance.Msg("Could not generate graveyard metapuzzle because grave sprites are not initialized.");
                return;
            }

            bool[,] binaryStrings = new bool[5, 8];

            // Generate the three full binary strings.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    binaryStrings[i, j] = ((Randomizer.Instance?.Rng?.Next(0, 2) ?? 0) == 0);
                }
            }

            // Two binary strings have to be palindromic, so let's generate those here.
            for (int i = 3; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bool value = ((Randomizer.Instance?.Rng?.Next(0, 2) ?? 0) == 0);
                    binaryStrings[i, j] = value;
                    binaryStrings[i, 7 - j] = value;
                }
            }

            // Write the puzzle.
            Puzzle puzzle = new();
            puzzle.Load(109);
            puzzle.Clear();

            for (int j = 0; j < 8; j++)
            {
                int total = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (binaryStrings[i, j])
                    {
                        total++;
                    }
                }

                puzzle.SetSolution(j, 0, total % 2 == 1);
            }

            puzzle.Save(109);
            puzzle.WriteSolution(109);

            // Replace the gravestone sprites.
            int[,] graveIds = {
                { 40, 39, 35, 38, 36, 33, 37, 34 },
                { 30, 29, 28, 27, 26, 25, 24, 23 },
                { 16, 31, 18, 32, 19, 20, 21, 22 },
                { 46, 42, 41, 45, 43, 47, 48, 57 },
                { 54, 52, 50, 53, 51, 55, 49, 56 }
            };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string path = $"AreaRoot_Graveyard/GraphicsRoot/Graveyard_GraveyardRoot/Graveyard_Nongameplay_GravestoneRoot/Graveyard_Nongameplay_Gravestone ({graveIds[i, j]})";
                    GameObject graveObject = GameObject.Find(path);
                    if (graveObject == null)
                    {
                        Randomizer.Instance?.LoggerInstance.Msg($"Could not find grave object {path}");
                        continue;
                    }

                    SpriteRenderer graveSprite = graveObject.GetComponent<SpriteRenderer>();

                    if (binaryStrings[i, j])
                    {
                        graveSprite.sprite = _graveyardMetapuzzleOne;
                    }
                    else
                    {
                        graveSprite.sprite = _graveyardMetapuzzleZero;
                    }
                }
            }
        }
    }
}
