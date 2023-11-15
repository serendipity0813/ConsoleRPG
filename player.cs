using System;
using static ConsoleRPG.ConsoleRPG;


namespace ConsoleRPG
{

    //인터페이스, 클래스 등

    public interface ICharter       //플레이어 인터페이스 생성 
    {
        string Name { get; }
        int Health { get; }
        int Attack { get; }
        int Defend { get; }
        bool IsDead { get; }
        void TakeDamage(int damage);
    }
    public class Player : ICharter      //플레이어 캐릭터 클래스
    {
        //캐릭터 클래스 필드
        public string Name { get; }     //저장 후 변동 없어 프로퍼티는 get으로만 설정
        public string Job { get; }      
        public int Level { get; set; }      //지속적으로 변경되어 프로퍼티 - get, set
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defend { get; set; }
        public int Money { get; set; }
        public int exp = 0;                 //플레이어 경험치 - 일정수치 이상 획득시 레벨업
        public int ticket = 0;              //던전 진행시 얻는 아이템
        public bool IsDead => Health <= 0;    //플레이어 체력이 0이하로 떨어지면 true값으로 전환 
        public static Player player;          //플레이어 변수 선언


        //플레이어 클래스 속성
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

        public void TakeDamage(int damage)      //던전 진행시 몬스터에게 데미지를 받는 메소드
        {
            Health -= (damage - player.Defend);     //몬스터 공격력 - 플레이어 방어력 만큼 체력 감소
            if (IsDead) Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else Console.WriteLine($"{Name}이(가) {damage-Defend}의 데미지를 받았습니다. 남은 체력: {Health}");
        }

        public static int GetBonusAttack()      //현재 장착한 아이템 공격력 합 만큼 반환 
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip)
                    sum += Item.items[i].Attack;
            }
            return sum;
        }

        public static int GetBonusDefend()      //방어력 합산 반환
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip)
                    sum += Item.items[i].Defend;
            }
            return sum;
        }

        public static int GetBonusHealth()      //체력 합산 반환
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (Item.items[i].Equip)
                    sum += Item.items[i].Health;
            }

            return sum;
        }


    }

}
     


   



