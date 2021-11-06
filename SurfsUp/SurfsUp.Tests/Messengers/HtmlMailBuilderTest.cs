using FluentAssertions;
using SurfsUp.SurfsUp.Messengers;
using System;
using System.Collections.Generic;
using Xunit;

namespace SurfsUp.Tests.Messengers
{
    public class HtmlMailBuilderTest
    {
        [Fact]
        public void Test_BuildHtmlMail()
        {
            // Arrange
            var messages = new List<Message>()
            {
                new Message()
                {
                    Dates = new HashSet<DayOfWeek>()
                    {
                        DayOfWeek.Monday,
                        DayOfWeek.Tuesday
                    },
                    SpotName = "Vieux Bouceau",
                    SpotUrl = "https://magicseaweed.com/Vieux-Boucau-Surf-Report/64/"
                },
                new Message()
                {
                    Dates = new HashSet<DayOfWeek>()
                    {
                        DayOfWeek.Wednesday,
                        DayOfWeek.Thursday
                    },
                    SpotName = "Gerra",
                    SpotUrl = "https://magicseaweed.com/Gerra-Surf-Report/4393/"
                },
                new Message()
                {
                    Dates = new HashSet<DayOfWeek>()
                    {
                        DayOfWeek.Friday,
                        DayOfWeek.Saturday
                    },
                    SpotName = "Ansedonia",
                    SpotUrl = "https://magicseaweed.com/Ansedonia-La-Sinistra-Surf-Report/3586/"
                }
            };

            var htmlMailBuilder = new HtmlMailBuilder();

            // Act
            string htmlMail = htmlMailBuilder.BuildHtmlMail(messages);

            // Assert 
            htmlMail.Should().NotBeNullOrEmpty();
        }
    }
}
