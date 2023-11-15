using System;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{
    internal class GameManager      //게임 기능적인 부분을 관리하는 클래스
    {


        //Display 함수 모음    

        public static void DisplayHome()        //메인 로비화면 출력
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

            int input = CheckInput(1, 5);       //입력하는 숫자에 따라 화면 출력

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

        public static void DisplayInfo()        //캐릭터 정보 화면 출력
        {
            Console.Clear();

            Console.WriteLine("나의 정보를 표시합니다.");         //플레이어의 이름, 직업 등등 수치 출력 및 추가수치도 따로 표시
            Console.WriteLine();
            Console.WriteLine($"이름 : {Player.player.Name}");
            Console.WriteLine($"직업 : {Player.player.Job}");
            Console.WriteLine($"레벨 : {Player.player.Level}");
            int bonusHealth = Player.GetBonusHealth();
            Console.WriteLine($"체력 : {Player.player.Health} ( + {bonusHealth})");
            int bonusAttack = Player.GetBonusAttack();
            Console.WriteLine($"공격력 : {Player.player.Attack} ( + {bonusAttack})");
            int bonusDefend = Player.GetBonusDefend();
            Console.WriteLine($"방어력 : {Player.player.Defend} ( + {bonusDefend})");
            Console.WriteLine($"가진돈 : {Player.player.Money}");
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

        public static void DisplayInventory()       //아이템 인벤토리 출력
        {
            Console.Clear();

            Console.WriteLine("소유하고 있는 아이템을 확인합니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            for (int i = 1; i < Item.ItemCnt; i++)      //모든 아이템을 확인하며 HAVE 값이 TRUE라면 아이템 데이터 출력
            {
                if (Item.items[i].Have == true)
                {
                    Console.Write($"{Item.items[i].Number}. ");
                    Item.items[i].PrintItemData();
                }
            }

            Console.WriteLine(" ");             //추가로 진행할 수 있는 기능 출력

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

            Console.Clear();
        }

        public static void DisplayShop()        //아이템 상점화면 출력 
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
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

        public static void DisplayWeaponShop()      //무기상점 출력
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
            Console.WriteLine("구입하려는 키보드를 선택하세요.");
            Console.WriteLine();
            for (int i = 1; i < 6; i++)     //아이템 고유번호 1~5번까지 아이템 데이터 출력
            {
                Console.Write("{0}. ", i);
                Item.items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 키보드의 번호를 입력하세요.");


            int input = CheckInput(0, 5);       //아이템 구매시 선택한 번호를 idx로 받아 아이템 구매 메소드 실행

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
        public static void DisplaySubweaponShop()       //보조무기 상점화면 출력
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
            Console.WriteLine("구입하려는 마우스를 선택하세요.");
            Console.WriteLine();
            for (int i = 6; i < 11; i++)                //보조무기 아이템 데이터 출력
            {
                Console.Write("{0}. ", i - 5);
                Item.items[i].PrintItemData();
            }
            Console.WriteLine("0. 다음을 기약하며 핸드폰을 끈다.");
            Console.WriteLine();
            Console.WriteLine("구매를 원하는 마우스의 번호를 입력하세요.");


            int input = CheckInput(0, 5);

            switch (input)          //선택한 번호 +5를 인덱스로 받아 아이템 구매 메소드 출력 - 보조무기는 6부터 시작하기 때문
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
        public static void DisplayArmorShop()       //갑옷 상점 출력, 기능은 다른 상점과 동일함
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
            Console.WriteLine("구입하려는 옷을 선택하세요.");
            Console.WriteLine();
            for (int i = 11; i < 16; i++)
            {
                Console.Write("{0}. ", i - 10);
                Item.items[i].PrintItemData();
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
        public static void DisplayShieldShop()      //방어구 상점 출력, 기능 동일
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
            Console.WriteLine("구입하려는 이어폰을 선택하세요.");
            Console.WriteLine();
            for (int i = 16; i < 21; i++)
            {
                Console.Write("{0}. ", i - 15);
                Item.items[i].PrintItemData();
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
        public static void DisplayAccessoryShop()       //장신구 상점 출력, 기능은 동일
        {
            Console.Clear();

            Console.WriteLine($"현재 보유금액 : {Player.player.Money}");
            Console.WriteLine("구입하려는 꿀템을 선택하세요.");
            Console.WriteLine();
            for (int i = 21; i < 26; i++)
            {
                Console.Write("{0}. ", i - 20);
                Item.items[i].PrintItemData();
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

        public static void DisplayItemEquip()       //아이템 장착관리 선택시 출력되는 화면
        {
            Console.WriteLine("장착하려는 아이템의 번호를 입력해주세요.");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            int input = CheckInput(0, Item.ItemCnt);        //장착을 원하는 아이템 선택시 선택 숫자를 idx로 받아 아이템 장착 메소드 출력

            switch (input)
            {
                case 0://장착을 원하는 아이템 선택시 선택 숫자를 idx로 받아 아이템 장착 메소드 출력
                    DisplayHome();
                    break;
                default:
                    Item.EquipItem(input);
                    DisplayInventory();
                    break;
            }
        }

        public static void DisplayItemSell()        //아이템 판매 선택시 출력되는 화면
        {
            Console.WriteLine("판매하려는 아이템의 번호를 입력해주세요.");
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            int input = CheckInput(0, Item.ItemCnt);        //판매를 원하는 아이템 선택시 번호를 idx로 받아 아이템 판매 메소드 출력

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

        public static void DisplayCommunity()       //커뮤니티 화면 출력
        {
            Console.Clear();
            Console.WriteLine("ticket을 5개 소비하고 만나고자 하는 지인을 선택하세요");
            Console.WriteLine($"보유티켓 : {Player.player.ticket}");

            if (Player.player.ticket <= 5)          //커뮤니티 진행은 티켓이 5개 소비되므로 5개 이하 소지시 로비로 돌아가도록 설정
            {
                Console.WriteLine("티켓이 부족합니다. 엔터를 누르면 집으로 돌아갑니다.");
                Console.ReadKey();
                DisplayHome();

            }
            else
            {
                    //티켓이 5개 이상이라면 선택지 출력
            }
            {
                Console.WriteLine("1. 부모님");
                Console.WriteLine("2. 학창시절 친구");
                Console.WriteLine("3. 직장 동료");
                Console.WriteLine("4. 대학교 동기");
                Console.WriteLine("5. 랜덤채팅");
                Console.WriteLine("0. 오늘은 집에서 쉬도록 하자!");


                int input = CheckInput(0, 5);
                int num = input + 25;       //커뮤니티 진행시 획득하는 아이템 고유번호가 26~30이므로 

                switch (input)
                {
                    case 0:
                        DisplayHome();
                        break;
                    case 1:      //티켓을 소비하고 아이템 획득 및 자동장착
                        Player.player.ticket -= 5;      
                        Console.WriteLine("사랑하는 부모님과 식사를 하며 응원과 지지를 받습니다.");
                        Console.WriteLine("전설의 기운 획득 - 모든 스텟 +10");
                        Item.EquipItem(num);
                        Item.items[num].Have = true;
                        Console.ReadKey();
                        DisplayHome();
                        break;
                    case 2:
                        Player.player.ticket -= 5;
                        Console.WriteLine("학창시절 친구와 술 한잔 하며 좋은 기운을 받습니다.");
                        Console.WriteLine("힘의 기운 획득 - 공격력 +10");
                        Item.EquipItem(num);
                        Item.items[num].Have = true;
                        Console.ReadKey();
                        DisplayHome();
                        break;
                    case 3:
                        Player.player.ticket -= 5;
                        Console.WriteLine("직장 동료와 만나 친해지며 사이가 돈독해집니다.");
                        Console.WriteLine("방어의 기운 획득 - 방어 +10");
                        Item.EquipItem(num);
                        Item.items[num].Have = true;
                        Console.ReadKey();
                        DisplayHome();
                        break;
                    case 4:
                        Player.player.ticket -= 5;
                        Console.WriteLine("대학시절 동기를 만나 최신동향 정보와 꿀팁을 공유합니다.");
                        Console.WriteLine("체력의 기운 획득 - 체력 +10");
                        Item.EquipItem(num);
                        Item.items[num].Have = true;
                        Console.ReadKey();
                        DisplayHome();
                        break;
                    case 5:
                        Player.player.ticket -= 5;
                        Console.WriteLine("랜덤채팅에서 이상한 사람을 만나 큰일날 뻔 했지만 겨우 도망쳤습니다.");
                        Console.WriteLine("나태의 기운 획득 - 모든 스텟 -10");
                        Item.EquipItem(num);
                        Item.items[num].Have = true;
                        Console.ReadKey();
                        DisplayHome();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;

                }
            }
        }
         
        public static void DisplayCompany()     //던전선택 화면 출력
        {
            Console.Clear();
            Console.WriteLine("출근하려는 회사를 선택하세요.");
            Console.WriteLine("1. 아르바이트 : 1티어 아이템 1개 이상 장착 권장");
            Console.WriteLine("2. 중소기업 : 모든 1티어 아이템 장착 권장");
            Console.WriteLine("3. 중견기업 : 모든 2티어 아이템 장착 권장");
            Console.WriteLine("4. 대기업 : 모든 3티어 아이템 장착 권장");
            Console.WriteLine("5. 글로벌기업 : 모든 4티어 아이템 장착 권장");
            Console.WriteLine("6. 히든 : 모든 최상위 아이템 장착 권장");
            Console.WriteLine();
            Console.WriteLine("0. 오늘은 집에서 쉬도록 하자!");

            int input = CheckInput(0, 6);

            switch (input)
            {
                case 0:
                    DisplayHome();
                    break;
                default:
                    Monster.Work(input);        //선택한 번호를 input으로 받아서 던전진행 함수 호출
                    break;

            }

        }

        public static int CheckInput(int min, int max)      //입력 숫자를 판단하는 함수
        {
            while (true)
            {
                string input = Console.ReadLine();      //입력받은 값을 input에 문자로 저장

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


   