using System.Diagnostics;

namespace HeroProgramm;
using System;


public static class GameLogic
{
    public static void initializePlayer(Hero Player)
    {
        Console.WriteLine("- Please type \"view\" to view your highscores. Otherwise just press enter.");
        string viewing = Console.ReadLine();
        if (viewing == "view")
        {
            Console.WriteLine("                            ** LEADERBOARD **\n" +
                              " Player ["+highScore.rogueScoreName+"] has obtained "+highScore.rogueScore+" points with the Rogue class\n" +
                              " Player ["+highScore.warriorScoreName+"] has obtained "+highScore.warriorScore+" points with the Warrior class\n" +
                              " Player ["+highScore.archerScoreName+"] has obtained "+highScore.archerScore+" points with the Archer class\n" +
                              " Player ["+highScore.mageScoreName+"] has obtained "+highScore.mageScore+" points with the Mage class\n" +
                              " Player ["+highScore.magicalArcherScoreName+"] has obtained "+highScore.magicalArcherScore+" points with a secret class\n");
        }
        int freeGuildStats = 10;
        Player.experience = 0;
        Player.prestige = 0;
        Player.gold = 500;
        Player.weapons = new List<Weapons>(); 
        bool lostGold = false; 
        bool foughtCopyCat = false; 
        bool savedElfWarrior = false; 
        bool foundPeoplesSword = false;
        bool tookOldMansChallenge = false;
        bool hallOfFame = false;
        obtainableWeapons.initializeWeaponList();
        allMonsters.initializeMonsterList();
        Console.Write("- Choose a hero name : ");
        Player.name = Console.ReadLine();
        Console.WriteLine("- For the upcoming choices, please type the number of the choice you want,\n" +
                          "  the first being  \"1\" and so on...");
        Console.WriteLine("- Choose one of the following classes :\n Rogue | Warrior | Archer | Mage");
        string heroClass = Console.ReadLine();
        switch (heroClass)
        {
            case "2":
                Player.heroClass = "Warrior";
                Player.hp = 10;
                Player.atkDamage = 3;
                Player.luck = 0;
                break;
            case "3":
                Player.heroClass = "Archer";
                Player.hp = 6;
                Player.atkDamage = 6;
                Player.luck = 3;
                break;
            case "4":
                Player.heroClass = "Mage";
                Player.hp = 9;
                Player.atkDamage = 4;
                Player.luck = 1;
                break;
            default:
                Player.heroClass = "Rogue";
                Player.hp = 7;
                Player.atkDamage = 5;
                Player.luck = 2;
                break;
        }
        Console.Clear();
        Console.WriteLine("- Welcome Adventurer! You are a brave hero fending off against the forces of chaos!\n" +
                          "  At the moment you are a weak and pitiful "+Player.heroClass+" in a very large world. \n" +
                          "  However, we trust in your determination to save the world from annihilation!\n"+ 
                          "  Greatest luck to you "+Player.name+", you'll be needing it!");
        Console.WriteLine("Tip?! Enter \"status\" at anytime to view your stats. Give it a try!");
        string a = Console.ReadLine();
        Console.Clear();
        if (a == "status")
        {
            Status(Player);
        }
        EnterWorldRift(Player);
    }

    public static void EnterWorldRift(Hero Player)
    {
        Console.Clear();
        string territory;
        Random rd = new Random();
        int randomNumber = rd.Next(0, 4);
        switch (randomNumber)
        {
            case 0:
                territory = "Dungeon";
                if (Player.heroClass == "Rogue")
                {
                    Console.WriteLine("- Right class, right place!");
                    Scenarios.StatPoint(Player,2);
                }

                if (Player.heroClass == "Mage")
                {
                    Console.WriteLine("- Wrong class, wrong place!");
                    Player.luck -= 1;
                    Console.WriteLine("- You have lost 1 luck...");
                }
                enteredDungeon(territory, Player);
                break;
            case 1:
                territory = "Kingdom";
                if (Player.heroClass == "Warrior")
                {
                    Console.WriteLine("- Right class, right place!");
                    Scenarios.StatPoint(Player,2);
                }
                if (Player.heroClass == "Archer")
                    Player.hp -= 1;
                enteredKingdom(territory, Player);
                break;
            case 2:
                territory = "Magic Tower";
                if (Player.heroClass == "Mage")
                {
                    Console.WriteLine("- Right class, right place!");
                    Scenarios.StatPoint(Player,2);
                }
                if (Player.heroClass == "Rogue")
                    Player.hp += 1;
                enteredMagicTower(territory, Player);
                break;
            default:
                territory = "Forest";
                if (Player.heroClass == "Archer")
                {
                    Console.WriteLine("- Right class, right place!");
                    Scenarios.StatPoint(Player,2);
                }
                if (Player.heroClass == "Warrior")
                    Player.hp -= 1;
                enteredForest(territory, Player);
                break;
        }
        
        
        
    }

