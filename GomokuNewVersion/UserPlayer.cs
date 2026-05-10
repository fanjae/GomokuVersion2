namespace GomokuNewVersion
{
    internal class UserPlayer : IPlayer, ISystemMessageReceiver
    {
        private readonly BoardRenderer renderer;

        private Position cursor = new Position(0,0);
        private string systemMessage = "";

        public Stone Stone { get; }

        public UserPlayer(Stone stone, BoardRenderer renderer)
        {
            Stone = stone;
            this.renderer = renderer;
        }

        public Position SelectMove(Board board) // 플레이어의 이동 관련 처리
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                renderer.Render(board, cursor, Stone);

                Console.WriteLine($"{Stone} 플레이어의 턴입니다.");
                Console.WriteLine("방향키로 이동하고, Space를 눌러 돌을 배치하세요.");
                Console.Write(systemMessage);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    MoveCursor(board, -1, 0);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    MoveCursor(board, 1, 0);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    MoveCursor(board, 0, -1);
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    MoveCursor(board,0, 1);
                }
                else if (key == ConsoleKey.Spacebar)
                {
                    systemMessage = "";
                    return cursor;
                }
            }
        }
        private void MoveCursor(Board board, int dRow, int dCol) // 커서 위치 옮기기
        {
            Position next = new Position(cursor.Row + dRow, cursor.Col + dCol);

            if (board.IsInside(next)) // 유효한 범위만 이동
            {
                cursor = next;
            }
        }
        public void ReceiveSystemMessage(string message)
        {
            systemMessage = message;
        }
    }
}
