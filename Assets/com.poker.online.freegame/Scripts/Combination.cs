using System.Collections.Generic;

public static class Combination
{
    private static Card findCard;

    public static int IsRoyalFlush(Card[] cards)
    {
        List<Card> sequence = new List<Card>();

        if (cards.IsContains(CardValue.ace, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.king, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.queen, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.jack, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.ten, out findCard)) sequence.Add(findCard);

        return sequence.Count == 5 && sequence.ToArray().IsEqualsSuit() ? 10 : -1;
    }

    public static int IsStraightFlush(Card[] cards)
    {
        List<Card> sequence = new List<Card>();

        if (cards.IsContains(CardValue.king, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.queen, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.jack, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.ten, out findCard)) sequence.Add(findCard);
        if (cards.IsContains(CardValue.nine, out findCard)) sequence.Add(findCard);

        return sequence.Count == 5 && sequence.ToArray().IsEqualsSuit() ? 9 : -1;
    }
}