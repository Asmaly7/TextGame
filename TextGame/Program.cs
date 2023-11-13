using static TextGame.Program;

namespace TextGame
{
    internal class Program
    {
        private static Character player;
        private static List<Weapon> weaponsInventory = new List<Weapon>(); // 무기 목록
        private static List<Armor> armorsInventory = new List<Armor>(); // 방어구 목록

        private static Weapon equippedWeapon; // 현재 장착된 무기
        private static Armor equippedArmor; // 현재 장착된 방어구

        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; set; }
            public int Atk { get; set; }
            public int Def { get; set; }
            public int Hp { get; set; }
            public int Gold { get; set; }

            public int MaxHp { get; set; }



            public Character(string name, string job, int level, int atk, int def, int hp, int gold, int maxHp)
            {
                Name = name;
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
                MaxHp = maxHp;
            }
        }
        public class Monster
        {
            public string Name { get; }
            public int Level { get; }
            public int Atk { get; }
            public int Def { get; }
            public int Hp { get; set; }

            public Monster(string name, int level, int atk, int def, int hp)
            {
                Name = name;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
            }

            public int Attack()
            {
                // 간단한 공격 로직 구현
                Random random = new Random();
                int damage = random.Next(Atk / 2, Atk);
                return damage;
            }
        }
        public class Item
        {
            public string Name { get; }
            public string Type { get; }
            public int StatModifier { get; }
            public string Description { get; }
            public string EffectDescription { get; }  

            public Item(string name, string type, int statModifier, string description, string effectDescription)
            {
                Name = name;
                Type = type;
                StatModifier = statModifier;
                Description = description;
                EffectDescription = effectDescription;
            }

            public override string ToString()
            {
                return $"{Name} | {Type} +{StatModifier} | {Description}";
            }

            public string GetItemInfo()
            {
                return $"{Name} | {Type} +{StatModifier} | {Description} | {EffectDescription}";  
            }
        }

        public class Weapon : Item
        {
            public Weapon(string name, int atkModifier, string description, string effectDescription)
                : base(name, "공격력", atkModifier, description, effectDescription)
            {
            }
        }

        public class Armor : Item
        {
            public Armor(string name, int defModifier, string description, string effectDescription)
                : base(name, "방어력", defModifier, description, effectDescription)
            {
            }
        }


        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("임꺽정", "전사", 1, 10, 5, 500, 1000, 500);

            // 아이템 정보 세팅
            weaponsInventory.Add(new Weapon("낡은 검", 20, "흔히 볼 수 있는 검", "공격력이 20증가합니다."));
            armorsInventory.Add(new Armor("무쇠갑옷", 5, "무쇠로 만들어져 튼튼한 갑옷입니다","방어력이 5 증가합니다."));


        }



                     


        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("아스말리 텍스트 게임에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태정보창");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 마을로가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DisplayInventory();
                    break;

                case 3:
                    DisplayTown();
                    break;
            }
        }

        static string GetEquippedItemStat(Item equippedItem)
        {
            if (equippedItem != null)
            {
                return (equippedItem != null) ? $"(+{equippedItem.StatModifier})" : "";
            }
            return ""; 
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 : {player.Atk}  {GetEquippedItemStat(equippedWeapon)}");
            Console.WriteLine($"방어력 : {player.Def}  {GetEquippedItemStat(equippedArmor)}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유한 무기 목록:");
            for (int i = 0; i < weaponsInventory.Count; i++)
            {
                string equippedIndicator = (weaponsInventory[i] == equippedWeapon) ? " (E)" : "";
                Console.WriteLine($"{i + 1}. {weaponsInventory[i]}{equippedIndicator}");
            }

            Console.WriteLine();
            Console.WriteLine("보유한 방어구 목록:");
            for (int i = 0; i < armorsInventory.Count; i++)
            {
                string equippedIndicator = (armorsInventory[i] == equippedArmor) ? " (E)" : "";
                Console.WriteLine($"{i + 1 + weaponsInventory.Count}. {armorsInventory[i]}{equippedIndicator}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, weaponsInventory.Count + armorsInventory.Count);
            if (input == 0)
            {
                DisplayGameIntro();
            }
            else if (input <= weaponsInventory.Count)
            {
                EquipWeapon(input - 1);
            }
            else
            {
                EquipArmor(input - weaponsInventory.Count - 1);
            }
        }

        static void EquipOrUnequipWeapon(int index)
        {
            Weapon selectedWeapon = weaponsInventory[index];
            if (selectedWeapon == equippedWeapon)
            {
                Console.WriteLine($"{selectedWeapon} 무기를 해제합니다.");
                equippedWeapon = null;
            }
            else
            {
                Console.WriteLine($"{selectedWeapon} 무기를 장착합니다.");
                equippedWeapon = selectedWeapon;
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            DisplayInventory();
        }

        static void EquipOrUnequipArmor(int index)
        {
            Armor selectedArmor = armorsInventory[index];
            if (selectedArmor == equippedArmor)
            {
                Console.WriteLine($"{selectedArmor.Name} 방어구를 해제합니다.");
                equippedArmor = null;
            }
            else
            {
                Console.WriteLine($"{selectedArmor.Name} 방어구를 장착합니다.");
                equippedArmor = selectedArmor;
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            DisplayInventory();
        }

        static void EquipWeapon(int index)
        {
            Weapon selectedWeapon = weaponsInventory[index];
            if (selectedWeapon != equippedWeapon)
            {
                Console.WriteLine($"무기 {selectedWeapon.Name}을(를) 장착합니다.");
                Console.WriteLine($"능력치가 증가했습니다: {selectedWeapon.EffectDescription}");

                equippedWeapon = selectedWeapon;
                UpdatePlayerStats(selectedWeapon.StatModifier);
            }
            else
            {
                Console.WriteLine($"무기 {selectedWeapon.Name}을(를) 이미 장착 중입니다.");
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            DisplayInventory();
        }

        static void EquipArmor(int index)
        {
            Armor selectedArmor = armorsInventory[index];
            if (selectedArmor != equippedArmor)
            {
                Console.WriteLine($"방어구 {selectedArmor.Name}을(를) 장착합니다.");
                Console.WriteLine($"능력치가 증가했습니다: {selectedArmor.EffectDescription}");
                equippedArmor = selectedArmor;
                UpdatePlayerStats(selectedArmor.StatModifier);
            }
            else
            {
                Console.WriteLine($"방어구 {selectedArmor.Name}을(를) 이미 장착 중입니다.");
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            DisplayInventory();
        }

        // 플레이어의 능력치 업데이트 메서드
        static void UpdatePlayerStats(int statModifier)
        {
            player.Atk += statModifier;

            // 방어구를 장착하면 최대 체력 및 현재 체력도 증가
            player.Def += statModifier;
            player.MaxHp += statModifier;
            player.Hp += statModifier;
        }







        static void DisplayTown()
        {
            Console.Clear();

            Console.WriteLine("마을에 오신 것을 환영합니다!");
            Console.WriteLine("이곳에서 여러 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 여관 : 체력을 회복합니다.");
            Console.WriteLine("2. 상점 : 장비 구매가 가능합니다.");
            Console.WriteLine("3. 던전 : 골드와 경험치를 획득하세요");
            Console.WriteLine("4. 인벤토리 : 현재 보유한 아이템을 확인합니다.");
            Console.WriteLine();
            Console.WriteLine("원하시는 장소의 번호를 입력해주세요.");

            int input = CheckValidInput(1, 4);
            switch (input)
            {
                case 1:
                    VisitInn();
                    break;

                case 2:
                    VisitShop();
                    break;

                case 3:
                    EnterDungeon();
                    break;
                case 4:
                    DisplayInventory();
                    break;
            }
        }

        static void VisitInn()
        {
            Console.Clear();

            Console.WriteLine("여관에 오신 걸 환영합니다.");
            Console.WriteLine("현재 체력 상태: " + player.Hp + "/" + player.MaxHp);
            Console.WriteLine("체력을 회복하시겠습니까?");
            Console.WriteLine("1. 체력 회복하기");
            Console.WriteLine("2. 떠나기");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    RestoreHealth();
                    break;

                case 2:
                    DisplayTown();
                    break;
            }
        }

        static void RestoreHealth()
        {
            int MaxHp = 1000;
            // 여관에서 체력 회복 로직을 추가합니다.
            int healingAmount = MaxHp; 

            Console.WriteLine($"체력이 가득 회복되었습니다.");
            player.Hp += healingAmount;

            // 최대 체력을 초과하지 않도록 조정합니다.
            if (player.Hp > player.MaxHp)
            {
                player.Hp = player.MaxHp;
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            VisitInn();
        }

        static void VisitShop()
        {
            Console.Clear();
            Console.WriteLine("상점에 오신 것을 환영합니다.");
            Console.WriteLine("다양한 아이템을 구매할 수 있습니다.");

            DisplayShopInventory(); 

            Console.WriteLine("0. 나가기");
            int input = CheckValidInput(0, weaponsShop.Count + armorsShop.Count);

            if (input == 0)
            {
                DisplayTown();
            }
            else if (input <= weaponsShop.Count)
            {
                BuyItem(weaponsShop, input - 1);
            }
            else
            {
                BuyItem(armorsShop, input - weaponsShop.Count - 1);
            }
        }

            static List<string> weaponsShop = new List<string> { "수호자 장검", "코가막힌 대검", "기가막힌 대검" };
            static List<string> armorsShop = new List<string> { "수호자 방패", "코가막힌 갑옷세트", "기가막힌 갑옷세트" };
            static Dictionary<string, int> itemPrices = new Dictionary<string, int>
            {
            { "수호자 장검", 500 },
            { "코가막힌 대검", 1000 },
             { "기가막힌 대검", 2000},
            { "수호자 방패", 500 },
            { "코가막힌 갑옷세트", 1000 },
            { "기가막힌 갑옷세트", 2000 }
            };

        static void DisplayShopInventory()
        {
            Console.WriteLine("무기 목록:");
            for (int i = 0; i < weaponsShop.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weaponsShop[i]} - {itemPrices[weaponsShop[i]]} G");
            }

            Console.WriteLine();
            Console.WriteLine("방어구 목록:");
            for (int i = 0; i < armorsShop.Count; i++)
            {
                Console.WriteLine($"{i + 1 + weaponsShop.Count}. {armorsShop[i]} - {itemPrices[armorsShop[i]]} G");
            }
        }

        static void BuyItem(List<string> shopInventory, int index)
        {
            string selectedItemName = shopInventory[index];

            // 아이템이 이미 인벤토리에 있는지 확인
            if (weaponsInventory.Any(weapon => weapon.Name == selectedItemName) || armorsInventory.Any(armor => armor.Name == selectedItemName))
            {
                Console.WriteLine("이미 해당 아이템을 보유 중입니다.");
            }
            else
            {
                int itemPrice = itemPrices[selectedItemName];

                // 골드가 충분한지 확인
                if (player.Gold >= itemPrice)
                {
                    // 아이템을 구매하고 골드 차감
                    Console.WriteLine($"{selectedItemName}을(를) 구매했습니다.");
                    player.Gold -= itemPrice;

                    // 구매한 아이템을 인벤토리에 추가
                    if (weaponsShop.Contains(selectedItemName))
                    {
                        int atkModifier;
                        string description;

                        if (selectedItemName == "수호자 장검")
                        {
                            atkModifier = 5;
                            description = "+공격력5";
                        }
                        else if (selectedItemName == "코가막힌 대검")
                        {
                            atkModifier = 10;
                            description = "+공격력10";
                        }
                        else if (selectedItemName == "기가막힌 대검")
                        {
                            atkModifier = 20;
                            description = "+공격력20";
                        }
                        else
                        {
                            // 예외 처리 또는 기본 값 설정
                            atkModifier = 0;
                            description = "일반";
                        }

                        weaponsInventory.Add(new Weapon(selectedItemName, atkModifier, description, "효과설명"));
                    }
                    if (armorsShop.Contains(selectedItemName))
                    {
                        int defModifier;
                        string description;

                        if (selectedItemName == "수호자 방패")
                        {
                            defModifier = 5;
                            description = "+방어력 5";
                        }
                        else if (selectedItemName == "코가막힌 갑옷세트")
                        {
                            defModifier = 10;
                            description = "+방어력 10";
                        }
                        else if (selectedItemName == "기가막힌 갑옷세트")
                        {
                            defModifier = 20;
                            description = "+방어력 20";
                        }
                        else
                        {
                            // 예외 처리 또는 기본 값 설정
                            defModifier = 0;
                            description = "일반";
                        }

                        armorsInventory.Add(new Armor(selectedItemName, defModifier, description, "효과 설명"));
                    }

                }
                else
                {
                    Console.WriteLine("골드가 부족하여 구매할 수 없습니다.");
                }


            }   

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            VisitShop();
        }



        static void EnterDungeon()
        {
            Console.Clear();
            Console.WriteLine("던전에 입장합니다.");
            Console.WriteLine("C#을잡아 C# 보스에 도전하세요!");
            Console.WriteLine("1. C# 잡몹");
            Console.WriteLine("2. C# 보스");
            Console.WriteLine("3. 던전 나가기");

            int input = CheckValidInput(1, 3);

            if (input == 1)
            {
                Battle(new Monster("C# 잡몹", 1, 20, 5, 100));
            }
            else if (input == 2)
            {
                Battle(new Monster("C# 보스", 5, 100, 10, 1000));
            }
            else if (input == 3)
            {
                DisplayTown();
                return;
            }

            Console.WriteLine("아무 키나 누르면 계속...");
            Console.ReadKey();
            EnterDungeon(); // 다시 던전으로 돌아가기
        }

        static void Battle(Monster monster)
        {
            Console.WriteLine($"{monster.Name}과의 전투가 시작됩니다!");

            while (player.Hp > 0 && monster.Hp > 0)
            {
                // 플레이어의 턴
                Console.WriteLine($"당신의 턴 - {player.Name}의 체력: {player.Hp}, {monster.Name}의 체력: {monster.Hp}");
                Console.WriteLine("1. 공격");

                int choice = CheckValidInput(1, 1);

                if (choice == 1)
                {
                    int damage = player.Atk;
                    monster.Hp -= damage;
                    Console.WriteLine($"당신이 {monster.Name}에게 {damage}의 피해를 입혔습니다.");
                }

                // 몬스터의 턴
                if (monster.Hp > 0)
                {
                    int damage = monster.Attack();
                    player.Hp -= damage;
                    Console.WriteLine($"{monster.Name}이 당신에게 {damage}의 피해를 입혔습니다.");
                }
            }

            if (player.Hp <= 0)
            {
                Console.WriteLine("전투에서 패배했습니다...");
                // 패배 처리 또는 게임 종료 로직을 추가하세요.
            }
            else
            {
                Console.WriteLine($"{monster.Name}을(를) 처치했습니다! 경험치와 골드를 획득합니다.");

                // 경험치 및 골드 획득 로직 추가
                player.Gold += monster.Level * 100;
                player.Level += 1;
                player.MaxHp += 20;
                player.Hp = player.MaxHp;

                Console.WriteLine($"현재 레벨: {player.Level}, 골드: {player.Gold}, 최대 체력: {player.MaxHp}");

                if (monster.Name == "C# 보스")
                {
                    Console.WriteLine("C# 보스를 무찌르셨습니다! 당신은 이제 C# 마스터!!!");
                    
                }
            }
        }




        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다 다시선택해주세요.");
            }
        }
    }


    
}