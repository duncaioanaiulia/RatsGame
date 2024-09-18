// See https://aka.ms/new-console-template for more information

/*Imagine a game where one or more rats can attack a player. 
Each individual rat has an Attack value of 1. However, rats attack as a swarm,so each rat's Attack value is equal to the total number of rats in play.
        
Given that the rat enters play through the constructor and leaves play (dies) via its Dispose() method, please implement the Game and Rat classes so that,
at any point in the game, the Attack value of a rat is always consistent.
        
Rules:
- The Game class cannot have properties or fields. It can only contain events and methods.
- The Rat class's Attack is strictly an instance property with the initial value of 1.*/
     
     
using System;
//Starter code
 
public class Game
{
    public event EventHandler AddRat;

    public virtual void OnAddRat(EventArgs e)
    {
        AddRat?.Invoke(this, e);
    }
}

public class Rat : IDisposable
{
    private readonly Game _game;

    public string Id { get; }

    public int Attack { get; private set; } = 1;

    public Rat(Game game, string id)
    {
        _game = game;
        Id = id;

        game.AddRat += new EventHandler(Create);
        game.OnAddRat(EventArgs.Empty);
    }

    private void Create(object obj, EventArgs e)
    {
        Attack++;
    }

    public void Dispose()
    {
        Attack--;
        // todo
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var game = new Game();
        var rat1 = new Rat(game, "rat1");
        var rat2 = new Rat(game, "rat2");
        var rat3 = new Rat(game, "rat3");


        Console.WriteLine("Attack:" + rat1.Attack);
        Console.WriteLine("Attack:" + rat2.Attack);
        Console.WriteLine("Attack:" + rat3.Attack);

        ////////////////Test area (You are not allowed to use anything from this area)////////////////////////////
        TestAttackValueForAllRats(rat1, rat2, rat3);
        rat3.Dispose();
        rat3 = null;
        TestAttackValueForAllRats(rat1, rat2);
        ////////////////Test area (You are not allowed to use anything from this area)////////////////////////////
    }

    private static void TestAttackValueForAllRats(params Rat[] ratsInGame)
    {
        foreach (var rat in ratsInGame)
        {
            if (rat.Attack != ratsInGame.Length)
            {
                throw new Exception($"The rats do not have the same attack! Found rat {rat.Id} Attack = {rat.Attack} should be {ratsInGame.Length}");
            }

        }

        Console.WriteLine("Tests passed successfully!");
    }
}