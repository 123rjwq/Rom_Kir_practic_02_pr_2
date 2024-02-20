using System;

class Program
{
    static void Main()
    {
        // Флаг для управления повторением выбора операций
        bool repeat = true;
        string bishopPos = string.Empty;
        string piecePos = string.Empty;
        string bishopColor = string.Empty; // Переменная для хранения цвета слона

        // Основной цикл программы, продолжается до тех пор, пока пользователь не выберет выход
        while (repeat)
        {
            // Вывод меню выбора действий
            Console.WriteLine("Выберите одно из действий:");
            Console.WriteLine("1. Разместить фигуры на шахматной доске");
            Console.WriteLine("2. Определить, бьет ли слон фигуру");
            Console.WriteLine("3. Выйти из программы");
            Console.Write("Ваш выбор: ");

            // Проверка на ввод числа
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                Console.WriteLine();
                continue;
            }

            // Выполнение выбранного действия
            switch (choice)
            {
                case 1:
                    // Размещение фигур на шахматной доске
                    SetupBoard(out bishopPos, out piecePos, out bishopColor);
                    break;
                case 2:
                    // Проверка возможности побить фигуру слоном
                    if (bishopPos != string.Empty && piecePos != string.Empty)
                    {
                        CheckCapture(bishopPos, piecePos, bishopColor);
                    }
                    else
                    {
                        Console.WriteLine("Фигуры на шахматной доске не размещены");
                    }
                    break;
                case 3:
                    // Завершение программы
                    repeat = false;
                    break;
                default:
                    // Обработка неверного выбора
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }

            // Переход на новую строку для удобства чтения
            Console.WriteLine();
        }
    }

    // Метод для размещения фигур на доске
    static void SetupBoard(out string bishopPos, out string piecePos, out string bishopColor)
    {
        // Инициализация доски
        char[,] board = new char[8, 8];
        InitializeBoard(board);

        // Запрос координат для размещения слона и фигуры, а также цвета слона
        Console.WriteLine("Введите координаты слона, фигуры и цвет слона (в формате x1y1 x2y2 цвет):");
        string input = Console.ReadLine();

        // Разделение введённых координат
        string[] coordinates = input.Split(' ');

        // Проверка корректности введённых координат и цвета
        if (coordinates.Length != 3 || coordinates[0] == coordinates[1] || !ValidateCoordinates(coordinates[0]) || !ValidateCoordinates(coordinates[1]))
        {
            Console.WriteLine("Введены некорректные координаты или цвет");
            bishopPos = string.Empty;
            piecePos = string.Empty;
            bishopColor = string.Empty;
            return;
        }

        // Размещение фигур на доске
        bishopPos = coordinates[0];
        piecePos = coordinates[1];
        bishopColor = coordinates[2];

        PlacePieces(board, bishopPos, piecePos, bishopColor);

        // Вывод доски
        DrawBoard(board);
    }

    // Метод для проверки, может ли слон побить фигуру
    static void CheckCapture(string bishopPos, string piecePos, string bishopColor)
    {
        // Вывод информации о проверке
        Console.WriteLine("Операция   2: Определение, бьет ли слон фигуру");
        Console.WriteLine();

        // Вычисление координат слона и фигуры
        int bishopX = bishopPos[0] - 'a';
        int bishopY = bishopPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Проверка, может ли слон побить фигуру
        if (CanBishopCapture(bishopX, bishopY, pieceX, pieceY, bishopColor))
        {
            Console.WriteLine("Слон сможет побить фигуру за   1 ход");
        }
        else
        {
            Console.WriteLine("Слон не может побить фигуру");
        }
    }

    // Метод для инициализации доски
    static void InitializeBoard(char[,] board)
    {
        // Заполнение доски пустыми клетками
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                board[row, col] = '-';
            }
        }
    }

    // Метод для размещения фигур на доске
    static void PlacePieces(char[,] board, string bishopPos, string piecePos, string bishopColor)
    {
        // Вычисление координат слона и фигуры
        int bishopX = bishopPos[0] - 'a';
        int bishopY = bishopPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        // Размещение слона и фигуры на доске
        MoveBishop(board, bishopX, bishopY, bishopColor);
        PlacePiece(board, pieceX, pieceY, 'F');
    }

    // Метод для размещения слона на доске
    static void MoveBishop(char[,] board, int x, int y, string color)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = color == "white" ? 'B' : 'b'; // Размещаем слона на выбранных координатах
        }
    }

    // Метод для размещения фигуры на доске
    static void PlacePiece(char[,] board, int x, int y, char piece)
    {
        // Проверка корректности координат
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = piece; // Размещаем фигуру на выбранных координатах
        }
    }

    // Метод для вывода доски
    static void DrawBoard(char[,] board)
    {
        // Вывод заголовка доски
        Console.WriteLine("   a b c d e f g h");

        // Вывод доски в обратном порядке (с верхней стороны)
        for (int row = 7; row >= 0; row--)
        {
            Console.Write($"{row + 1} ");

            for (int col = 0; col < 8; col++)
            {
                Console.Write(board[row, col] + " ");
            }

            Console.WriteLine();
        }
    }

    // Метод для проверки корректности введённых координат
    static bool ValidateCoordinates(string coordinate)
    {
        // Проверка длины строки координат
        if (coordinate.Length != 2)
        {
            return false;
        }

        // Проверка диапазона символов координат
        char file = coordinate[0];
        char rank = coordinate[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
        {
            return false;
        }

        return true;
    }

    // Метод для определения возможности побить фигуру слоном
    static bool CanBishopCapture(int bishopX, int bishopY, int pieceX, int pieceY, string bishopColor)
    {
        // Проверка, что слон и фигура находятся на одной диагонали
        if (Math.Abs(bishopX - pieceX) == Math.Abs(bishopY - pieceY))
        {
            // Проверка, что слон и фигура находятся на клетках своего цвета
            if ((bishopColor == "white" && bishopX % 2 == pieceX % 2) || (bishopColor == "black" && (bishopX + 1) % 2 == pieceX % 2))
            {
                return true;
            }
        }

        return false;
    }
}
