using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace snake
{
    internal class GameManager
    {
        static string scoreFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scores.txt");

        public static void StartGame()
        {
            Lives lives = new Lives();

            // Проверяем, задано ли имя игрока
            if (string.IsNullOrWhiteSpace(PlayerSession.PlayerName))
            {
                Console.WriteLine("Please enter your name first!");
                Thread.Sleep(2000);
                Menu.ShowMainMenu();
                return;
            }

            int score = 0;
            int gameSpeed = 100;
            Level level = new Level(score);
            level.ApplySettingsToGame(ref gameSpeed);
            int currentLevel = level.LevelNumber;

            Console.CursorVisible = false;
            Console.SetWindowSize(120, 30);
            Console.SetBufferSize(120, 30);

            int mapWidth = Console.WindowWidth;
            int mapHeight = Console.WindowHeight;

            Console.Clear();
            Walls walls = new Walls(mapWidth, mapHeight);
            walls.Draw();

            int safeMargin = 10;
            Point startPoint = new Point(safeMargin, mapHeight / 2, '○');


            Snake snake = new Snake(startPoint, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(mapWidth, mapHeight);
            FoodCreator.Food food = foodCreator.CreateFood(snake.pList, walls.GetAllPoints());
            food.Draw();

            ShowScore(score, currentLevel, lives, gameSpeed);
            lives.ShowLives();

            bool gameRunning = true;


            while (gameRunning)
            {
                Point nextHead = snake.GetNextPoint();
                char collisionChar = walls.CheckCollision(nextHead);
                bool selfCollision = snake.pList.Skip(1).Any(p => p.IsHit(nextHead)); 

                if (collisionChar != '\0' || selfCollision)
                {
                    switch (collisionChar)
                    {
                        case '☠': gameRunning = false; break; // сразу выход из игры
                        case '⛓': // одна жизнь минус
                            lives.LoseLife();
                            if (!lives.IsAlive) gameRunning = false;
                            break;
                        case '⚫': // минус 100 очков + ускорение
                            score = Math.Max(0, score - 100);
                            gameSpeed = Math.Max(50, gameSpeed - 100);
                            break;
                        case '☢': // минус 50 очков + уменьшение скорости
                            score = Math.Max(0, score - 50);
                            gameSpeed += 20;
                            break;
                        default:
                            if (selfCollision || collisionChar == '║' || collisionChar == '═')
                                gameRunning = false;
                            break;
                    }
                }

                if (!gameRunning) break;

                bool ate = snake.Eat(food.Position);

                snake.Move(ate); // если поел, не удаляем хвост

                if (ate)
                {
                    switch (food.Type)
                    {
                        case FoodCreator.FoodType.Regular:
                            score += 10;
                            break;
                        case FoodCreator.FoodType.Bonus:
                            score += 25;
                            break;
                        case FoodCreator.FoodType.Money:
                            score += 50;
                            break;
                        case FoodCreator.FoodType.Health:
                            if (lives.CurrentLives < 3)
                            {
                                lives.AddLife();
                            }
                            else
                            {
                                score += 5;
                            }
                            break;
                        case FoodCreator.FoodType.Sun:
                            score += 100;
                            gameSpeed = Math.Max(50, gameSpeed - 10);
                            break;
                        case FoodCreator.FoodType.Flower:
                            score += 10;
                            break;
                        case FoodCreator.FoodType.Magic:
                            score += 10;
                            break;
                    }

                    Level newLevel = new Level(score);
                    if (newLevel.LevelNumber > currentLevel)
                    {
                        currentLevel = newLevel.LevelNumber;
                        newLevel.ApplySettingsToGame(ref gameSpeed);
                        walls.AddObstacles(newLevel.Obstacles, mapWidth, mapHeight);
                    }

                    food = foodCreator.CreateFood(snake.pList, walls.GetAllPoints());
                }



                food.Draw();
                ShowScore(score, currentLevel, lives, gameSpeed);
                lives.ShowLives();

                Thread.Sleep(gameSpeed);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    snake.HandleKey(key.Key);
                }
            }

            WriteGameOver();
            SaveScore(PlayerSession.PlayerName, score);
            Console.ReadKey();
            Menu.ShowMainMenu();
        }

        static void SaveScore(string name, int score)
        {
            try
            {
                // Проверяем и создаём файл, если его нет
                if (!File.Exists(scoreFile))
                {
                    File.Create(scoreFile).Close(); // Создаём и сразу закрываем
                }

                // Проверяем имя игрока
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Unknown";
                }

                // Формируем строку для записи
                string line = $"{name}:{score}";

                // Записываем в файл
                File.AppendAllText(scoreFile, line + Environment.NewLine);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving score: {ex.Message}");
            }
        }

        static void ShowScore(int score, int currentLevel, Lives lives, int gameSpeed)
        {
            // Очищаем строку статуса
            Console.SetCursorPosition(0, 1);
            Console.Write(new string(' ', Console.WindowWidth));

            // Формируем строку статуса
            string status = $"Player: {PlayerSession.PlayerName} | Score: {score} | Level: {currentLevel} | Speed: {gameSpeed}ms"; 

            // Обрезаем строку, если она слишком длинная
            if (status.Length > Console.WindowWidth)
            {
                status = status.Substring(0, Console.WindowWidth);
            }

            // Выводим по центру верхней строки
            int x = Math.Max(0, (Console.WindowWidth - status.Length) / 2);
            Console.SetCursorPosition(x, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(status);
        }

        static int GetHighScore()
        {
            if (!File.Exists(scoreFile)) return 0;
            return File.ReadAllLines(scoreFile)
                .Select(line => int.Parse(line.Split(':')[1]))
                .Max();
        }

        static int WriteGameOver()
        {
            int xOffset = Console.WindowWidth / 2 - 15;
            int yOffset = Console.WindowHeight / 2 - 3;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteText("============================", xOffset, yOffset++);
            WriteText("    G A M E    O V E R", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("    Designed by A. K.", xOffset + 2, yOffset++);
            WriteText("============================", xOffset, yOffset++);

            return yOffset;
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}