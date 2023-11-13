using System;
using System.Collections.Generic;

internal class Program
{
    private static Character player;
    private static List<Item> inventory;

    static void Main(string[] args)
    {
        GameDataSetting();
        DisplayGameIntro();
    }

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);

        // 아이템 정보 세팅
        inventory = new List<Item>
        {
            new Item("검낡은 검", ItemType.Weapon, 5, 0, 50,"쉽게 볼 수 있는 낡은 검입니다."),
            new Item("무쇠갑옷", ItemType.Armor, 0, 5, 30,"무쇠로 만들어져 튼튼한 갑옷입니다."),
            new Item("녹을 먹은 검", ItemType.Weapon, 5, 0,10,"누군가의 유품처럼 느껴진다."),
            new Item("도끼", ItemType.Weapon, 8, 0, 70, "쉽게 볼 수 있는 도끼입니다."),
            new Item("플레이트 아머", ItemType.Armor, 0, 8, 100, "누구나 하나쯤 집에 있는 갑옷이다"),
            new Item("회손이 심한 단검", ItemType.Weapon, 3, 0, 100, "누군가의 유품처럼 느껴진다."),
            new Item("가고일 가죽 갑옷", ItemType.Armor, 0, 3, 150, "옆에 소가죽이라고 젹혀있다"),
            new Item("빨간 물약",ItemType.Potion,0,0,100,"하루 1잔이 권장량이고 적혀있다." )

        };
    }

    static void DisplayGameIntro()
    {
        Console.Clear();

        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckValidInput(1, 2);
        switch (input)
        {
            case 1:
                DisplayMyInfo();
                break;

            case 2:
                DisplayInventory();
                break;
        }
    }

    static void DisplayMyInfo()
    {
        Console.Clear();

        Console.WriteLine("상태보기");
        Console.WriteLine("캐릭터의 정보를 표시합니다.");
        Console.WriteLine();
        Console.WriteLine($"Lv.{player.Level}");
        Console.WriteLine($"{player.Name}({player.Job})");
        Console.WriteLine($"공격력 :{player.Atk}");
        Console.WriteLine($"방어력 : {player.Def}");
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
        Console.WriteLine("아이템을 표시합니다.");
        Console.WriteLine();

        int index = 1;
        foreach (var item in inventory)
        {
            string equippedStatus = IsEquipped(item) ? "[E] " : "";
            Console.WriteLine($"{index}. {equippedStatus}{item.Name} ({item.Type}) - 공격력: {item.Atk}, 방어력: {item.Def}");
            index++;
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("원하는 아이템을 선택해주세요.");

        int input = CheckValidInput(0, index - 1);
        if (input == 0)
        {
            DisplayGameIntro();
        }
        else
        {
            EquipItem(input - 1);
        }
    }

    static void EquipItem(int itemIndex)
    {
        Item selected = inventory[itemIndex];

        // 이미 장착 중이면 해제, 아니면 장착
        if (IsEquipped(selected))
        {
            UnequipItem(selected);
        }
        else
        {
            Equip(selected);
        }

        Console.WriteLine("인벤토리로 돌아가려면 아무 키나 눌러주세요.");
        Console.ReadKey();
        DisplayInventory();
    }

    static void Equip(Item item)
    {
        player.Atk += item.Atk;
        player.Def += item.Def;
        player.EquippedItems.Add(item);

        Console.WriteLine($"{item.Name}을(를) 장착했습니다.");
        Console.WriteLine($"새로운 능력치 - 공격력: {player.Atk}, 방어력: {player.Def}");
    }

    static void UnequipItem(Item item)
    {
        player.Atk -= item.Atk;
        player.Def -= item.Def;
        player.EquippedItems.Remove(item);

        Console.WriteLine($"{item.Name}을(를) 해제했습니다.");
        Console.WriteLine($"새로운 능력치 - 공격력: {player.Atk}, 방어력: {player.Def}");
    }

    static bool IsEquipped(Item item)
    {
        return player.EquippedItems.Contains(item);
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

            Console.WriteLine("잘못된 입력입니다.");
        }
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Potion
    // 다른 아이템 타입 추가 가능
}

public class Item
{
    public string Name { get; }
    public ItemType Type { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Gold { get; }
    public string Explanation { get; }

    public Item(string name, ItemType type, int atk, int def, int gold, string explanation)
    {
        Name = name;
        Type = type;
        Atk = atk;
        Def = def;
        Gold = gold;
        Explanation = explanation;
    }
}

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; }
    public int Gold { get; set; }
    public List<Item> EquippedItems { get; } = new List<Item>();

    public Character(string name, string job, int level, int atk, int def, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }
}