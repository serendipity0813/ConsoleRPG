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

    public interface IItem
    {
        int Number { get; set; }
        bool Equip { get; set; }
        bool Have { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        int Attack { get; set; }
        int Defend { get; set; }
        int Health { get; set; }
        int Price { get; set; }

    }
    public class Item : IItem
    {
        public int Number { get; set; }
        public bool Equip { get; set; }
        public bool Have { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Attack { get; set; }
        public int Defend { get; set; }
        public int Health { get; set; }
        public int Price { get; set; }

        static Item[] items;
        public static int ItemCnt = 0;


        public Item(int number, string name, string type, int attack, int defend, int health, int price, bool have = false, bool equip = false)
        {
            Number = number;
            Name = name;
            Type = type;
            Attack = attack;
            Defend = defend;
            Health = health;
            Price = price;
            Equip = equip;
            Have = have;
        }

        public void PrintItemData()
        {
            if (Equip)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[");
                Console.Write("E");
                Console.Write("]");
                Console.ResetColor();
            }
            else if (Have)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[");
                Console.Write("I");
                Console.Write("]");
                Console.ResetColor();
            }
            else
                Console.Write(" ");
            Console.Write($"이름 : {Name}");
            Console.Write(" | ");
            if (Price != 0) Console.Write($"가격 : {Price}");
            Console.Write(" | ");
            if (Attack != 0) Console.Write($"Atk {(Attack >= 0 ? "+" : "")}{Attack} ");
            if (Defend != 0) Console.Write($"Def {(Defend >= 0 ? "+" : "")}{Defend} ");
            if (Health != 0) Console.Write($"Hp {(Health >= 0 ? "+" : "")}{Health}");
            Console.Write(" | ");
            Console.WriteLine();

        }


        public static void EquipItem(int idx)
        {
            int remain = idx % 5;
            int start = idx - remain + 1;
            for (int i = start; i < start + 5; i++)
            {
                items[i].Equip = false;
            }
            items[idx].Equip = !items[idx].Equip;
            player.Attack += items[idx].Attack;
            player.Defend += items[idx].Defend;
            player.Health += items[idx].Health;
        }
        public static void BuyItem(int input)
        {
            if (items[input].Have == true)
            {
                Console.WriteLine("이미 가지고 있는 물건입니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else if (player.Money < items[input].Price)
            {
                Console.WriteLine("잔액이 부족합니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else
            {
                player.Money -= items[input].Price;
                items[input].Have = true;
                Console.WriteLine($"{items[input].Price} 을 지불하고 {items[input].Name} 을 구입하였습니다.");
                Console.WriteLine("Enter를 누르면 상점으로 돌아갑니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }
        }
        public static void SellItem(int idx)
        {
            int sellmoney;
            sellmoney = items[idx].Price / 10 * 8;
            player.Money += sellmoney;
            items[idx].Have = !items[idx].Have;
            if (items[idx].Equip)
                items[idx].Equip = !items[idx].Equip;
        }
        public static void AddItem(Item item)
        {
            if (Item.ItemCnt == 34) return;
            items[Item.ItemCnt] = item;
            Item.ItemCnt++;
        }

    }


}






