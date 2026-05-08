namespace GomokuNewVersion
{
    internal class GameController
    {
        private readonly Board board = new Board();
        private readonly BoardRenderer renderer = new BoardRenderer();
        private readonly GomokuRule rule = new GomokuRule();

        private int cursorX = 0;
        private int cursorY = 0;
        private Stone turn = Stone.Black;
        private string systemMessage = "";

        public void Run()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                renderer.Render(board, cursorX, cursorY, turn);

                Console.WriteLine($"Player {(turn == Stone.Black ? 1 : 2)}P의 턴입니다.");
                Console.WriteLine("방향키로 이동하고, Space를 눌러 돌을 배치하세요.");
                Console.Write(systemMessage);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    MoveCursor(-1, 0);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    MoveCursor(1, 0);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    MoveCursor(0, -1);
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    MoveCursor(0, 1);
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    if (!board.IsEmpty(cursorX, cursorY)) // 돌이 배치되었는지 체크
                    {
                        systemMessage = "이미 돌이 배치되어 있습니다. 다른 곳에 배치해주세요.\n";
                        continue;
                    }
                    if (rule.IsDoubleThree(board, cursorX, cursorY, turn)) // 33 금수 체크
                    {
                        systemMessage = "33 금수입니다. 다른 곳에 배치해주세요.\n";
                        continue;
                    }

                    board.PlaceStone(cursorX, cursorY, turn);

                    if (rule.IsWin(board, cursorX, cursorY, turn)) // 승리 체크
                    {
                        Console.Clear();
                        renderer.Render(board, cursorX, cursorY, turn, false);
                        Console.WriteLine($"Player {(turn == Stone.Black ? 1 : 2)}P의 승리로 게임을 종료합니다.");
                        Console.WriteLine("아무 키나 입력하면 메뉴로 돌아갑니다.");
                        Console.ReadKey(true);
                        break;
                    }

                    ChangeTurn(); // 턴 교체
                    systemMessage = "";
                }
            }
        }
        private void MoveCursor(int dx, int dy) // 커서 위치 옮기기
        {
            int nextX = cursorX + dx;
            int nextY = cursorY + dy;

            if (board.IsInside(nextX, nextY)) // 유효한 범위만 이동
            {
                cursorX = nextX;
                cursorY = nextY;
            }

            systemMessage = "";
        }

        private void ChangeTurn()
        {
            turn = turn == Stone.Black ? Stone.White : Stone.Black;
        }
    }
}
