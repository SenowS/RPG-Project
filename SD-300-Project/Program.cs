using System;
using System.Collections.Generic;

//Hero Creation
Console.WriteLine("Welcome to my RPG!");
Console.WriteLine("Please choose a name for your hero.");
string HeroName = Console.ReadLine();
Hero MainHero = new Hero(HeroName);
int wins = 0;
int loss = 0;
int games = 0;

//Main Menu Call
while (true)
{
    MainMenu();
}

// The main body of the game
// It calls on all the main functions to hold together the program and is controlled by a switch case which takes in user input
// Runs on a while loop so that it will not close randomly unless it is broken by the user
void MainMenu()
{
    Console.Clear();
    Console.WriteLine("MAIN MENU");
    Console.WriteLine("Statistics = 1");
    Console.WriteLine("Display Inventory = 2");
    Console.WriteLine("To fight something = 3");
    Console.WriteLine("To leave = CTRL + C");

    int menuSelection = Convert.ToInt32(Console.ReadLine());
    MenuSelect(menuSelection);

    void MenuSelect(int menuSelection)
    {

        switch (menuSelection)
        {
            case 1:
                StatisticsPage();
                break;

            case 2:
                InventoryPage();
                break;

            case 3:
                BossFightPage();
                break;

            default:
                Console.Clear();
                MainMenu();
                break;
        }
    }
}

// The display page for everything statistics
// uses an if/else with user input to decide if you are going to the main menu or not
void StatisticsPage()
{
    int winCount = wins;
    int lossCount = loss;
    int gamesCount = games;

    Console.Clear();
    Console.WriteLine("Statistics Page");
    Console.WriteLine($"Number of fights won: {winCount}");
    Console.WriteLine($"Number of fights lost: {lossCount}");
    Console.WriteLine($"Number of games played: {gamesCount}");
    Console.WriteLine("Would you like to return to the main menu?");
    Console.WriteLine("1 for Yes and 2 for No");
    int returnMenu = Convert.ToInt32(Console.ReadLine());
    if (returnMenu == 1)
    {
        MainMenu();
    }
    else
    {
        StatisticsPage();
    }
}

// The display page for everything inventory related
// Uses switch cases to decided where to send you

void InventoryPage()
{
    Console.Clear();
    Console.WriteLine("Inventory Page");
    Console.WriteLine("Would you like to change your weapon or armor?");
    Console.WriteLine("For Weapon changes choose 1.");
    Console.WriteLine("For Armor changes choose 2");
    Console.WriteLine("To see your current stats choose 3");
    Console.WriteLine("Would you like to return to the main menu?");
    Console.WriteLine("4 for Yes and 5 for No");
    int menuOptions = Convert.ToInt32(Console.ReadLine());

    switch (menuOptions)
    {
        case 1:
            WeaponPage();
            break;

        case 2:
            ArmorPage();
            break;

        case 3:
            StatsPage();
            break;

        case 4:
            MainMenu();
            break;

        case 5:
            InventoryPage();
            break;

        default:
            Console.WriteLine("Unknown value");
            break;
    }
}

// Weapon page gets call in the inventory page and uses the Hero method: EquipWeapon to change what weapon is equipped
// Passes in the user input for the functions switch case
void WeaponPage()
{
    Console.Clear();
    Console.WriteLine("Weapon Page");
    Console.WriteLine("Which weapon would you like to switch to?");
    Console.WriteLine("Choose 1 for the sword.");
    Console.WriteLine("Choose 2 for the bow.");
    Console.WriteLine("Choose 3 for the staff");
    Console.WriteLine("Choose 4 for your fists");
    int weaponOptions = Convert.ToInt32(Console.ReadLine());
    MainHero.EquipWeapon(weaponOptions);
    InventoryPage();
}

// Armor page is called from the inventory page and calls on the Hero method: EquipArmor to change what armor is currently equipped
// Takes user input and uses it for the Equip switch case
void ArmorPage()
{
    Console.Clear();
    Console.WriteLine("Armor Page");
    Console.WriteLine("Which armor would you like to switch to?");
    Console.WriteLine("Choose 1 for the Chainmail.");
    Console.WriteLine("Choose 2 for the Cloak.");
    Console.WriteLine("Choose 3 for the Leather Armor");
    Console.WriteLine("Choose 4 for your Robes");
    int armorOptions = Convert.ToInt32(Console.ReadLine());
    MainHero.EquipArmor(armorOptions);
    InventoryPage();
}

// The stats page function is called on from the inventory page and pulls functions from the Hero class to display the stats used for the screen
void StatsPage()
{
    Console.Clear();
    MainHero.ShowStats();
    Console.WriteLine("");
    MainHero.ShowInventory();
    Console.WriteLine("");
    Console.WriteLine("Would you like to return to the main menu or back to the inventory page?");
    Console.WriteLine("1 for Main Menu or 2 for Inventory Page");
    int returnMenu = Convert.ToInt32(Console.ReadLine());
    if (returnMenu == 1)
    {
        MainMenu();
    }
    else if (returnMenu == 2)
    {
        InventoryPage();
    }
    else
    {
        StatsPage();
    }
}

