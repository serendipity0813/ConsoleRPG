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

    //인터페이스, 클래스 등

    public interface ICharter
    {
        string Name { get; }
        int Health { get; }
        int Attack { get; }
        int Defend { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
    }
    public class Player : ICharter
    {
        //캐릭터 클래스 필드
        public string Name { get; }
        public string Job { get; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defend { get; set; }
        public int Money { get; set; }
        public int exp = 0;
        public int ticket = 0;
        public bool IsDead => Health < 0;
        public static Player player;


        //캐릭터 클래스 속성
        public Player(string name, string job, int level, int health, int attack, int defend, int money)
        {
            Name = name;
            Job = job;
            Level = level;
            Health = health;
            Attack = attack;
            Defend = defend;
            Money = money;
        }


        public static void PlayerDataSetting()
        {
            player = new Player("Unity", "개발자", 1, 100, 10, 5, 10);      //캐릭터 초기값 세팅
        }

        public void TakeDamage(int damage)
        {
            Health -= (damage - player.Defend);
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage}의 데미지를 받았습니다. 남은 체력: {Health}");
        }

        public static int GetBonusAttack()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip) sum += Item.items[i].Attack;
            }
            return sum;
        }

        public static int GetBonusDefend()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip) sum += Item.items[i].Defend;
            }
            return sum;
        }

        public static int GetBonusHealth()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip) sum += Item.items[i].Health;
            }

            return sum;
        }


    }

}
     


   



