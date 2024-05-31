using System;
using System.Collections.Concurrent;

namespace Interview
{
    public class VisitorService
    {
        private readonly ConcurrentDictionary<string, bool> _visitors = new ConcurrentDictionary<string, bool>();

        public bool IsReturningVisitor(string name)
        {
            return _visitors.ContainsKey(name);
        }

        public void AddVisitor(string name)
        {
            _visitors.TryAdd(name, true);
        }
    }

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }

    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }

    public class Greeter
    {
        private readonly VisitorService _visitorService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public Greeter(VisitorService visitorService, IDateTimeProvider dateTimeProvider)
        {
            _visitorService = visitorService;
            _dateTimeProvider = dateTimeProvider;
        }

        public string CreateInvitationMessage(string name, string age, bool sex, int friendsCount, bool doNotHaveFriends, string surname)
        {
            string greeting = GetGreeting();
            string title = sex ? "Mr" : "Mrs";
            string friendsInvitation = doNotHaveFriends ? string.Empty : $" and your {friendsCount} friends";
            var message = $"{greeting}\n";

            message += $"{title} {name} {surname}.\n";
            message += $"We are glad to see you{(_visitorService.IsReturningVisitor(name) ? " again." : ".")}\n";
            _visitorService.AddVisitor(name);
            message += $"We want to invite you{friendsInvitation} to the party.";

            return message;
        }

        private string GetGreeting()
        {
            var now = _dateTimeProvider.Now;
            if (now.Hour < 10)
            {
                return "Good morning";
            }
            else if (now.Hour >= 10 && now.Hour < 20)
            {
                return "Good day";
            }
            else
            {
                return "Good evening";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var visitorService = new VisitorService();
            var dateTimeProvider = new SystemDateTimeProvider();
            var greeter = new Greeter(visitorService, dateTimeProvider);

            Console.WriteLine(greeter.CreateInvitationMessage("Kirill", "21", true, 3, false, "Gorbunov"));
            Console.WriteLine(greeter.CreateInvitationMessage("Egor", "22", true, 0, true, "Kakoyto"));
            Console.WriteLine(greeter.CreateInvitationMessage("Ivan", "15", true, 3, false, "Eshekakoyto"));

            Console.ReadLine();
        }
    }
}