public static class Combination
{
    public static int IsRoyalFlush(Card[] cards)
    {
        if(!cards.IsEqualsSuit())
        {
            return -1;
        }

        bool Ace = cards.IsContains(CardValue.ace);
        bool King = cards.IsContains(CardValue.king);
        bool Queen = cards.IsContains(CardValue.queen);
        bool Jack = cards.IsContains(CardValue.jack);
        bool Ten = cards.IsContains(CardValue.ten);

        return Ace && King && Queen && Jack && Ten ? -1 : 10;
    }

    public static int IsStraightFlush(Card[] cards)
    {
        if (!cards.IsEqualsSuit())
        {
            return -1;
        }

        bool King = cards.IsContains(CardValue.king);
        bool Queen = cards.IsContains(CardValue.queen);
        bool Jack = cards.IsContains(CardValue.jack);
        bool Ten = cards.IsContains(CardValue.ten);
        bool Nine = cards.IsContains(CardValue.nine);

        return King && Queen && Jack && Ten && Nine ? -1 : 9;
    }
}