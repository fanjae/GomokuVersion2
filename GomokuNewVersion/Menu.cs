namespace GomokuNewVersion
{
    internal class Menu
    {
        public void Run()
        {
            while (true)
            {
                int input = ShowMenu();

                switch (input)
                {
                    case 1:
                        PlayerVsPlayer();
                        break;
                    case 2:
                        ShowHowToPlay();
                        break;
                    case 3:
                        ShowCredits();
                        break;
                    case 4:
                        Console.WriteLine("프로그램을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("[Error] Program Logic Error");
                        break;
                }
            }
        }

        private int ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("============================");
                Console.WriteLine("오목 게임을 플레이합니다!");
                Console.WriteLine("1. 2인용 플레이");
                Console.WriteLine("2. 게임 방법");
                Console.WriteLine("3. Credits");
                Console.WriteLine("4. 종료");
                Console.WriteLine("============================");
                Console.Write("원하는 메뉴를 입력하세요 : ");

                string? inputText = Console.ReadLine();

                if (int.TryParse(inputText, out int input) && input >= 1 && input <= 4)
                    return input;

                Console.WriteLine("다시 입력하세요.");
                Console.ReadKey(true);
            }
        }

        private void ShowHowToPlay() // 게임 플레이 방법 메서드
        {
            Console.Clear();
            Console.WriteLine("============================");
            Console.WriteLine("오목은 보드판 위에 5개의 돌을 연속으로 놓으면 승리하는 게임입니다.");
            Console.WriteLine("현재 2인용 게임만 구현되어 있으며, 1P가 먼저 시작합니다.");
            Console.WriteLine();
            Console.WriteLine("[단축키]");
            Console.WriteLine("키보드 방향키 : 보드판에서 이동합니다.");
            Console.WriteLine("Space Bar : 바둑돌을 놓습니다.");
            Console.WriteLine("============================");
            Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
            Console.ReadKey(true);
        }

        private void ShowCredits() // 크레딧 메서드
        {
            Console.Clear();
            Console.WriteLine("Made by. FanJae.");
            Console.WriteLine("아무 키나 누르면 메뉴로 돌아갑니다.");
            Console.ReadKey(true);
        }

        private void PlayerVsPlayer()
        {
            BoardRenderer renderer = new BoardRenderer();

            IPlayer blackPlayer = new UserPlayer(Stone.Black, renderer);
            IPlayer whitePlayer = new UserPlayer(Stone.White, renderer);

            GameController gameController = new GameController(blackPlayer, whitePlayer, renderer);
            gameController.Run();
            
        }
    }
}
