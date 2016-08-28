namespace AgeRanger.Specs.Framework
{
  using TechTalk.SpecFlow;

  [Binding]
  public class BindingFramework
  {
    private Api api;

    [BeforeScenario]
    public void BeforeScenario()
    {
      //TODO: implement logic that has to run before executing each scenario
    }

    [AfterScenario]
    public void AfterScenario()
    {
      //TODO: implement logic that has to run after executing each scenario
    }

    [Given(@"Configure the AgeRranger Web API URL to '(.*)'")]
    public void GivenConfigureTheAgeRrangerWebAPIURLTo(string apiUrl)
    {
      this.api = new Api(apiUrl);
    }

    [Given(@"Ensure the AgeRranger Web API is up and running")]
    public void GivenEnsureTheAgeRrangerWebAPIIsUpAndRunning()
    { 
      dynamic response;
      this.api.GetResponse(out response);
    }

    [Given(@"first name, last name and age of person are '(.*)', '(.*)' and (.*)")]
    public void GivenFirstNameLastNameAndAgeOfPersonAreAnd(string p0, string p1, int p2)
    {
      ScenarioContext.Current.Pending();
    }

    [When(@"add request is made to person API")]
    public void WhenAddRequestIsMadeToPersonAPI()
    {
      ScenarioContext.Current.Pending();
    }

    [Then(@"person should be added")]
    public void ThenPersonShouldBeAdded()
    {
      ScenarioContext.Current.Pending();
    }

  }
}
