﻿using System;
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
    internal class GameManager
    {


        //Display 함수 모음    

        public static void DisplayHome()
        {
            Console.Clear();    //콘솔창 정리 후 집에 도착했다는 문구와 함께 선택지 출력

            Console.WriteLine("삑 삑 삐삐삑...  띠로리~");
            Console.WriteLine("집에 들어오니 마음이 안정되는 것 같다.");
            Console.WriteLine("다음 행동을 선택하세요.");
            Console.WriteLine();
            Console.WriteLine("1. 현재상태 확인");
            Console.WriteLine("2. 인벤토리 확인");
            Console.WriteLine("3. 인터넷 쇼핑");
            Console.WriteLine("4. 지인 만나기");
            Console.WriteLine("5. 회사로 출근");
            Console.WriteLine();

            int input = CheckInput(1, 5);

            switch (input)
            {
                case 1:
                    DisplayInfo();
                    break;
                case 2:
                    DisplayInventory();
                    break;
                case 3:
                    DisplayShop();
                    break;
                case 4:
                    DisplayCommunity();
                    break;
                case 5:
                    DisplayCompany();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    CheckInput(1, 5);
                    break;

            }

        }

        public static void DisplayInfo()
        {
            Console.Clear();

            Console.WriteLine("나의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"이름 : {player.Name}");
            Console.WriteLine($"직업 : {player.Job}");
            Console.WriteLine($"레벨 : {player.Level}");
            int bonusHealth = Player.GetBonusHealth();
            Console.WriteLine($"체력 : {player.Health} ( + {bonusHealth})");
            int bonusAttack = Player.GetBonusAttack();
            Console.WriteLine($"공격력 : {player.Attack} ( + {bonusAttack})");
            int bonusDefend = Player.GetBonusDefend();
            Console.WriteLine($"방어력 : {player.Defend} ( + {bonusDefend})");
            Console.WriteLine($"가진돈 : {player.Money}");
            Console.WriteLine();
            Console.WriteLine("나가기 : 0");

            int input = CheckInput(0, 0);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
            }
        }

        public static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("소유하고 있는 아이템을 확인합니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("[장착/소지 여부]  |  [아이템 번호]   |  [아이템 이름]  |  [아이템 타입]  |  [아이템 효과]  ");
            for (int i = 1; i < Item.ItemCnt; i++)
            {
                if (items[i].Have == true)
                {
                    items[i].PrintItemData();
                }
            }

            Console.WriteLine(" ");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("2. 당근마켓에 아이템 팔기");
            Console.WriteLine(" ");
            Console.WriteLine("원하시는 행동을 입력하시오");
            Console.WriteLine();

            int input = CheckInput(0, 2);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    DisplayItemEquip();
                    break;
                case 2:
                    DisplayItemSell();
                    break;

            }

        }

        public static void DisplayShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("필요한 물품을 사기 위해 핸드폰을 킨다. 어떤 사이트에 들어갈까?");
            Console.WriteLine();
            Console.WriteLine("1. 다나와에 들어가서 키보드(무기) 를 구입한다.");
            Console.WriteLine("2. 네이버 스토어에 들어가서 마우스(보조무기) 를 구입한다.");
            Console.WriteLine("3. 무신사에 들어가서 옷(방어구) 를 구입한다.");
            Console.WriteLine("4. 11번가에 들어가서 이어폰(방패) 를 구입한다.");
            Console.WriteLine("5. 쿠팡에 들어가서 여러가지 물건(엑세서리) 를 보고 구입한다.");
            Console.WriteLine();
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");

            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    DisplayWeaponShop();
                    break;
                case 2:
                    DisplaySubweaponShop();
                    break;
                case 3:
                    DisplayArmorShop();
                    break;
                case 4:
                    DisplayShieldShop();
                    break;
                case 5:
                    DisplayAccessoryShop();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }

        }

        public static void DisplayWeaponShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("구입하려는 키보드를 선택하세요.");
            Console.WriteLine();
            for (int i = 1; i < 6; i++)
            {
                Console.Write("{0}. ", i);
                items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 키보드의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    Item.BuyItem(input);
                    break;
                case 2:
                    Item.BuyItem(input);
                    break;
                case 3:
                    Item.BuyItem(input);
                    break;
                case 4:
                    Item.BuyItem(input);
                    break;
                case 5:
                    Item.BuyItem(input);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }


        }
        public static void DisplaySubweaponShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("구입하려는 마우스를 선택하세요.");
            Console.WriteLine();
            for (int i = 6; i < 11; i++)
            {
                Console.Write("{0}. ", i - 5);
                items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 마우스의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    input += 5;
                    Item.BuyItem(input);
                    break;
                case 2:
                    input += 5;
                    Item.BuyItem(input);
                    break;
                case 3:
                    input += 5;
                    Item.BuyItem(input);
                    break;
                case 4:
                    input += 5;
                    Item.BuyItem(input);
                    break;
                case 5:
                    input += 5;
                    Item.BuyItem(input);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }

        }
        public static void DisplayArmorShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("구입하려는 옷을 선택하세요.");
            Console.WriteLine();
            for (int i = 11; i < 16; i++)
            {
                Console.Write("{0}. ", i - 10);
                items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 옷의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    input += 10;
                    Item.BuyItem(input);
                    break;
                case 2:
                    input += 10;
                    Item.BuyItem(input);
                    break;
                case 3:
                    input += 10;
                    Item.BuyItem(input);
                    break;
                case 4:
                    input += 10;
                    Item.BuyItem(input);
                    break;
                case 5:
                    input += 10;
                    Item.BuyItem(input);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }
        }
        public static void DisplayShieldShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("구입하려는 이어폰을 선택하세요.");
            Console.WriteLine();
            for (int i = 16; i < 21; i++)
            {
                Console.Write("{0}. ", i - 15);
                items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 이어폰의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    input += 15;
                    Item.BuyItem(input);
                    break;
                case 2:
                    input += 15;
                    Item.BuyItem(input);
                    break;
                case 3:
                    input += 15;
                    Item.BuyItem(input);
                    break;
                case 4:
                    input += 15;
                    Item.BuyItem(input);
                    break;
                case 5:
                    input += 15;
                    Item.BuyItem(input);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }
        }
        public static void DisplayAccessoryShop()
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {player.Money}");
            Console.WriteLine("구입하려는 꿀템을 선택하세요.");
            Console.WriteLine();
            for (int i = 21; i < 26; i++)
            {
                Console.Write("{0}. ", i - 20);
                items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 꿀템의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    input += 20;
                    Item.BuyItem(input);
                    break;
                case 2:
                    input += 20;
                    Item.BuyItem(input);
                    break;
                case 3:
                    input += 20;
                    Item.BuyItem(input);
                    break;
                case 4:
                    input += 20;
                    Item.BuyItem(input);
                    break;
                case 5:
                    input += 20;
                    Item.BuyItem(input);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }
        }

        public static void DisplayItemEquip()
        {
            Console.WriteLine("장착하려는 아이템의 번호를 입력해주세요.");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            int input = CheckInput(0, Item.ItemCnt);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                default:
                    Item.EquipItem(input);
                    DisplayInventory();
                    break;
            }
        }

        public static void DisplayItemSell()
        {
            Console.WriteLine("판매하려는 아이템의 번호를 입력해주세요.");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            int input = CheckInput(0, Item.ItemCnt);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                default:
                    Item.SellItem(input);
                    DisplayInventory();
                    break;
            }
        }

        public static void DisplayCommunity()
        {
            Console.Clear();
            Console.WriteLine("ticket을 5개 소비하고 만나고자 하는 지인을 선택하세요");
            Console.WriteLine($"보유티켓 : {player.ticket}");
            Console.WriteLine("1. 부모님");
            Console.WriteLine("2. 학창시절 친구");
            Console.WriteLine("3. 직장 동료");
            Console.WriteLine("4. 대학교 동기");
            Console.WriteLine("5. 랜덤채팅");
            Console.WriteLine("0. 오늘은 집에서 쉬도록 하자!");

            int input = CheckInput(0, 5);
            int num = input + 25;

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                case 1:
                    player.ticket -= 5;
                    Console.WriteLine("사랑하는 부모님과 식사를 하며 응원과 지지를 받습니다.");
                    Console.WriteLine("전설의 기운 획득 - 모든 스텟 +10");
                    Item.EquipItem(num);
                    items[num].Have = true;
                    DisplayHome();
                    break;
                case 2:
                    player.ticket -= 5;
                    Console.WriteLine("학창시절 친구와 술 한잔 하며 좋은 기운을 받습니다.");
                    Console.WriteLine("힘의 기운 획득 - 공격력 +10");
                    Item.EquipItem(num);
                    items[num].Have = true;
                    DisplayHome();
                    break;
                case 3:
                    player.ticket -= 5;
                    Console.WriteLine("직장 동료와 만나 친해지며 사이가 돈독해집니다.");
                    Console.WriteLine("방어의 기운 획득 - 방어 +10");
                    Item.EquipItem(num);
                    items[num].Have = true;
                    DisplayHome();
                    break;
                case 4:
                    player.ticket -= 5;
                    Console.WriteLine("대학시절 동기를 만나 최신동향 정보와 꿀팁을 공유합니다.");
                    Console.WriteLine("체력의 기운 획득 - 체력 +10");
                    Item.EquipItem(num);
                    items[num].Have = true;
                    DisplayHome();
                    break;
                case 5:
                    player.ticket -= 5;
                    Console.WriteLine("랜덤채팅에서 이상한 사람을 만나 큰일날 뻔 했지만 겨우 도망쳤습니다.");
                    Console.WriteLine("나태의 기운 획득 - 모든 스텟 -10");
                    Item.EquipItem(num);
                    items[num].Have = true;
                    DisplayHome();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;

            }
        }
         
        public static void DisplayCompany()
        {
            Console.Clear();
            Console.WriteLine("출근하려는 회사를 선택하세요.");
            Console.WriteLine("1. 아르바이트 : 공방합 10이상, 체력 100이상 권장");
            Console.WriteLine("2. 중소기업 : 공방합 25이상, 체력 250이상 권장");
            Console.WriteLine("3. 중견기업 : 공방합 40이상, 체력 400이상 권장");
            Console.WriteLine("4. 대기업 : 공방합 55이상, 체력 550이상 권장");
            Console.WriteLine("5. 글로벌기업 : 공방합 70이상, 체력 700이상 권장");
            Console.WriteLine("6. 히든 : 모든 최상위 아이템 장착 권장");
            Console.WriteLine("0. 오늘은 집에서 쉬도록 하자!");

            int input = CheckInput(0, 6);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                default:
                    Monster.Work(input);
                    break;

            }

        }

        public static void DataSetting()
        {
            player = new Player("Unity", "개발자", 1, 100, 10, 5, 100000);      //캐릭터 초기값 세팅
            items = new Item[35];
            companys = new Monster[10];

            companys[0] = new Monster(" ", 1, 1, 1, 1);
            companys[1] = new Monster("아르바이트", 100, 10, 0, 10);
            companys[2] = new Monster("중소기업", 250, 40, 10, 50);
            companys[3] = new Monster("중견기업", 400, 70, 20, 250);
            companys[4] = new Monster("대기업", 550, 100, 30, 1250);
            companys[5] = new Monster("글로벌기업", 700, 130, 40, 6250);
            companys[6] = new Monster("스파르타코딩클럽", 1000, 200, 0, 50000);

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
        public static int CheckInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var select);        //예외처리 진행하며 입력받은 값 int 변환

                if (parseSuccess)   //변수 입력이 제대로 되었을 때
                {
                    if (select >= min && select <= max)     //입력값이 선택창의 최소, 최대 범위안에 속할 때
                        return select;
                    else
                        Console.WriteLine("입력값이 잘못되었습니다. 올바른 범위의 숫자를 입력해주세요!");
                }
                else
                    Console.WriteLine("입력값이 잘못되었습니다. 다시 입력해주세요!");  //아니라면 오류문구 출력

            }


        }


    }


}


   