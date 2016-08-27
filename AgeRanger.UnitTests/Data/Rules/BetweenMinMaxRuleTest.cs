namespace AgeRanger.UnitTests.Data.Rules
{
  using AgeRanger.Data.Models;
  using AgeRanger.Data.Rules;
  using NUnit.Framework;
  using Should;

  [TestFixture]
  public class BetweenMinMaxRuleTest
  {
    [Test]
    public void BetweenMinMaxTestForPersonWithAge20_ShouldMatchTestGroup()
    {
      var rule = new BetweenMinMaxRule<Person>
      {
        Min = 10, Max= 30, Name = "TestGroup"
      };
      var person = new Person
      {
        Age = 20
      };

      rule.IsSatisfied(person).ShouldBeTrue();
    }

    [Test]
    public void BetweenMinMaxTestForPersonWithAge30_ShouldNotMatchTestGroup()
    {
      var rule = new BetweenMinMaxRule<Person>
      {
        Min = 10,
        Max = 30,
        Name = "TestGroup"
      };
      var person = new Person
      {
        Age = 30
      };

      rule.IsSatisfied(person).ShouldBeFalse();
    }
  }
}
