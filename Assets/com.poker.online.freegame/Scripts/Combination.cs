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

    private static int IsStraight()
    {
        var intGroup = cards.Select(card => card.CardIntValue());
        int min = intGroup.Min();

        bool second = intGroup.Contains(min + 1);
        bool third = intGroup.Contains(min + 2);
        bool four = intGroup.Contains(min + 3);
        bool five = intGroup.Contains(min + 4);

        return second && third && four && five ? 4 : -1;
    }

    private static int IsTwoPair()
    {
        var intGroup = cards.Select(card => card.CardIntValue());

        var duplicateGroup = intGroup.GroupBy(i => i).ToDictionary(y => y.Key, y => y.Count());
        int pairCount = duplicateGroup.Where(i => i.Value == 2).Count();

        return pairCount == 2 ? 3 : -1;
    }

    public static int IsPair()
    {
        var intGroup = cards.Select(card => card.CardIntValue());

        var duplicateGroup = intGroup.GroupBy(i => i).ToDictionary(y => y.Key, y => y.Count());
        int pairCount = duplicateGroup.Where(i => i.Value == 2).Count();

        return pairCount == 1 ? 2 : -1;
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
        else if (IsStraight() > 0)
        {
            return (IsStraight(), "Straight");
        }
        else if (IsTwoPair() > 0)
        {
            return (IsTwoPair(), "TwoPair");
        }
        else if (IsPair() > 0)
        {
            return (IsPair(), "Pair");
        }
        else
        {
            return (-1, "none");
        }
    }

    public static Card GetHighCard(Card[] userCards)
    {
        var intGroup = userCards.Select((card) => card.CardIntValue());

        int max = intGroup.Where(i => i == intGroup.Max()).First();
        int maxIndex = intGroup.ToList().FindIndex(i => i == max);

        return userCards[maxIndex];
    }
}