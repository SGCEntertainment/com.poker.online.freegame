using System.Linq;

public static class CardArrayExtension
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
        findCard = cards.First(card => card.CardValue == cardValue);
        return cards.All(card => card.CardValue == cardValue);
    }
}
