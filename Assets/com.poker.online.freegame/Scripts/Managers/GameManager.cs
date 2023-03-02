using System.Collections.Generic;
using System.Collections;

using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get => FindObjectOfType<GameManager>();
    }

    private int[] streetCardCounts = { 0, 3, 1, 1};

    [SerializeField] Deck deck;
    [SerializeField] UserDatabase userDatabase;

    private Room Room { get; set; }
    private List<Card> CardsForGame { get; set; }

    public void StartGame()
    {
        Transform parent = GameObject.Find("Environment").transform;
        Room = Instantiate(Resources.Load<Room>("room"), parent);

        List<Profile> profiles = userDatabase.Profiles;

        for (int i = 0; i < Room.players.Length; i++)
        {
            Profile profile = profiles[Random.Range(0, profiles.Count)];
            Room.players[i].Profile = profile;

            profiles.Remove(profile);
        }

        CardsForGame = deck.Cards;
        StartCoroutine(nameof(GameCycle));
    }

    public void ExitRoom()
    {
        StopAllCoroutines();

        if(Room)
        {
            Destroy(Room.gameObject);
        }
    }

    public void Deal()
    {
        if(!Player.IsMyStep)
        {
            return;
        }

        Player.IsMyStep = false;
    }

    public void AddToPot(int amount)
    {
        Room.AddToPot(amount);
    }

    private Card GetCardFromDeck()
    {
        Card card = CardsForGame.GetRandomCard();
        CardsForGame.Remove(card);
        return card;
    }

    private Player GetWinner()
    {
        var tableAndPlayerCards = Room.players.GroupBy(player => new List<Card>()
        {
            player.Cards[0],
            player.Cards[1],

            Room.table.Cards[0],
            Room.table.Cards[1],
            Room.table.Cards[2],
            Room.table.Cards[3],
            Room.table.Cards[4],
        });

        Dictionary<Player, (int, string)> result = new Dictionary<Player, (int, string)>();
        tableAndPlayerCards.ToList().ForEach((data) =>
        {
            result.Add(data.Single(), Combination.GetCombination(data.Key.ToArray()));
        });

        foreach(var res in result)
        {
            res.Key.Combination = res.Value.Item2;
            //Debug.Log($"<color=red>{res.Key.Name}</color> has a combination <color=red>{res.Value.Item2}</color>");
        }

        var winRating = result.Max(res => res.Value.Item1);
        var winCombination = result.Select(res => res.Value).Where(v => v.Item1 == winRating).First();
        //Debug.Log($"Winning combination among players: <color=red>{winCombination.Item2}</color>");

        var winners = result.Where(res => res.Value == winCombination).Select(p => p.Key);

        if (winners.Count() > 1)
        {
            List<Card> winnerCards = new List<Card>();
            winners.ToList().ForEach((data) =>
            {
                winnerCards.AddRange(data.Cards);
            });

            var t = winnerCards.GroupBy(wc => wc.CardValue).ToDictionary(y => y.Key, y => y.Count());
            foreach(var i in t)
            {
                //Debug.Log($"{i.Key} {i.Value}");
            }

            Card HighCard = Combination.GetHighCard(winnerCards.ToArray());
            //Card[] tmp = winners.Select(c => c.Cards.GetMinCard(HighCard)).ToArray();
            //HighCard = Combination.GetHighCard(tmp);

            Player winner = Room.players.Where(player => player.Cards.Contains(HighCard)).First();
            //Debug.Log($"{winner.Name} <color=red>high card </color>{HighCard.CardValue} {HighCard.GetCardStringSuit()}");
            return winner;
        }

        return winners.Single();
    }

    private IEnumerator GameCycle()
    {
        yield return new WaitUntil(() => Room.IsReady);
        yield return StartCoroutine(nameof(DealCards));

        while (true)
        {
            yield return StartCoroutine(nameof(DealTableCards));
            if (Room.streetId >= streetCardCounts.Length)
            {
                foreach(Player p in Room.players)
                {
                    p.HideCards(false);
                    yield return new WaitForSeconds(0.25f);
                }

                Debug.Log($"Winner: {GetWinner().Profile.name}");
                yield return new WaitForSeconds(2.0f);
                foreach (Player p in Room.players)
                {
                    p.Combination = string.Empty;
                }

                Card[] cards = FindObjectsOfType<Card>();
                GameObject center = GameObject.Find("deck");

                foreach (Card card in cards)
                {
                    card.transform.up = transform.parent.up;
                    card.transform.SetParent(center.transform);
                    card.transform.localScale = new Vector3(0.8f, 0.8f, 1);

                    while (card.transform.position != center.transform.position)
                    {
                        card.transform.position =
                           Vector2.MoveTowards(card.transform.position, center.transform.position, 100.0f * Time.deltaTime);

                        yield return null;
                    }
                }

                foreach(Card c in cards)
                {
                    Destroy(c.gameObject);
                }

                Room.ResetPot();
                CardsForGame = deck.Cards;
                Room.table.Clear();

                StopCoroutine(nameof(GameCycle));
                StartCoroutine(nameof(GameCycle));

                Room.streetId = 0;
            }

            Player.IsMyStep = true;
            foreach (Player p in Room.players)
            {
                yield return p.WaitPlayerTurn();
            }

            yield return null;
        }
    }

    private IEnumerator DealCards()
    {
        foreach (Player player in Room.players)
        {
            player.Cards = new Card[]
            {
                GetCardFromDeck(), GetCardFromDeck()
            };

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator DealTableCards()
    {
        int cardCount = streetCardCounts[Room.streetId];
        Card[] cards = new Card[cardCount];
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i] = GetCardFromDeck();
        }

        Room.streetId++;
        Room.table.Cards = cards.ToList();

        yield return null;
    }
}
