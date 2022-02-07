using KingLemurJulian.Core.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingLemurJulian.Core.Commands
{
    public class RandomActOfKindnessCommand : CommandExecutorBase
    {
        public override string CommandName => "RAK";

        private readonly IMediator mediator;
        private readonly Random rng;

        public RandomActOfKindnessCommand(IMediator mediator)
        {
            this.mediator = mediator;
            rng = new Random();
        }

        public override bool CanExecute(CommandRequest commandRequest)
        {
            if (string.Equals("RandomActOfKindness", commandRequest.CommandText, StringComparison.OrdinalIgnoreCase))
                return true;

            return base.CanExecute(commandRequest);
        }

        public override async Task Execute(CommandRequest commandRequest)
        {
            var index = rng.Next(0, randomActsOfKindness.Count + 1);

            var act = randomActsOfKindness[index];

            await mediator.Send(new CommandResponseRequest(commandRequest, act)).ConfigureAwait(false);
        }

        private readonly IReadOnlyList<string> randomActsOfKindness = new List<string>()
        {
            "Compliment someone today!",
            "Let someone know how much you appreciate them",
            "See someone struggling with lots of bags? Offer to help them",
            "Someone wronged you? Forgive them",
            "Say good morning/afternoon/evening to a stranger",
            "Life can get really busy - take some time out to spend with a family member",
            "Someone looking lost? Help them with directions",
            "Help an elderly person cross the road or up the stairs",
            "Be someone’s shoulder to cry on",
            "Go the day without complaining",
            "Leave someone flowers anonymously",
            "Make an effort to get to know someone you don’t usually talk to",
            "Know someone going through something you’ve been through? Give them advice",
            "Help someone academically – lend them your study notes",
            "Old laptop or mobile lying around? Donate it",
            "Be eco-friendly – unplug electronics when you’re finished using them",
            "Plant a seed",
            "Apologise to someone you may have hurt",
            "Answer the phone in a cheerful voice – even if it is a sales person",
            "We rarely listen to others - ask someone about their day",
            "We all love surprises! Buy someone an unexpected gift",
            "Bake something for your family/friends",
            "We all need help sometimes; offer someone a helping hand",
            "Make a hot beverage for a friend/family",
            "Surprise your parents with flowers",
            "Give up your seat on the tube/bus",
            "Help a younger student with their work",
            "Write a complimentary note for someone",
            "Bake for your neighbour",
            "Offer to help your neighbours/friends with chores",
            "Reconnect with your grandparents or an elderly person you know – give them a call!",
            "It’s hard to stay connected – reach out to an elderly person you know",
            "House chores can be tiring – offer a helping hand",
            "Pay for someone else’s meal today",
            "Save your family some time and buy their groceries",
            "Wardrobe overflowing? Donate clothes to a charity",
            "Have lunch with a homeless person",
            "Surroundings looking messy? Tidy up the area around you",
            "Make a conscious effort to recycle",
            "Purchase ethical goods",
            "Open the door for someone",
            "Help someone struggling with heavy bags",
            "Read a good book recently? Pass it on to someone else",
            "Send flowers to a friend or a family member!",
            "Feeling inspired? Make a meal for your family or roommates",
            "Surprise your siblings with their favourite sweets/chocolate",
            "Know someone who is not coping very well? Give them a call",
            "Send a thank you card to someone who has made a difference in your life (a friend, family member, teacher etc.)",
            "Offer to babysit your siblings/cousins/nephews/nieces etc",
            "Share today’s food with your neighbour!",
            "Gift someone something they complimented you for",
            "Leave a kind message anywhere (in a library book, on a computer etc.)",
            "Buy someone a coffee",
            "Visit a friend who’s sick",
            "Buy more ethically sourced foods",
            "We walk past homeless people every day; can you spare them 5 minutes of your time?",
            "Feeling brave? Give blood",
            "Make amends with someone you may have wronged",
            "Smile at 3 people today",
            "Is that litter on the floor? Pick it up and bin it",
            "Share something interesting you’ve learnt today",
            "Make your voice count - sign a petition for a good cause",
            "Make someone a cup of coffee",
            "Neighbour’s lawn looking messy? Offer to mow it",
            "Remember that family member you haven’t seen for a while? See how they are doing",
            "Taking public transport? Offer your seat to someone else",
            "Forgive someone who has wronged you",
            "Make someone’s day – tell a friend why you appreciate them",
            "Help someone carry their pushchair up/down the stairs",
            "Treat a friend – buy them lunch!",
            "Know someone who’s feeling under the weather? Pay them a visit!",
            "Lend a friend a book you think they’d like",
            "Go green – don’t waste paper",
            "Remember to turn the lights off when you leave a room!",
            "Save water – turn the tap off when brushing your teeth!",
            "Save water – take a shorter shower today",
            "Recycle 3 things today",
            "Put your phone down and have a conversation with a friend",
            "Hug your parents",
            "Google 'survey for charity' and complete one. They receive money for every one you fill out!",
            "Who will be making dinner for your family today? Tag, you’re it!",
            "Help somebody with a chore they need done!",
            "Go out of your way to thank someone today!",
            "Pick up somebody else’ tab next time you go for a coffee",
            "Support a small, local business as a customer",
            "Oooh wait! There’s somebody behind you; hold the door open!",
            "Pay for someone’s bus ticket",
            "Volunteer your time for a good cause",
            "Empty your wallet for charity",
            "Good servicing requires a lot of effort; tip them!",
            "No matter how annoying they can be, tell your siblings how much you appreciate them",
            "Help someone improve, give them constructive feedback",
            "Share your lunch with a friend",
            "It can get lonely when you are old, pay your grandparents a visit",
            "Remember that friend you haven’t seen for ages? Give them a call",
            "Start the day right – make breakfast for everyone",
            "Be proactive – sign a petition for a good cause",
            "Buy your mom a flower",
            "Hold the door for someone!",
            "Smile at a someone",
            "Say “Thank you” to someone who made a difference.",
            "Send a card to people who dedicate their lives to helping us – soldiers, police officers, fire fighters and teachers to name a few.",
            "It’s never too late to say “thanks.”",
            "Send a funny meme",
            "Listen to someone tell how their day was, JUST LISTEN",
            "Help an artist! Buy their art, Like, Share, Comment - Sharing is caring, and goes a long way!",
            "Offer a ride",
            "Post positive notes.",
            "Buy a small gift for someone. Just because.",
            "Don’t ignore the next homeless person you see. Buy him food. Enjoy his smile when you give it to him.",
            "If you print an Internet coupon before going to a store, print a few extras to give to other customers.",
            "Donate a small sum of money to a charity you love.",
            "Leave a Nice Note on Someone’s Car",
            "Read a Book to an Elderly Person",
            "Do not be afraid to give genuine compliments",
            "Let Someone Cut in Front of You in Line",
            "If traveling - Bring Someone a Souvenir",
            "Send a Random Thank You Email to Someone You Admire",
            "Plant a Tree",
            "Help Someone Try Something New",
            "Make Someone Laugh",
            "Talk to the Shy One at a Party - Don't let that corner lurker feel lonely",
            "Help an elderly person with their groceries. - Careful though, some prefer to handle it on their own!",
            "Inspire someone who is a little down."
        };
    }
}
