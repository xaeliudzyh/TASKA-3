using NUnit.Framework;
using static NUnit.Framework.Assert;
using static Task1.Task1;

namespace Task1;

public class Tests
{
    [Test]
    public void RoundWinnerTest()
    {
        That(RoundWinner( new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Spades, Rank.Eight)),
            Is.EqualTo(Player.PlFirst));
        That(RoundWinner( new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Clubs, Rank.King)),
            Is.EqualTo(Player.PlFirst));
        That(RoundWinner( new Card(Suit.Clubs, Rank.Six), new Card(Suit.Spades, Rank.Eight)),
            Is.EqualTo(Player.PlSecond));
        That(RoundWinner( new Card(Suit.Clubs, Rank.Ace), new Card(Suit.Spades, Rank.Ace)),
            Is.EqualTo(null));


    }

    [Test]
    public void FullDeckTest()
    {
        var deck = FullDeck();
        That(deck, Has.Count.EqualTo(DeckSize));
        
    }

    [Test]
    public void RoundTest()
    {

        //test1
        var six = new Card(Suit.Spades, Rank.Six);
        var ace = new Card(Suit.Spades, Rank.Ace);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> { six } },
            { Player.PlSecond, new List<Card> { ace } }
        };
        var gameWinner = Round(hands);
        That(gameWinner, Is.EqualTo(new Tuple<Player, List<Card>>(Player.PlSecond, new List<Card> { six, ace })));
    }

    [Test]
    public void RoundTest2()
    {
        //test2
        var eight = new Card(Suit.Spades, Rank.Eight);
        var seven = new Card(Suit.Hearts, Rank.Seven);
        Dictionary<Player, List<Card>> handss = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> { eight } },
            { Player.PlSecond, new List<Card> { seven } }
        };
        var gameWinnerr = Round(handss);
        That(gameWinnerr, Is.EqualTo(new Tuple<Player, List<Card>>(Player.PlFirst, new List<Card> { eight, seven })));

    }
    
    [Test]
    public void RoundTest3()
    {

    //test3
        var jCard1 = new Card(Suit.Spades,Rank.Jack);
        var jCard2 = new Card(Suit.Hearts,Rank.Jack);
        Dictionary<Player, List<Card>> handsss = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> {jCard1} },
            { Player.PlSecond, new List<Card> {jCard2} }
        };
        var gameWinnerrr = Round(handsss);
        That(gameWinnerrr, Is.EqualTo(new Tuple<Player, List<Card>>(Player.Draw, new List<Card> { jCard1, jCard2 })));
    }

    /*[Test]
    public void Game2CardsTestDraw()
    {
        var jack1 = new Card(Suit.Spades,Rank.Jack);
        var jack2 = new Card(Suit.Hearts,Rank.Jack);
        var six1 = new Card(Suit.Spades,Rank.Six);
        var six2 = new Card(Suit.Hearts,Rank.Six);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> { jack1,six1 } },
            { Player.PlSecond, new List<Card> { jack2,six2 } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.Draw));
    }*/
    
    [Test]
    public void Game2CardsTestPl2()
    {
        var jack = new Card(Suit.Spades,Rank.Jack);
        var ace = new Card(Suit.Hearts,Rank.Ace);
        var six1 = new Card(Suit.Spades,Rank.Six);
        var six2 = new Card(Suit.Hearts,Rank.Six);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> { six1,jack } },
            { Player.PlSecond, new List<Card> { six2,ace } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.PlSecond));
    }
    
    [Test]
    public void Game2CardsTestPl2_1()
    {
        var jack = new Card(Suit.Spades,Rank.Jack);
        var ace = new Card(Suit.Hearts,Rank.Ace);
        var six1 = new Card(Suit.Spades,Rank.Six);
        var six2 = new Card(Suit.Hearts,Rank.Six);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> { jack,six1 } },
            { Player.PlSecond, new List<Card> { ace,six2 } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.PlSecond));
    }
    
    [Test]
    public void Game2CardsTestPl1_1()
    {
        var jack = new Card(Suit.Spades,Rank.Jack);
        var ace = new Card(Suit.Hearts,Rank.Ace);
        var six1 = new Card(Suit.Spades,Rank.Six);
        var six2 = new Card(Suit.Hearts,Rank.Six);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlSecond, new List<Card> { jack,six1 } },
            { Player.PlFirst, new List<Card> { ace,six2 } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.PlFirst));
    }
    
    [Test]
    public void Game2CardsTestPl1_2()
    {
        var jack = new Card(Suit.Spades,Rank.Jack);
        var ace = new Card(Suit.Hearts,Rank.Ace);
        var six1 = new Card(Suit.Spades,Rank.Six);
        var six2 = new Card(Suit.Hearts,Rank.Six);
        var seven1 = new Card(Suit.Hearts, Rank.Seven);
        var king2 = new Card(Suit.Spades, Rank.King);
            ;
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlSecond, new List<Card> { jack,king2,six1 } },
            { Player.PlFirst, new List<Card> { ace,six2,seven1 } }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(Player.PlFirst));
    }
    
    
    
    
    private static T TODO<T>()
    {
        throw new NotImplementedException();
    }
}