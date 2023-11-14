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

        public static Item[] items;
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
            Console.Write("   ");
            Console.Write($"이름 : {Name}");
            Console.Write(" | ");
            if (Price != 0) Console.Write($"가격 : {Price}");
            Console.Write(" | ");
            if (Attack != 0) Console.Write($"Atk {(Attack >= 0 ? "+" : "")}{Attack} ");
            if (Defend != 0) Console.Write($"Def {(Defend >= 0 ? "+" : "")}{Defend} ");
            if (Health != 0) Console.Write($"Hp {(Health >= 0 ? "+" : "")}{Health}");
            Console.WriteLine();

        }


        public static void EquipItem(int idx)
        {
            if (items[idx].Equip)
            {
                Console.WriteLine("이미 장착중인 아이템입니다.");
            }
            int remain = idx % 5;
            int start = idx - remain + 1;
            for (int i = start; i < start + 6; i++)
            {
                items[i].Equip = false;
            }
            items[idx].Equip = !items[idx].Equip;
            Player.player.Attack += items[idx].Attack;
            Player.player.Defend += items[idx].Defend;
            Player.player.Health += items[idx].Health;
        }
        public static void BuyItem(int input)
        {
            if (items[input].Have == true)
            {
                Console.WriteLine("이미 가지고 있는 물건입니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else if (Player.player.Money < items[input].Price)
            {
                Console.WriteLine("잔액이 부족합니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else
            {
                Player.player.Money -= items[input].Price;
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
            Player.player.Money += sellmoney;
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

        public static void ItemDataSetting()
        {
            Item.items = new Item[35];

            Item.AddItem(new Item(0, " ", " ", 1, 1, 1, 1, false, false));

            Item.AddItem(new Item(1, "물려받은 키보드", "weapon", 20, 0, 0, 10, false, false));
            Item.AddItem(new Item(2, "다이소 키보드", "weapon", 40, 0, 0, 50, false, false));
            Item.AddItem(new Item(3, "보급형 기계식 키보드", "weapon", 60, 0, 0, 250, false, false));
            Item.AddItem(new Item(4, "전문 브랜드 기계식 키보드", "weapon", 80, 0, 0, 1250, false, false));
            Item.AddItem(new Item(5, "장인의 맞춤제작 키보드", "weapon", 100, 0, 0, 6250, false, false));

            Item.AddItem(new Item(6, "다이소 마우스", "subweapon", 10, 5, 0, 10, false, false));
            Item.AddItem(new Item(7, "무선 마우스", "subweapon", 20, 10, 0, 50, false, false));
            Item.AddItem(new Item(8, "무선 버티컬 마우스", "subweapon", 30, 15, 0, 250, false, false));
            Item.AddItem(new Item(9, "전문 브랜드 마우스", "subweapon", 40, 20, 0, 1250, false, false));
            Item.AddItem(new Item(10, "장인의 맞춤제작 마우스", "subweapon", 50, 25, 0, 6250, false, false));

            Item.AddItem(new Item(11, "후드티&츄리닝 세트", "armor", 0, 0, 100, 10, false, false));
            Item.AddItem(new Item(12, "장인 맞춤제작 정장", "armor", 0, 0, 200, 50, false, false));
            Item.AddItem(new Item(13, "물려받은 정장", "armor", 0, 0, 300, 250, false, false));
            Item.AddItem(new Item(14, "깔끔한 댄디룩 스타일", "armor", 0, 0, 400, 1250, false, false));
            Item.AddItem(new Item(15, "아이언맨 슈트", "armor", 0, 0, 500, 6250, false, false));

            Item.AddItem(new Item(16, "귀마개", "shield", 0, 8, 0, 10, false, false));
            Item.AddItem(new Item(17, "유선 이어폰", "shield", 0, 16, 0, 50, false, false));
            Item.AddItem(new Item(18, "저가형 무선 이어폰", "shield", 0, 24, 0, 250, false, false));
            Item.AddItem(new Item(19, "고급 브랜드 무선 이어폰", "shield", 0, 32, 0, 1250, false, false));
            Item.AddItem(new Item(20, "최상급 브랜드 고오급 해드셋 ", "shield", 0, 40, 0, 6250, false, false));

            Item.AddItem(new Item(21, "손목보호대", "accessory", 0, 2, 50, 10, false, false));
            Item.AddItem(new Item(22, "등받이 쿠션", "accessory", 0, 4, 100, 50, false, false));
            Item.AddItem(new Item(23, "웹캠", "accessory", 0, 6, 150, 250, false, false));
            Item.AddItem(new Item(24, "더블 모니터", "accessory", 0, 8, 200, 1250, false, false));
            Item.AddItem(new Item(25, "전문 브랜드 맞춤 의자", "weapon", 0, 10, 250, 6250, false, false));

            Item.AddItem(new Item(26, "전설의 기운", "energy", 10, 10, 10, 0, false, false));
            Item.AddItem(new Item(27, "힘의 기운", "energy", 10, 0, 0, 0, false, false));
            Item.AddItem(new Item(28, "방어의 기운", "energy", 0, 10, 0, 0, false, false));
            Item.AddItem(new Item(29, "체력의 기운", "energy", 0, 0, 10, 0, false, false));
            Item.AddItem(new Item(30, "나태의 기운", "energy", -10, -10, -10, 0, false, false));


        }

    }


}






