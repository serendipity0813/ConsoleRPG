using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{
    public class Monster : ICharter
    {
        //캐릭터 클래스 필드
        public string Name { get; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defend { get; set; }
        public int Money { get; set; }
        public bool IsDead => Health < 0;
        public static int StageCnt = 0;
        public static Monster[] companys;

        //캐릭터 클래스 속성
        public Monster(string name, int health, int attack, int defend, int money)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defend = defend;
            Money = money;
        }

        public static void MonsterDataSetting()
        {
            companys = new Monster[10];

            companys[0] = new Monster(" ", 1, 1, 1, 1);
            companys[1] = new Monster("아르바이트", 100, 10, 0, 10);
            companys[2] = new Monster("중소기업", 250, 40, 10, 50);
            companys[3] = new Monster("중견기업", 400, 70, 20, 250);
            companys[4] = new Monster("대기업", 550, 100, 30, 1250);
            companys[5] = new Monster("글로벌기업", 700, 130, 40, 6250);
            companys[6] = new Monster("스파르타코딩클럽", 1000, 200, 0, 50000);
          
        }



        public void TakeDamage(int damage)
        {
            Health -= (damage-Defend);
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Health}");
        }

        public static void Work(int idx)
        {
            Console.Clear();
            int Pmaxhp = Player.player.Health;
            int Mmaxhp = Monster.companys[idx].Health;
            Console.WriteLine($"출근합니다! 플레이어 정보: 체력({Player.player.Health}), 공격력({Player.player.Attack}), 방어력({Player.player.Defend})");
            Console.WriteLine($"회사정보 : 이름({companys[idx].Name}), 체력({companys[idx].Health}), 공격력({companys[idx].Attack}), 방어력({Player.player.Defend})");
            Console.WriteLine("----------------------------------------------------");

            while (!Player.player.IsDead && !companys[idx].IsDead) // 플레이어나 몬스터가 죽을 때까지 반복
            {
                // 플레이어의 턴
                Console.WriteLine($"{Player.player.Name}의 턴!");
                companys[idx].TakeDamage(Player.player.Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (companys[idx].IsDead)
                {
                    Player.player.Health = Pmaxhp;
                    Monster.companys[idx].Health = Mmaxhp;

                    Player.player.Money += companys[idx].Money;
                    Player.player.exp += idx;
                    Console.WriteLine($"{idx} 만큼 경험치를 획득합니다");
                    Console.WriteLine($"무사히 퇴근합니다. {companys[idx].Money}만큼 보수를 획득하고 ticket을 1개 획득합니다.");
                    if (Player.player.exp >= Player.player.Level * 10)
                    {
                        Console.WriteLine("일정량 이상의 경험치를 획득, LEVEL UP! - 공격력, 방어력, 체력이 일정수치 상승합니다.");
                        Player.player.exp -= Player.player.Level * 10;
                        Player.player.Level++;
                        Player.player.Attack++;
                        Player.player.Defend++;
                        Player.player.Health += 10;

                    }
                    break;
                }

                // 몬스터의 턴
                Console.WriteLine($"{companys[idx].Name}의 턴!");
                Player.player.TakeDamage(companys[idx].Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (Player.player.IsDead)
                {
                    Player.player.Health = Pmaxhp;
                    Monster.companys[idx].Health = Mmaxhp;
                    Console.WriteLine($"퇴사하고 집으로 돌아갑니다.");
                    break;
                }

        

            }
            Console.ReadKey();
            GameManager.DisplayHome();
        }
    }
}

    

   
   



