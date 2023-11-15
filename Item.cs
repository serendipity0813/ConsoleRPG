using System;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{

    public interface IItem      //아이템 인터페이스
    {
        int Number { get; }
        bool Equip { get; set; }        //장착여부 메소드로 수정 가능
        bool Have { get; set; }         //아이템 구매, 판매 메소드로 수정 가능
        string Name { get; }
        string Type { get; }
        int Attack { get; }
        int Defend { get; }
        int Health { get; }
        int Price { get; }

    }
    public class Item : IItem
    {
        public int Number { get;}
        public bool Equip { get; set; }     //장착여부 메소드로 수정 가능
        public bool Have { get; set; }      //아이템 구매, 판매 메소드로 수정 가능
        public string Name { get; }
        public string Type { get;}
        public int Attack { get; }
        public int Defend { get; }
        public int Health { get; }
        public int Price { get; }

        public static Item[] items;     //아이템 클래스 배열 선언
        public static int ItemCnt = 0;      //아이템 개수 카운터


        public Item(int number, string name, string type, int attack, int defend, int health, int price, bool have = false, bool equip = false)
        {  
            //아이템 클래스 속성
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

        public void PrintItemData()     //아이템 데이터 출력 함수 
        {
            if (Equip)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;        //아이템 장착시 출력
                Console.Write("[");
                Console.Write("E");
                Console.Write("]");
                Console.ResetColor();
            }
            else if (Have)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;      //아이템 보유시 출력
                Console.Write("[");
                Console.Write("I");
                Console.Write("]");
                Console.ResetColor();
            }
            else
            Console.Write("   ");                   //아이템 이름, 가격 등 출력 후 아이템 효과중 0이 아닌 효과 출력
            Console.Write($"이름 : {Name}");
            Console.Write(" | ");
            if (Price != 0) Console.Write($"가격 : {Price}");
            Console.Write(" | ");
            if (Attack != 0) Console.Write($"Atk {(Attack >= 0 ? "+" : "")}{Attack} ");
            if (Defend != 0) Console.Write($"Def {(Defend >= 0 ? "+" : "")}{Defend} ");
            if (Health != 0) Console.Write($"Hp {(Health >= 0 ? "+" : "")}{Health}");
            Console.WriteLine();

        }


        public static void EquipItem(int idx)       //아이템 장착 메소드
        {
            if (items[idx].Equip)       //이미 장착중인 아이템의 경우 출력
            {
                Console.WriteLine("이미 장착중인 아이템입니다.");
            }
            int remain = idx % 5;                                       //아이템이 5단위로 타입이 나뉘기 때문에 5로 나눠 나머지 확인
            int start = idx - remain + 1;                               //해당 아이템 타입의 첫 번째 아이템 변수를 찾아 START에 저장 
            for (int i = start; i < start + 6; i++)                     //해당 아이템 부터 5개의 아이템(같은 타입 아이템)의 장착 여부를 FALSE로 전환 - 단일장착을 위해
            {
                items[i].Equip = false;     
            }
            items[idx].Equip = !items[idx].Equip;                       //선택한 아이템을 장착하며 아이템 효과를 캐릭터 속성에 추가 합산하여 적용
            Player.player.Attack += items[idx].Attack;
            Player.player.Defend += items[idx].Defend;
            Player.player.Health += items[idx].Health;
        }
        public static void BuyItem(int input)               //아이템 구매 메소드
        {
            if (items[input].Have == true)      //아이템을 이미 가지고 있는 경우 아이템샵 씬 다시 호출
            {
                Console.WriteLine("이미 가지고 있는 물건입니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else if (Player.player.Money < items[input].Price)      //아이템 가격보다 보유 금액이 부족한 경우 아이템샵 씬 다시 호출
            {
                Console.WriteLine("잔액이 부족합니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }

            else
            {
                Player.player.Money -= items[input].Price;          //아이템 금액만큼 보유금액 차감 후 아이템 보유 bool값을 true로 전환
                items[input].Have = true;
                Console.WriteLine($"{items[input].Price} 을 지불하고 {items[input].Name} 을 구입하였습니다.");
                Console.WriteLine("Enter를 누르면 상점으로 돌아갑니다.");
                Console.ReadKey();
                GameManager.DisplayShop();
            }
        }
        public static void SellItem(int idx)        //아이템 판매 메소드
        {
            int sellmoney;
            sellmoney = items[idx].Price / 10 * 8;      //판매금액을 아이템 가격의 80%로 계산하여 저장
            Player.player.Money += sellmoney;           //판매 금액만큼 플레이어 보유 금액 추가합산
            items[idx].Have = !items[idx].Have;             
            if (items[idx].Equip)
                items[idx].Equip = !items[idx].Equip;       //아이템을 장착하고 있었다면 해제하고 아이템 소지 bool값을 false로 전환
        }
        public static void AddItem(Item item)               //아이템 추가 메소드
        {          
                if (Item.ItemCnt == 34) return;             //아이템 숫자는 35개 까지 (0번 포함) 가능
                items[Item.ItemCnt] = item;                 //아이템 배열에 해당 아이템 데이터를 저장
                Item.ItemCnt++;         

        }

        public static void ItemDataSetting()        //아이템 배열 선언 후 아이템 클래스 객체 생성 후 배열에 저장
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






