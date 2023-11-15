using System;
using System.Threading;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{
    public class Monster : ICharter     //몬스터 클래스 - 캐릭터 인터페이스 종속
    {
        //몬스터 클래스 필드
        public string Name { get; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defend { get; set; }
        public int Money { get; set; }
        public bool IsDead => Health <= 0;      //체력이 0 이하가 되면 트루값 반환
        public static int StageCnt = 0;         //스테이지 카운트 변수
        public static Monster[] companys;       //몬스터 배열 선언

        //몬스터 클래스 속성
        public Monster(string name, int health, int attack, int defend, int money)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defend = defend;
            Money = money;
        }

        public static void MonsterDataSetting()     //몬스터 데이터값을 배열에 저장
        {
            companys = new Monster[10];     

            companys[0] = new Monster(" ", 1, 1, 1, 1);
            companys[1] = new Monster("아르바이트", 100, 10, 5, 10);
            companys[2] = new Monster("중소기업", 250, 40, 15, 50);
            companys[3] = new Monster("중견기업", 400, 70, 25, 250);
            companys[4] = new Monster("대기업", 550, 100, 35, 1250);
            companys[5] = new Monster("글로벌기업", 700, 130, 45, 6250);
            companys[6] = new Monster("스파르타코딩클럽", 1000, 180, 60, 50000);
          
        }


        public void TakeDamage(int damage)      //전투 진행시 몬스터가 데미지를 받는 메소드
        {
            Health -= (damage-Defend);          //플레이어 데이지 계산과 동일하게 적용
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage-Defend}의 데미지를 받았습니다. 남은 체력: {Health}");
        }

        public static void Work(int idx)        //던전 진행 메소드 - stage 선택시 선택한 숫자를 idx로 받아서 배열의 몬스터 선택
        {
            Console.Clear();
            int Pmaxhp = Player.player.Health;          //던전 진행 후 체력을 원상태로 전환하기 위해 기존 체력값 저장
            int Mmaxhp = Monster.companys[idx].Health;
            Player.player.ticket++;                     //특수 재화인 티켓 습득 
            Console.WriteLine("회사로 출근합니다!");
            Console.WriteLine($"출근합니다! 플레이어 정보: 체력({Player.player.Health}), 공격력({Player.player.Attack}), 방어력({Player.player.Defend})");
            Console.WriteLine($"회사정보 : 이름({companys[idx].Name}), 체력({companys[idx].Health}), 공격력({companys[idx].Attack}), 방어력({Player.player.Defend})");
            Console.WriteLine("----------------------------------------------------");      //플레이어와 몬스터 정보 출력 후 전투 시작

            while (!Player.player.IsDead && !companys[idx].IsDead) // 플레이어나 몬스터가 죽을 때까지 반복
            {
                // 플레이어의 턴
                Console.WriteLine($"{Player.player.Name}의 턴!");
                companys[idx].TakeDamage(Player.player.Attack);     //플레이어의 턴이므로 몬스터 데미지 받음
                Console.WriteLine();
                Thread.Sleep(1000);     //잠시 멈추는 시간

                if (companys[idx].IsDead)       //몬스터 체력이 0이하가 된 경우
                {
                    Player.player.Health = Pmaxhp;      //플레이어 체력 복구
                    Monster.companys[idx].Health = Mmaxhp;      //몬스터 체력 복구

                    Player.player.Money += companys[idx].Money;     //몬스터의 money값 만큼 플레이어 머니 추가
                    Player.player.exp += idx;       //경험치 획득
                    Console.WriteLine($"{idx} 만큼 경험치를 획득합니다");
                    Console.WriteLine($"무사히 퇴근합니다. {companys[idx].Money}만큼 보수를 획득하고 ticket을 1개 획득합니다.");

                    if (Player.player.exp >= Player.player.Level * 10)      //플레이어가 레벨*10 만큼 경험치 획득시 레벨업 
                    {
                        Console.WriteLine("일정량 이상의 경험치를 획득, LEVEL UP! - 공격력, 방어력, 체력이 일정수치 상승합니다.");
                        Player.player.exp -= Player.player.Level * 10;      //경험치를 줄이고 레벨이 상승하며 모든 스텟 상승
                        Player.player.Level++;
                        Player.player.Attack += 5;
                        Player.player.Defend += 5;
                        Player.player.Health += 50;

                    }
                    break;
                }

                // 몬스터의 턴
                Console.WriteLine($"{companys[idx].Name}의 턴!");     
                Player.player.TakeDamage(companys[idx].Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (Player.player.IsDead)       //플레이어 사망시 몬스터, 플레이어 체력 회복 후 로비 화면으로 이동
                {
                    Player.player.Health = Pmaxhp;
                    Monster.companys[idx].Health = Mmaxhp;
                    Console.WriteLine($"퇴사하고 집으로 돌아갑니다.");
                    break;
                }

        

            }
            Console.ReadKey();      //결과값 확인 및 씬 전환을 위해 리드키 호출 -> 키 입력시 로비 화면으로 이동
            GameManager.DisplayHome();
        }
    }
}

    

   
   