// This function handles all of the boss fight
// It takes in many of the properties offered by other classes and uses them to simulate the battle
// Once a boss is defeated it is removed from the list
// Once one is lost it will call the reAdd(); function to refill the list
void BossFightPage()
{
    var random = new Random();
    var list = BossCollection.BossSelection;
    int BossNumber = random.Next(list.Count);
    int HeroAttack = MainHero.BaseStrength + MainHero.EquippedWeapon.WeaponPower;
    int HeroDefence = MainHero.BaseDefence + MainHero.EquippedArmor.ArmorDefense;
    int HeroDamage = HeroAttack - list[BossNumber].Defence;
    int BossDamage = list[BossNumber].Strength - HeroDefence;
    int HeroHealth = MainHero.CurrentHealth;
    int BossHealth = list[BossNumber].CurrentHealth;

    Console.Clear();
    Console.WriteLine("Boss Page");
    Console.WriteLine(list[BossNumber].Name);
    list[BossNumber].BossStats();

    Console.WriteLine("1 for Yes and 2 for No");
    int returnMenu = Convert.ToInt32(Console.ReadLine());
    if (returnMenu == 1)
    {
        Console.Clear();
        Console.WriteLine("Fight has begun!");

        while (BossHealth > 0 | HeroHealth > 0)
        {
            Console.WriteLine($"{list[BossNumber].Name} just hit {MainHero.Name} for {BossDamage}");
            HeroHealth -= BossDamage;
            Console.WriteLine($"{MainHero.Name} lost {BossDamage} health");

            //checks if the hero has been defeated or not
            //boss will always do the first hit
            if (HeroHealth < 0)
            {
                reAdd();
                loss++;
                games++;
                Console.WriteLine("Oh no you died.");
                Console.WriteLine("Would you like to return to the main menu? Or head over to the statistics page? Or fight another boss?");
                Console.WriteLine("1 for the main menu | 2 for the statistics page | 3 for another boss fight");
                int lostMenu = Convert.ToInt32(Console.ReadLine());
                if (lostMenu == 1)
                {
                    MainMenu();
                }
                else if (lostMenu == 2)
                {
                    StatisticsPage();
                }
                else if (lostMenu == 3)
                {
                    BossFightPage();
                }
            }

            Console.WriteLine($"{MainHero.Name} attacks for {HeroDamage}");
            BossHealth -= HeroDamage;
            Console.WriteLine($"{list[BossNumber].Name} Lost {HeroDamage}");

            //Checks if the boss has been defeated or not
            //Player will always attack second
            if (BossHealth < 0)
            {
                Boss currentBoss = list[BossNumber];
                wins++;
                games++;
                list.Remove(currentBoss);
                if (list.Count == 0)
                {
                    reAdd();
                }
                Console.WriteLine("Oh You Won!");
                Console.WriteLine("Would you like to return to the main menu? Or head over to the statistics page? Or fight another boss?");
                Console.WriteLine("1 for the main menu | 2 for the statistics page | 3 for another boss fight");
                int wonMenu = Convert.ToInt32(Console.ReadLine());
                if (wonMenu == 1)
                {
                    MainMenu();
                }
                else if (wonMenu == 2)
                {
                    StatisticsPage();
                }
                else if (wonMenu == 3)
                {
                    BossFightPage();
                }
            }
        }
    }
    else if (returnMenu == 2)
    {
        MainMenu();
    }
    else
    {
        BossFightPage();
    }
}

//Called when you lose a fight in the boss battles
//Adds on more instances of the previously made bosses. Will not add more of the two bosses set out to kill

void reAdd()
{
    BossCollection.BossSelection.Add(new Boss("Tyler", 33, 30, 200, 200));
    BossCollection.BossSelection.Add(new Boss("Alec", 37, 15, 125, 125));
    BossCollection.BossSelection.Add(new Boss("Sam", 38, 25, 175, 175));
    BossCollection.BossSelection.Add(new Boss("Keat", 46, 20, 250, 250));
    BossCollection.BossSelection.Add(new Boss("Kyle", 32, 15, 100, 100));
    BossCollection.BossSelection.Add(new Boss("Eric", 26, 5, 75, 75));
}
class Hero
{
    public string Name { get; set; }
    public int BaseStrength { get; set; } = 5;
    public int BaseDefence { get; set; } = 5;
    public int OriginalHealth { get; set; } = 500;
    public int CurrentHealth = 500;

    public Weapon EquippedWeapon = WeaponList.WeaponSelection[0];
    public Armor EquippedArmor = ArmorList.ArmorSelection[0];

    public List<int> WeaponOptions { get; set; }
    public List<int> ArmorOptions { get; set; }
    public Hero(string name)
    {
        Name = name;
    }

