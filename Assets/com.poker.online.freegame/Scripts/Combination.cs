using System.Collections.Generic;
using System.Linq;

public static class Combination
{
    private static Card findCard;
    private static Card[] cards;

    private static int IsRoyalFlush()
    {
        List<Card> sequence = new List<Card>();

        if (cards.IsContains(CardValue.ace, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.king, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.queen, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.jack, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.ten, out findCard)) sequence.Add(findCard);

        return sequence.Count == 5 && sequence.ToArray().IsEqualsSuit() ? 10 : -1;
    }

    private static int IsStraightFlush()
    {
        List<Card> sequence = new List<Card>();

        if (cards.IsContains(CardValue.king, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.queen, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.jack, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.ten, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.nine, out findCard)) sequence.Add(findCard);

        return sequence.Count == 5 && sequence.ToArray().IsEqualsSuit() ? 9 : -1;
    }

    private static int IsFourOfKind()
    {
        var group = cards.GroupBy(c => c.CardValue).ToDictionary(y => y.Key, y => y.Count());
        return group.ContainsValue(4) ? 8 : -1;
    }

    private static int IsFlush()
    {
        var group = cards.GroupBy(c => c.CardSuit).ToDictionary(y => y.Key, y => y.Count());
        return group.ContainsValue(5) ? 7 : -1;
    }

    private static int IsFullHouse()
    {
        var group = cards.GroupBy(c => c.CardValue).ToDictionary(y => y.Key, y => y.Count());
        return group.ContainsValue(3) && group.ContainsValue(2) ? 6 : -1;
    }

    private static int IsThreeOfKind()
    {
        var group = cards.GroupBy(c => c.CardValue).ToDictionary(y => y.Key, y => y.Count());
        return group.ContainsValue(3) ? 5 : -1;
    }

    public static int IsStraight(Card[] cardset)
    {
        var intGroup = cardset.Select(card => card.CardIntValue());
        UnityEngine.Debug.Log(intGroup.Count());
        foreach (int i in intGroup)
        {
            //UnityEngine.Debug.Log(i);
        }
        var group = cards.GroupBy(c => c.CardValue).ToDictionary(y => y.Key, y => y.Count());
        return group.ContainsValue(3) ? 4 : -1;
    }

    public static (int,string) GetCombination(Card[] _cards)
    {
        cards = _cards;
        if(IsRoyalFlush() > 0)
        {
            return (IsRoyalFlush(), "Royal Flush");
        }
        else if(IsStraightFlush() > 0)
        {
            return (IsStraightFlush(), "Straight Flush");
        }
        else if (IsFourOfKind() > 0)
        {
            return (IsFourOfKind(), "Four of a kind");
        }
        else if (IsFlush() > 0)
        {
            return (IsFlush(), "Flush");
        }
        else if (IsFullHouse() > 0)
        {
            return (IsFullHouse(), "FullHouse");
        }
        else if (IsThreeOfKind() > 0)
        {
            return (IsThreeOfKind(), "Three of a kind");
        }
        //else if (IsStraight() > 0)
        //{
        //    return (IsStraight(), "Straight");
        //}
        else
        {
            return (-1, "none");
        }
    }
}