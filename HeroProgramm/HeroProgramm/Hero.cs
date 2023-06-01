using System.Security.Cryptography;

namespace HeroProgramm;

public class Hero
{
    public string name;
    public string heroClass;
    public int hp;
    public int atkDamage;
    public float experience;
    public int luck;
    public int prestige;
    public List<Weapons> weapons;
    public int gold;
    public int level = 1;
    public bool lostGold = false;
    public bool foughtCopyCat = false;
    public bool savedElfWarrior = false;
    public bool foundPeoplesSword = false;
    public bool tookOldMansChallenge = false;
    public bool hallOfFame = false;
    public int freeGuildStats = 10;
}

public class highScore
{
    public static int rogueScore=0;
    public static int mageScore=0;
    public static int warriorScore=0;
    public static int archerScore=0;
    public static int magicalArcherScore=0;
    
    public static string rogueScoreName="None";
    public static string mageScoreName="None";
    public static string warriorScoreName="None";
    public static string archerScoreName="None";
    public static string magicalArcherScoreName="None";
}

public class Weapons
{
    public string name;
    public string rarity;
    public int classHero;
    public int atk;
    public int luck;
    public int hp;
    public int prestige;
    
    
}

public static class Level
{
    public static int MaxEXP = 5;

    public static void LevelUp(Hero Player)
    {
        while (Player.experience>=MaxEXP)
        {
            Player.experience -= MaxEXP;
            MaxEXP *= 2;
            Player.level += 1;

            Random rd = new Random();
            int statBoost = rd.Next(0, 3);
            int boostHp;
            int boostATK;
            int boostLuck;

            if (statBoost == 0) 
            {
                boostLuck = 3;
                boostHp = 3;
                boostATK = 2;
            } 
            else if (statBoost == 1) 
            {
                boostLuck = 1;
                boostHp = 7;
                boostATK = 2;
            }
            else 
            { 
                boostLuck = 2;
                boostHp = 3;
                boostATK = 5;
            }
            Player.luck += boostLuck; 
            Player.hp += boostHp; 
            Player.atkDamage += boostATK;
            Console.WriteLine("- You leveled up! You obtained "+boostHp+"hp, "+boostATK+"atk and "+boostLuck+" luck");
        
        
            allMonsters.LevelUpMonsters();
        }
    }
}

public static class obtainableWeapons
{
    public static List<Weapons> allWeapons;

    public static void initializeWeaponList()
    {
        //classHero : 1 is warrior; 2 is mage; 3 is archer; 4 is rogue
        
        allWeapons = new List<Weapons>(); //0
        Weapons w1 = new Weapons();
        w1.name = "beginner sword";
        w1.rarity = "common";
        w1.classHero = 1;
        w1.atk = 2;
        w1.luck = 0;
        w1.hp = 0;
        w1.prestige = 0;
        allWeapons.Add(w1);
        
        Weapons w2 = new Weapons(); //1
        w2.name = "beginner staff";
        w2.rarity = "common";
        w2.classHero = 2;
        w2.atk = 2;
        w2.luck = 0;
        w2.hp = 0;
        w2.prestige = 0;
        allWeapons.Add(w2);
        
        Weapons w3 = new Weapons(); //2
        w3.name = "beginner bow";
        w3.rarity = "common";
        w3.classHero = 3;
        w3.atk = 2;
        w3.luck = 0;
        w3.hp = 0;
        w3.prestige = 0;
        allWeapons.Add(w3);
        
        Weapons w4 = new Weapons(); //3
        w4.name = "beginner dagger";
        w4.rarity = "common";
        w4.classHero = 4;
        w4.atk = 2;
        w4.luck = 0;
        w4.hp = 0;
        w4.prestige = 0;
        allWeapons.Add(w4);
        
        Weapons wRare1 = new Weapons(); //4
        wRare1.name = "the people's will";
        wRare1.rarity = "legendary";
        wRare1.classHero = 0; //all classes
        wRare1.atk = 7;
        wRare1.luck = 7;
        wRare1.hp = 7;
        wRare1.prestige = 7;
        allWeapons.Add(wRare1);
        
        Weapons wRare2 = new Weapons(); //5
        wRare2.name = "void&oblivion";
        wRare2.rarity = "legendary";
        wRare2.classHero = 2; 
        wRare2.atk = 16;
        wRare2.luck = 2;
        wRare2.hp = 2;
        wRare2.prestige = 4;
        allWeapons.Add(wRare2);
        
        Weapons wRare3 = new Weapons(); //6
        wRare3.name = "bow of multiple arrows";
        wRare3.rarity = "rare";
        wRare3.classHero = 3; 
        wRare3.atk = 8;
        wRare3.luck = 3;
        wRare3.hp = 1;
        wRare3.prestige = 2;
        allWeapons.Add(wRare3);
        
        Weapons wRare4 = new Weapons(); //7
        wRare4.name = "staff of demise";
        wRare4.rarity = "epic";
        wRare4.classHero = 1; 
        wRare4.atk = 12;
        wRare4.luck = 1;
        wRare4.hp = 1;
        wRare4.prestige = 1;
        allWeapons.Add(wRare4);
    }
}

