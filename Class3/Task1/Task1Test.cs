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
        var six = new Card(Suit.Spades,Rank.Six);
        var ace = new Card(Suit.Spades,Rank.Ace);
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { Player.PlFirst, new List<Card> {six} },
            { Player.PlSecond, new List<Card> {ace} }
        };
        var gameWinner = Round(hands);
        That(gameWinner, Is.EqualTo(new Tuple<Player, List<Card>>(Player.PlSecond, new List<Card> { six, ace })));
    }

    [Test]
    public void Game2CardsTest()
    {
        var six = TODO<Card>();
        var ace = TODO<Card>();
        Dictionary<Player, List<Card>> hands = new Dictionary<Player, List<Card>>
        {
            { TODO<Player>(), new List<Card> {six} },
            { TODO<Player>(), new List<Card> {ace} }
        };
        var gameWinner = Game(hands);
        That(gameWinner, Is.EqualTo(TODO<Player>()));
    }
    
    private static T TODO<T>()
    {
        throw new NotImplementedException();
    }
}