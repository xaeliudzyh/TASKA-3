// Колода
using Deck = System.Collections.Generic.List<Card>;
// Набор карт у игрока
using Hand = System.Collections.Generic.List<Card>;
// Набор карт, выложенных на стол
using Table = System.Collections.Generic.List<Card>;

// Масть
internal enum Suit
{
    Clubs,     //крести
    Diamonds,  //буби
    Hearts,     //черви
    Spades      //пики
}

// Значение
internal enum Rank: uint
{
    Ace=14,
    King=13,
    Queen=12,
    Jack=11,
    Ten=10,
    Nine=9,
    Eight=8,
    Seven=7,
    Six=6
}

// Карта
record Card(Suit Ssuit, Rank Rrank);


// Тип для обозначения игрока (первый, второй)
internal enum Player
{
    PlFirst=0,
    PlSecond=1,
    Draw=2
}

namespace Task1
{
    public class Task1
    {
 /*
 * Реализуйте игру "Пьяница" (в простейшем варианте, на колоде в 36 карт)
 * https://ru.wikipedia.org/wiki/%D0%9F%D1%8C%D1%8F%D0%BD%D0%B8%D1%86%D0%B0_(%D0%BA%D0%B0%D1%80%D1%82%D0%BE%D1%87%D0%BD%D0%B0%D1%8F_%D0%B8%D0%B3%D1%80%D0%B0)
 * Рука — это набор карт игрока. Карты выкладываются на стол из начала "рук" и сравниваются
 * только по значениям (масть игнорируется). При равных значениях сравниваются следующие карты.
 * Набор карт со стола перекладывается в конец руки победителя. Шестерка туза не бьёт.
 *
 * Реализация должна сопровождаться тестами.
 */

        // Размер колоды
        internal const int DeckSize = 36;

        // Возвращается null, если значения карт совпадают
        internal static Player? RoundWinner(Card card1, Card card2)
        {
            if (card1.Rrank == card2.Rrank)
                return null;
            if (card1.Rrank > card2.Rrank)
                return Player.PlFirst;
            return Player.PlSecond;
        }
        
// Возвращает полную колоду (36 карт) в фиксированном порядке
        internal static Deck FullDeck()
        {
            var fullDeck = new Deck();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    fullDeck.Add(new Card(suit,rank));
                }
            }

            return fullDeck;
        }

// Раздача карт: случайное перемешивание (shuffle) и деление колоды пополам
        internal static Dictionary<Player, Hand> Deal(Deck deck)
        {
            var shuffledDeck = deck;
            for (int i = 0; i < DeckSize; i++)
            {
                int j = new Random().Next(i + 1);
                var temp = shuffledDeck[i];
                shuffledDeck[j] = shuffledDeck[i];
                shuffledDeck[i] = temp;
            }
            return new Dictionary<Player, Deck> { {Player.PlFirst, shuffledDeck.GetRange(0, 18)},
                {Player.PlSecond, shuffledDeck.GetRange(18, 36)}};
        }


// Один раунд игры (в том числе спор при равных картах).
// Возвращается победитель раунда и набор карт, выложенных на стол.
        internal static Tuple<Player, Table> Round(Dictionary<Player, Hand> hands)
        {
            var table = new List<Card>();
            Player? rW = null;
            do
            {
                var p1 = hands[Player.PlFirst].First();
                var p2 = hands[Player.PlSecond].First();
                
                hands[Player.PlFirst].RemoveAt(0);
                hands[Player.PlSecond].RemoveAt(0);
                
                table.Add(p1);
                table.Add(p2);
                if(p1.Rrank==p2.Rrank)
                    continue;
                rW = RoundWinner(p1, p2);

            } while (rW == null && (hands[Player.PlFirst].Count > 0 && hands[Player.PlSecond].Count > 0));
            /*if (hands[Player.PlSecond].Count== 0 && hands[Player.PlFirst].Count== 0) 
                return new Tuple<Player, Deck>(Player.Draw, table);
            else
            {
                if (hands[Player.PlSecond].Count== 0)
                    return new Tuple<Player, Deck>(Player.PlFirst, table);
                else
                {
                    if (hands[Player.PlFirst].Count== 0)
                                    return new Tuple<Player, Deck>(Player.PlSecond, table);
                }
            }*/
            if(rW==null)
                return new Tuple<Player, Deck>(Player.Draw, table);
            return new Tuple<Player, Deck>(rW!.Value, table);
        }

// Полный цикл игры (возвращается победивший игрок)
// в процессе игры печатаются ходы
        internal static Player Game(Dictionary<Player, Hand> hands)
        {
            while (hands[Player.PlFirst].Count>0 && hands[Player.PlFirst].Count>0 )
            {
                var round= Round(hands);
                var winner = round.Item1;
                var table = round.Item2;

                foreach (var card in table)
                {
                    if (hands[Player.PlFirst].Contains(card))
                    {
                        hands[Player.PlFirst].Remove(card);
                    }   
                    if (hands[Player.PlSecond].Contains(card))
                    {
                        hands[Player.PlSecond].Remove(card);
                    } 
                }
                hands[winner].AddRange(table);

            }

            if (hands[Player.PlFirst].Count == 0 && hands[Player.PlFirst].Count == 0)
                return Player.Draw;
            if (hands[Player.PlFirst].Count == 0)
                return Player.PlSecond;
            return Player.PlFirst;


        }


        public static void Main(string[] args)
        {
            var deck = FullDeck();
            var hands = Deal(deck);
            var winner = Game(hands);
            Console.WriteLine($"Победитель: {winner}");
        }
    }
}