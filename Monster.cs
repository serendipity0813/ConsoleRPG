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

        //캐릭터 클래스 속성
        public Monster(string name, int health, int attack, int defend, int money)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defend = defend;
            Money = money;
        }


        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Health}");
        }

        public static void Work(int idx)
        {
            idx = idx + 1;
            Console.Clear();
            Console.WriteLine($"출근합니다! 플레이어 정보: 체력({player.Health}), 공격력({player.Attack}), 방어력({player.Defend})");
            Console.WriteLine($"회사정보 : 이름({companys[idx].Name}), 체력({companys[idx].Health}), 공격력({companys[idx].Attack}), 방어력({player.Defend})");
            Console.WriteLine("----------------------------------------------------");

            while (!player.IsDead && !companys[idx].IsDead) // 플레이어나 몬스터가 죽을 때까지 반복
            {
                // 플레이어의 턴
                Console.WriteLine($"{player.Name}의 턴!");
                companys[idx].TakeDamage(player.Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (companys[idx].IsDead)
                {
                    player.Money += companys[idx].Money;
                    player.exp += idx;
                    Console.WriteLine($"{idx} 만큼 경험치를 획득합니다");
                    Console.WriteLine($"무사히 퇴근합니다. {companys[idx].Money}만큼 보수를 획득하고 ticket을 1개 획득합니다.");
                    if (player.exp >= player.Level * 10)
                    {
                        Console.WriteLine("일정량 이상의 경험치를 획득, LEVEL UP! - 공격력, 방어력, 체력이 일정수치 상승합니다.");
                        player.exp -= player.Level * 10;
                        player.Level++;
                        player.Attack++;
                        player.Defend++;
                        player.Health += 10;

                    }
                    break;
                }

                // 몬스터의 턴
                Console.WriteLine($"{companys[idx].Name}의 턴!");
                player.TakeDamage(companys[idx].Attack);
                Console.WriteLine();
                Thread.Sleep(1000);

                if (player.IsDead)
                {
                    Console.WriteLine($"퇴사하고 집으로 돌아갑니다.");
                    break;
                }

        

            }
            Console.ReadKey();
            GameManager.DisplayHome();
        }
    }
}

    

   
   