    public static void enteredDungeon(string territory, Hero Player)
    {
        Console.WriteLine("- You have entered a "+territory+"...");
        Console.WriteLine("  It is dark. You hear creatures that might not be able to venture to the outer world\n"+
                          "  lurk. The dungeon is a dark and scary place. You may want to go enter for the loot, \n"+
                          "  but is it worth your life?\n"+
                          "  Do you wish to proceed? \n"+
                          "  yes | no");
        string firstAction = Console.ReadLine();
        switch (firstAction)
        {
            case "2":
                Console.Clear();
                Scenarios.EscapesDungeon(Player);
                break;
            case "status":
                Status(Player);
                enteredDungeon(territory, Player);
                break;
            //case 1
            default:
                Console.Clear();
                Scenarios.ConfrontsDungeon(Player);
                break;
        }
    }
    public static void enteredKingdom(string territory, Hero Player)
    {
        Console.WriteLine("- You have entered a "+territory+"...");
        
        Console.WriteLine("  The Kingdom is a marvelous place... on the outside. Will you uncover all\n"+
                          "  of its secrets and mysteries that it keeps purposely hidden? \n"+
                          "  Do you wish to check out :\n"+
                          "  The Adventurer's Guild | The Slums | The local Inn ");
        string firstAction = Console.ReadLine();
        switch (firstAction)
        {
            case "2":
                Console.Clear();
                Scenarios.EnteredSlums(Player);
                break;
            case "3":
                Console.Clear();
                Scenarios.EnteredInn(Player);
                break;
            case "status":
                Status(Player);
                enteredKingdom(territory, Player);
                break;
            //case 1
            default:
                Console.Clear();
                Scenarios.EnteredGuild(Player);
                break;
        }
    }
    public static void enteredMagicTower(string territory, Hero Player)
    {
        Console.WriteLine("- You have entered a "+territory+"...");
        Console.WriteLine("  The magic tower is an entity known to all but that very few understand.\n"+
                          "  It overflows with knowledge, power, and even more knowledge!\n");
        Console.WriteLine("  As you venture within the tower you become aware of your surroundings. WHat amazes\n"+
                          "  you the most is the whole texture and color of the tower. It seems and feels\n"+
                          "  like they exist and don't simultaneously...\n"+
                          "  You then decide to walk to an empty desk, an open book is laid on it. \n"+
                          "  The words \"SREVREP\" and \"AKAB\" fill both pages, however you see a side note where \n" +
                          "  \"Peeking is art not greed\" is written.\n"+
                          "  Do you wish to:\n"+
                          "  Take the book | Press on the word \"TREVREP\" | Press on the word \"AKAB\" ");
        string firstAction = Console.ReadLine();
        if (firstAction == "status")
        {
            Status(Player);
            firstAction = Console.ReadLine();
        }
        switch (firstAction)
        {
            case "2":
                Console.Clear();
                Scenarios.Pervert(Player);
                break;
            case "3":
                Console.Clear();
                Scenarios.Baka(Player);
                break;
            //case 1
            default:
                if (Player.hallOfFame)
                {
                    Console.WriteLine("- You have already entered [the hall of fame]");
                    enteredMagicTower("Magic Tower", Player);
                }
                else if (Player.experience>10 && Player.prestige>3)
                    Scenarios.ClickedOnTheBook(Player);
                else
                {
                    Console.WriteLine("- We admire your first for knowledge, however, it is too soon. You are \n"+
                                      "  not ready. You will have to deal with the consequences.\n"+
                                      "  You have lost all your current experience points and more...");
                    Player.experience = -1;
                    Player.name += " the book thief";
                    Status(Player);
                    Console.Clear();
                    enteredMagicTower(territory, Player);
                }
                break;
        }
    }
    public static void enteredForest(string territory, Hero Player)
    {
        Console.WriteLine("- You have entered a "+territory+"...");
        Console.WriteLine("- You are deep within the forest...\n"+
                          "  Once you enter, it is difficult to figure out your destination.\n"+
                          "  Would you like to go what you believe is :\n" +
                          "  Tip?! It is recommended you avoid the south.\n"+
                          "  North | South | East | West");
        string firstAction = Console.ReadLine();
        switch (firstAction)
        {
            case "2":
                Console.Clear();
                Scenarios.forestSouth(Player);
                break;
            case "3":
                Console.Clear();
                Scenarios.forestEast(Player);
                break;
            case "status":
                Status(Player);
                enteredForest(territory, Player);
                break;
            case "4":
                Console.Clear();
                Scenarios.forestWest(Player);
                break;
            //case 1
            default:
                Console.Clear();
                Scenarios.forestNorth(Player);
                break;
        }
    }
    
    public static void Status(Hero Player)
    {
        Console.WriteLine("** Name : "+Player.name+" **\n" +
                          "* Occupation : "+Player.heroClass+" *\n" +
                          "* Level : "+Player.level+" *\n" +
                          "* Experience : "+Player.experience+"/"+Level.MaxEXP +" *\n"+
                          "* HP : "+Player.hp+" *\n" +
                          "* ATK : "+Player.atkDamage+" *\n" +
                          "* Prestige : "+Player.prestige+" *\n" +
                          "* Gold : "+Player.gold+" *\n" +
                          "* Luck : "+Player.luck+" *");
        Console.Write("Your inventory consists of : ");
        if (Player.weapons.Count == 0)
        {
            Console.Write("nothing\n");
            Console.WriteLine("Press enter to resume...");
            Console.ReadLine();
        }
        else
        {
            foreach (var weapon in Player.weapons)
            {
                Console.Write(weapon.name+" | ");
            }
            Console.Write("\n");
            ShowEquipementStats(Player);
        }


    }
    
    public static void ShowEquipementStats(Hero Player)
    {
        Console.WriteLine("- If you enter the number which corresponds to the weapon's acquisition \n" +
                          "  you may vew it's stats or press enter to resume...");
        string value = Console.ReadLine();
        int number;
        bool success = int.TryParse(value, out number);
        if (success && number>0&&number<=Player.weapons.Count)
        {
            Console.Clear();
            Console.WriteLine("- "+Player.weapons[number-1].name + " is a "+Player.weapons[number-1].rarity+" grade weapon.\n" +
                              "  it has "+Player.weapons[number-1].hp+"hp "+Player.weapons[number-1].atk+"atk " +
                              +Player.weapons[number-1].luck+" luck and " +Player.weapons[number-1].prestige+" prestige " +
                              ".");
            ShowEquipementStats(Player);
        }
    }
}

