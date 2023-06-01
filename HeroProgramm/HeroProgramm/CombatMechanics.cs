namespace HeroProgramm;

public class CombatMechanics
{
    public static void Fight(Hero Player, Monster enemy)
    {
        if (enemy.monsterType == "mimic")
        {
            enemy.luck = Player.luck;
            enemy.monsterHP = Player.hp;
            enemy.monsterATK = Player.atkDamage;
        }
        if (Player.prestige+Player.luck>enemy.luck)
            HeroFight(Player, enemy);
        else
        {
            MonsterFight(Player, enemy);
        }
    }

    public static void MonsterFight(Hero Player, Monster enemy)
    {
        Console.WriteLine("- The " + enemy.monsterName+" is trying to attack you!\n" +
                          "  It has "+enemy.monsterHP+"hp "+" and "+enemy.monsterATK+"atk\n" +
                          "  Do you wish to :\n" +
                          "  Parry | Dodge | Attack");
        string action = Console.ReadLine();
        Console.Clear();
        Random rd = new Random();
        Random rdv = new Random();

        if (enemy.luck < 0)
            enemy.luck = 0;
        if (Player.luck < 0)
            Player.luck = 0;
        int enemyLuck = rd.Next(0, enemy.luck);
        int heroLuck = rdv.Next(0, Player.luck);
        
        switch (action)
        {
            case "2": //dodge
                if (heroLuck >= enemyLuck)
                {
                    Console.WriteLine("- You have successfully dodged the enemy's attack!\n" +
                                      "  You have lost 1 luck point.");
                    Player.luck -= 1;
                }
                else
                {
                    Console.WriteLine("- You have taken "+enemy.monsterATK+" damage.");
                    Player.hp -= enemy.monsterATK;
                }
                CheckDeath(Player, enemy.monsterName);
                break;
            case "status":
                GameLogic.Status(Player);
                MonsterFight(Player,enemy);
                break;
            case "3":  //attack gamble
                Console.WriteLine("- You have taken "+enemy.monsterATK+" damage.\n" +
                                  "  The "+enemy.monsterName+" has taken "+Player.atkDamage/2);
                Console.WriteLine("- You have entered a berserk state, your stats will be increased\n" +
                                  "  at random proportionally to half your damage taken");
                enemy.monsterHP -= Player.atkDamage/2;
                Scenarios.StatPoint(Player, enemy.monsterATK/3);
                Player.hp -= enemy.monsterATK;
                if (Player.hp < 0)
                    Player.hp = 0;
                Console.WriteLine("- You have "+Player.hp+"hp remaining.");
                CheckDeath(Player, enemy.monsterName);
                CheckDeathMonster(enemy);
                break;
            //parry
            default:
                Console.WriteLine("- You have taken "+enemy.monsterATK/2+" damage.");
                Player.hp -= enemy.monsterATK/2;
                CheckDeath(Player, enemy.monsterName);
                break;
        }
        if (enemy.alive)
            HeroFight(Player, enemy);
        else
        {
            DefeatedMonster(Player, enemy);
        }
    }
    
    public static void HeroFight(Hero Player, Monster enemy)
    {
        bool fled = false;
        Console.WriteLine("- You have the initiative!\n" +
                          "  The monster has "+enemy.monsterHP+"hp "+" and "+enemy.monsterATK+"atk\n" +
                          "  Do you wish to :\n" +
                          "  Attack | Flee");
        string action = Console.ReadLine();
        Console.Clear();
        Random rd = new Random();
        
        if (enemy.luck < 0)
            enemy.luck = 0;
        if (Player.luck < 0)
            Player.luck = 0;
        
        int enemyLuck = rd.Next(0, enemy.luck);
        int heroLuck = rd.Next(0, Player.luck);

        switch (action)
        {
            case "2": //flee
                Console.Write("- You have fled the fight. You have lost ");
                if (Player.gold > 50)
                {
                    Player.gold -= 50;
                    Console.Write("50 gold and ");
                }
                fled = true;

                int loss = enemy.giveEXP / 2;
                Console.Write(loss+" experience.");
                Player.experience -= loss;
                break;
            case "status":
                GameLogic.Status(Player);
                HeroFight(Player,enemy);
                break;
            //attack
            default:
                if (heroLuck > enemyLuck + 5)
                {
                    Console.WriteLine("- You have successfully dealt a critical blow!\n" +
                                      "  The "+enemy.monsterName+" was dealt "+(Player.atkDamage+heroLuck)+" damage!");
                    enemy.monsterHP -= (Player.atkDamage+heroLuck);
                }
                else
                {
                    Console.WriteLine("- You have dealt "+Player.atkDamage+" damage.");
                    enemy.monsterHP -= Player.atkDamage;
                }
                CheckDeathMonster(enemy);
                break;
            
        }
        if (fled)
            return;
        if (enemy.alive)
            MonsterFight(Player, enemy);
        else
        {
            DefeatedMonster(Player, enemy);
        }
    }
    
    public static void CheckDeath(Hero Player, string deathCause)
    {
        if (Player.hp<=0)
            Scenarios.Death(deathCause, Player);
    }
    
    public static void CheckDeathMonster(Monster enemy)
    {
        if (enemy.monsterHP <= 0)
            enemy.alive = false;
    }

    public static void DefeatedMonster(Hero Player, Monster enemy)
    {
        Player.experience += enemy.giveEXP;
        Player.gold += enemy.monsterATK * 10;
        Console.WriteLine("- You have defeated a "+enemy.monsterName+" and obtained "+enemy.giveEXP+"exp " +
                          "  and "+(enemy.monsterATK*10)+"gold.");
        if (Player.experience>Level.MaxEXP)
        {
            Level.LevelUp(Player);
        }
    }
    
}