// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Bots
{
    public class PizzaChatBot : ActivityHandler

    {
        public const string WelcomeText = "안녕하세요 ILIKEPIZZA봇입니다.";

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            await SendWelcomeMessageAsync(turnContext, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var text = turnContext.Activity.Text.ToLowerInvariant();
            var responseText = ProcessInput(text);
            await turnContext.SendActivityAsync(responseText, cancellationToken: cancellationToken);

            if (text == "피자 주문")
            {
                await SendPizzaMenuAsync(turnContext, cancellationToken);
            }
            else if (text == "콜라 주문")
            {
                await SendColaMenuAsync(turnContext, cancellationToken);
            }
            else if (text == "치즈 피자")
            {
                await ProcessPizzaOrder(turnContext, "치즈 피자", cancellationToken);
            }
            else if (text == "페퍼로니 피자")
            {
                await ProcessPizzaOrder(turnContext, "페퍼로니 피자", cancellationToken);
            }
            else if (text == "야채 피자")
            {
                await ProcessPizzaOrder(turnContext, "야채 피자", cancellationToken);
            }
            else if (text == "코카 콜라")
            {
                await ProcessColaOrder(turnContext, "코카 콜라", cancellationToken);
            }
            else if (text == "펩시 콜라")
            {
                await ProcessColaOrder(turnContext, "펩시 콜라", cancellationToken);
            }
            else if (text == "코카 콜라 제로")
            {
                await ProcessColaOrder(turnContext, "코카 콜라 제로", cancellationToken);
            }
            else if (text == "펩시 콜라 제로")
            {
                await ProcessColaOrder(turnContext, "펩시 콜라 제로", cancellationToken);
            }
            else
            {
                await SendSuggestedActionsAsync(turnContext, cancellationToken);
            }
        }

        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync($"{member.Name}. {WelcomeText}", cancellationToken: cancellationToken);
                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                }
            }
        }

        private static string ProcessInput(string text)
        {
            const string mainText = "을 선택하셨습니다.";
            switch (text)
            {
                case "피자 주문":
                    {
                        return $"{text} {mainText}";
                    }

                case "콜라 주문":
                    {
                        return $"{text} {mainText}";
                    }

                case "배달 조회":
                    {
                        return "방금 출발했습니다.";
                    }
                case "주문 종료":
                    {
                        return "주문을 종료하겠습니다. 감사합니다.";
                    }

                default:
                    {
                        return $"{text}{mainText}";
                    }
            }
        }

        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("작업을 선택해 주세요");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "피자 주문", Type = ActionTypes.ImBack, Value = "피자 주문", Image = "https://images.pexels.com/photos/2147491/pexels-photo-2147491.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", ImageAltText = "피자 주문" },
                    new CardAction() { Title = "콜라 주문", Type = ActionTypes.ImBack, Value = "콜라 주문", Image = "https://images.pexels.com/photos/50593/coca-cola-cold-drink-soft-drink-coke-50593.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", ImageAltText = "콜라 주문" },
                    new CardAction() { Title = "배달 조회", Type = ActionTypes.ImBack, Value = "배달 조회", Image = "https://images.pexels.com/photos/4393426/pexels-photo-4393426.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2", ImageAltText = "배달 조회" },
                    new CardAction() { Title = "주문 종료", Type = ActionTypes.ImBack, Value = "주문 종료" },
                },
            };

            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task SendPizzaMenuAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("피자를 선택해 주세요");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "치즈 피자", Type = ActionTypes.ImBack, Value = "치즈 피자" },
                    new CardAction() { Title = "페퍼로니 피자", Type = ActionTypes.ImBack, Value = "페퍼로니 피자" },
                    new CardAction() { Title = "야채 피자", Type = ActionTypes.ImBack, Value = "야채 피자" },
                },
            };

            await turnContext.SendActivityAsync(reply, cancellationToken);
        }

        private static async Task ProcessPizzaOrder(ITurnContext<IMessageActivity> turnContext, string pizzaType, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text($"{pizzaType}가 주문되었습니다.");
            await turnContext.SendActivityAsync(reply, cancellationToken);

            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }

        private static async Task ProcessColaOrder(ITurnContext<IMessageActivity> turnContext, string colaType, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text($"{colaType}가 주문되었습니다.");
            await turnContext.SendActivityAsync(reply, cancellationToken);

            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }


        private static async Task SendColaMenuAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("콜라를 선택해 주세요");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction() { Title = "코카 콜라", Type = ActionTypes.ImBack, Value = "코카 콜라" },
                    new CardAction() { Title = "펩시 콜라", Type = ActionTypes.ImBack, Value = "펩시 콜라" },
                    new CardAction() { Title = "코카 콜라 제로", Type = ActionTypes.ImBack, Value = "코카 콜라 제로" },
                    new CardAction() { Title = "펩시 콜라 제로", Type = ActionTypes.ImBack, Value = "펩시 콜라 제로" },
                },
            };

            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
    }
}
