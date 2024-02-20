using System;

class Program
{
    static void Main()
    {
        bool repeat = true;
        string bishopPos = string.Empty;
        string piecePos = string.Empty;
        string bishopColor = string.Empty;

        while (repeat)
        {
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

            switch (choice)
            {
                case 1:
                    SetupBoard(out bishopPos, out piecePos, out bishopColor);
                    break;
                case 2:
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
                    repeat = false;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void SetupBoard(out string bishopPos, out string piecePos, out string bishopColor)
    {
        char[,] board = new char[8, 8];
        InitializeBoard(board);

        Console.WriteLine("Введите координаты слона, фигуры и цвет слона (в формате x1y1 x2y2 цвет):");
        string input = Console.ReadLine();

        string[] coordinates = input.Split(' ');

        if (coordinates.Length != 3 || coordinates[0] == coordinates[1] || !ValidateCoordinates(coordinates[0]) || !ValidateCoordinates(coordinates[1]))
        {
            Console.WriteLine("Введены некорректные координаты или цвет");
            bishopPos = string.Empty;
            piecePos = string.Empty;
            bishopColor = string.Empty;
            return;
        }

        bishopPos = coordinates[0];
        piecePos = coordinates[1];
        bishopColor = coordinates[2];

        PlacePieces(board, bishopPos, piecePos, bishopColor);

        DrawBoard(board);
    }

    static void CheckCapture(string bishopPos, string piecePos, string bishopColor)
    {
        Console.WriteLine("Операция  2: Определение, бьет ли слон фигуру");
        Console.WriteLine();

        int bishopX = bishopPos[0] - 'a';
        int bishopY = bishopPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        if (CanBishopCapture(bishopX, bishopY, pieceX, pieceY, bishopColor))
        {
            Console.WriteLine("Слон сможет побить фигуру за  1 ход");
        }
        else
        {
            Console.WriteLine("Слон не может побить фигуру");
        }
    }

    static void InitializeBoard(char[,] board)
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                board[row, col] = '-';
            }
        }
    }

    static void PlacePieces(char[,] board, string bishopPos, string piecePos, string bishopColor)
    {
        int bishopX = bishopPos[0] - 'a';
        int bishopY = bishopPos[1] - '1';

        int pieceX = piecePos[0] - 'a';
        int pieceY = piecePos[1] - '1';

        MoveBishop(board, bishopX, bishopY, bishopColor);
        PlacePiece(board, pieceX, pieceY, 'F');
    }

    static void MoveBishop(char[,] board, int x, int y, string color)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = color == "white" ? 'B' : 'b'; // Размещаем слона на выбранных координатах
        }
    }

    static void PlacePiece(char[,] board, int x, int y, char piece)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[y, x] = piece;
        }
    }

    static void DrawBoard(char[,] board)
    {
        Console.WriteLine("   a b c d e f g h");

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

    static bool ValidateCoordinates(string coordinate)
    {
        if (coordinate.Length != 2)
        {
            return false;
        }

        char file = coordinate[0];
        char rank = coordinate[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
        {
            return false;
        }

        return true;
    }

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
