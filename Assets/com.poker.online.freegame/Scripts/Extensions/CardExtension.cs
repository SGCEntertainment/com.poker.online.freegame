using System.Collections.Generic;
using System.Linq;

public static class CardExtension
{
    public static bool IsEqualsSuit(this Card[] cards)
    {
        bool IsP = cards.All(card => card.CardSuit == CardSuit.P);
        bool IsC = cards.All(card => card.CardSuit == CardSuit.C);
        bool IsT = cards.All(card => card.CardSuit == CardSuit.T);
        bool IsB = cards.All(card => card.CardSuit == CardSuit.B);

        return IsP || IsC || IsT || IsB;
    }

    public static bool IsContains(this Card[] cards, CardValue cardValue, out Card findCard)
    {
        findCard = cards.FirstOrDefault(card => card.CardValue == cardValue);
        return cards.Any(card => card.CardValue == cardValue);
    }

    public static bool IsContains(this Card[] cards, Card _card)
    {
        return cards.Select(c => c.CardValue == _card.CardValue).First();
    }

    public static Card GetMinCard(this Card[] cards, Card _card)
    {
        return cards.Where(c => c.CardValue != _card.CardValue).First();
    }

    public static Card GetRandomCard(this List<Card> cards)
    {
        return cards[UnityEngine.Random.Range(0, cards.Count)];
    }

    public static int CardIntValue(this Card card) => card.CardValue switch
    {
        CardValue.six => 6,
        CardValue.seven => 7,
        CardValue.eight => 8,
        CardValue.nine => 9,
        CardValue.ten => 10,
        CardValue.jack => 11,
        CardValue.queen => 12,
        CardValue.king => 13,
        CardValue.ace => 14,
        _ => -1,
    };
}
