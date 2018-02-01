using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synoptic.Tests
{
    [TestFixture]
    public class CommandActionFinderInheritanceTests
    {
        [Test]
        public void should_find_actions_in_commands()
        {
            var command = new CommandFinder().FindInType(typeof(CommandActionFinderTestCommand));
            Assert.That(command, Is.Not.Null);
            
            var actions = new CommandActionFinder().FindInCommand(command);
            Assert.That(actions, Is.Not.Null);

            var commandActions = actions as IList<CommandAction> ?? actions.ToList();

            Assert.That(commandActions.Count(), Is.EqualTo(3));

            Assert.That(command.Name, Is.EqualTo("cmd"));

            Assert.That(commandActions[2].Name, Is.EqualTo("action"));
            Assert.That(commandActions[2].Description, Is.EqualTo("Test action description."));
            Assert.That(commandActions[2].Parameters[0].Description, Is.EqualTo("Test parameter description"));
        }

        internal class CommandActionFinderTestCommand : CommandActionFinderTestCommandBase
        {
            [CommandAction]
            public void MyAction(string param1)
            {
            }

            [CommandAction]
            public void MyOtherAction(string param1)
            {
            }

            public override void MyLastAction(string param1)
            {
            }
        }

        [Command(Name = "cmd", Description = "Test command description.")]
        internal abstract class CommandActionFinderTestCommandBase
        {
            [CommandAction(Name = "action", Description = "Test action description.")]
            public virtual void MyLastAction(
                [CommandParameter(Description = "Test parameter description")] string param1)
            {
            }
        }
    }
}