    //Called in the inventory page to map out all the basic stats without any sort of boost
    public void ShowStats()
    {
        Console.WriteLine($"{Name}'s Stats:");
        Console.WriteLine($"Your Base Strength is: {BaseStrength}");
        Console.WriteLine($"Your Base Defence is: {BaseDefence}");
        Console.WriteLine($"Your Original Health is: {OriginalHealth}");
        Console.WriteLine($"Your Current Health is: {CurrentHealth}");
    }

    // Used in the inventory screen to display your current stats and what additional bonuses are being received
    // set as void because it doesn't need to return anything
    public void ShowInventory()
    {
        Console.WriteLine("Your items you currently have are:");
        Console.WriteLine($"Your current weapon is: {EquippedWeapon.WeaponName}.");
        Console.WriteLine($"It offers an extra: {EquippedWeapon.WeaponPower} damage!");
        Console.WriteLine($"Your current armor is: {EquippedArmor.ArmorName}.");
        Console.WriteLine($"It offers an extra: {EquippedArmor.ArmorDefense} protection!");

    }

    // Takes in a key number using console read line in the inventory page
    // pass it into a switch case to determine which weapon to swap to
    public void EquipWeapon(int weaponOptions)
    {
        switch (weaponOptions)
        {
            case 1:
                EquippedWeapon = WeaponList.WeaponSelection[1];
                //EquppedWeapon.Power;
                break;

            case 2:
                EquippedWeapon = WeaponList.WeaponSelection[2];
                break;

            case 3:
                EquippedWeapon = WeaponList.WeaponSelection[3];
                break;

            case 4:
                EquippedWeapon = WeaponList.WeaponSelection[0];
                break;

            default:
                Console.WriteLine("Unknown value");
                break;
        }
    }

    //Takes in a key number from the inventory screen and uses that as an arguement 
    // Passes it into a switch case to determine which weapon to equip to your character
    public void EquipArmor(int armorOptions)
    {
        switch (armorOptions)
        {
            case 1:
                EquippedArmor = ArmorList.ArmorSelection[1];
                //EquppedWeapon.Power;
                break;

            case 2:
                EquippedArmor = ArmorList.ArmorSelection[2];
                break;

            case 3:
                EquippedArmor = ArmorList.ArmorSelection[3];
                break;

            case 4:
                EquippedArmor = ArmorList.ArmorSelection[0];
                break;

            default:
                Console.WriteLine("Unknown value");
                break;
        }
    }
}

class Boss
{
    public string Name { get; set; }
    public int Strength { get; set; }
    public int Defence { get; set; }
    public int OriginalHealth { get; set; }
    public int CurrentHealth { get; set; }
    public Boss(string name, int strength, int defence, int health, int currentHealth)
    {
        Name = name;
        Strength = strength;
        Defence = defence;
        OriginalHealth = health;
        CurrentHealth = currentHealth;
    }

    // This will use console writeline to take all the individual values and push them out
    // Used in the boss fight screen to give a preview of the fight
    public void BossStats()
    {
        Console.WriteLine($"Boss is: {Name}");
        Console.WriteLine($"Boss Strength is: {Strength}");
        Console.WriteLine($"Boss Defence is: {Defence}");
        Console.WriteLine($"Boss Health is: {OriginalHealth}");
        Console.WriteLine($"Boss Current Health is: {CurrentHealth}");
    }
}

class BossCollection
{
    public static List<Boss> BossSelection = new List<Boss>
        {
            new Boss ( "Tyler", 33, 30, 200, 200 ),
            new Boss ( "Alec", 37, 15, 125, 125 ),
            new Boss ( "Sam", 38, 25, 175, 175 ),
            new Boss ( "Keat", 46, 20, 250, 250 ),
            new Boss ( "Kyle", 32, 15, 100, 100),
            new Boss ( "Eric", 26, 5, 75, 75),
            new Boss ( "Noah", 5000, 4000, 25000, 25000 ),
            new Boss ( "Sasha", 5000, 4000, 25000, 25000 ),
        };
}

public class Weapon
{
    public Weapon(string name, int power)
    {
        WeaponName = name;
        WeaponPower = power;
    }
    public string WeaponName { get; set; }
    public int WeaponPower { get; set; }
}
public class Armor
{
    public Armor(string name, int defense)
    {
        ArmorName = name;
        ArmorDefense = defense;
    }
    public string ArmorName { get; set; }
    public int ArmorDefense { get; set; }
}

static class WeaponList
{
    public static List<Weapon> WeaponSelection = new List<Weapon>
        {
            new Weapon ( "Fists", 10 ),
            new Weapon ( "Sword", 40 ),
            new Weapon ( "Bow", 30 ),
            new Weapon ( "Staff", 25 )
        };
}

static class ArmorList
{
    public static List<Armor> ArmorSelection = new List<Armor>
        {
            new Armor ( "Robes", 10 ),
            new Armor ( "Chainmail", 35 ),
            new Armor ( "Cloak", 15 ),
            new Armor ( "Leather Armor", 20 )
        };
}