namespace GomokuNewVersion
{
    internal class GameController
    {
        private readonly Board board = new Board();
        private readonly GomokuRule rule = new GomokuRule();
        private readonly BoardRenderer renderer;

        private readonly IPlayer blackPlayer;
        private readonly IPlayer whitePlayer;

        private Stone turn = Stone.Black;

        public GameController(IPlayer blackPlayer, IPlayer whitePlayer, BoardRenderer renderer) // 생성자를 통해 메뉴로부터 플레이어 정보 생성
        {
            this.blackPlayer = blackPlayer;
            this.whitePlayer = whitePlayer;
            this.renderer = renderer;
        }

        public void Run()
        {
            Console.Clear();
            Console.CursorVisible = false;

            while (true)
            {
                IPlayer currentPlayer = GetCurrentPlayer();

                Position move = currentPlayer.SelectMove(board);

                if (!TryApplyMove(move, currentPlayer.Stone, out string errorMessage))
                {
                    if(currentPlayer is ISystemMessageReceiver receiver)
                    {
                        receiver.ReceiveSystemMessage(errorMessage);
                    }
                    continue;
                }

                if (rule.IsWin(board, move, currentPlayer.Stone))
                {
                    Console.Clear();
                    renderer.Render(board, move, currentPlayer.Stone, false);
                    Console.WriteLine($"{currentPlayer.Stone} 플레이어의 승리로 게임을 종료합니다.");
                    Console.WriteLine("아무 키나 입력하면 메뉴로 돌아갑니다.");
                    Console.ReadKey(true);
                    break;
                }

                if (board.IsFull())
                {
                    Console.Clear();
                    renderer.Render(board, move, currentPlayer.Stone, false);
                    Console.WriteLine("무승부로 게임을 종료합니다.");
                    Console.WriteLine("아무 키나 입력하면 메뉴로 돌아갑니다.");
                    Console.ReadKey(true);
                    break;
                }

                ChangeTurn();
            }
        }

        private IPlayer GetCurrentPlayer()
        {
            return turn == Stone.Black ? blackPlayer : whitePlayer;
        }

        private void ChangeTurn()
        {
            turn = turn == Stone.Black ? Stone.White : Stone.Black;
        }

        private bool TryApplyMove(Position pos, Stone stone, out string errorMessage) // 현재 턴 적용
        {
            if (!board.IsInside(pos)) // 보드 범위 우선 체크
            {
                errorMessage = "보드 범위를 벗어났습니다.";
                return false;
            }

            if (!board.IsEmpty(pos)) // 돌이 배치되었는지 체크
            {
                errorMessage = "이미 돌이 배치되어 있습니다. 다른 곳에 배치해주세요.\n";
                return false;
            }
            if (rule.IsDoubleThree(board, pos, stone)) // 33 금수 체크
            {
                errorMessage = "33 금수입니다. 다른 곳에 배치해주세요.\n";
                return false;
            }

            if (!board.TryPlaceStone(pos, stone))
            {
                errorMessage = "돌을 배치할 수 없습니다.\n";
                return false;
            }

            errorMessage = "";
            return true;
        }
    }
}
