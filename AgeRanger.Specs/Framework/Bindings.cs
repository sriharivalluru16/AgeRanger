namespace AgeRanger.Specs.Framework
{
  using System;
  using System.Net;
  using System.Net.Http;
  using Newtonsoft.Json;
  using Should;
  using TechTalk.SpecFlow;
  using System.Linq;
  using System.Collections;
  using System.Collections.Generic;

  [Binding]
  public class Bindings
  {
    private Api api;
    private string personApiUri = "api/person/";
    private string ageGroupApiUri = "api/agegroup/fetch/";
   
    [Given(@"Configure the AgeRranger Web API URL to '(.*)'")]
    public void GivenConfigureTheAgeRrangerWebAPIURLTo(string apiUrl)
    {
      this.api = new Api(apiUrl);
    }

    [Given(@"Ensure the AgeRranger Web API is up and running")]
    public void GivenEnsureTheAgeRrangerWebAPIIsUpAndRunning()
    {
      
      this.api.GetResponse();
      var result = ScenarioContext.Current.Get<HttpResponseMessage>("ResponseMessage");
      result.StatusCode.ShouldEqual(HttpStatusCode.OK);
    }

    [Given(@"first name, last name and age of person are '(.*)', '(.*)' and (.*)")]
    public void GivenFirstNameLastNameAndAgeOfPersonAreAnd(string p0, string p1, int p2)
    {
      var person = new Data.Models.Person()
      {
        FirstName = p0,
        LastName = p1,
        Age = p2
      }; 
      ScenarioContext.Current.Set(person, "person"); 
    }


    [Given(@"a person with first name, last name and age of person are '(.*)', '(.*)' and (.*)")]
    public void GivenAPersonWithFirstNameLastNameAndAgeOfPersonAreAnd(string p0, string p1, int p2)
    {
      this.SaveOrUpdatePerson(p0, p1, p2);

      ScenarioContext.Current.Set(this.ExtractPersonFromResponse(), "person");
    }

    private void SaveOrUpdatePerson(string firstName, string lastName, int age)
    {
      var person = new Data.Models.Person()
      {
        FirstName = firstName,
        LastName = lastName,
        Age = age
      };

      this.api.GetResponse(this.personApiUri, person);
    }


    [When(@"post request is made to person API")]
    public void WhenAddRequestIsMadeToPersonAPI()
    {
      var person = ScenarioContext.Current.Get<Data.Models.Person>("person");

      this.api.GetResponse(this.personApiUri, person);
    
    }

    [Then(@"person should be added")]
    public void ThenPersonShouldBeAdded()
    {
      var result = ScenarioContext.Current.Get<HttpResponseMessage>("ResponseMessage");
      var person = this.ExtractPersonFromResponse();
      person.Id.ShouldBeGreaterThan(0);

      result.StatusCode.ShouldEqual(HttpStatusCode.Created);
    }

    private Data.Models.Person ExtractPersonFromResponse()
    {
      var personResponseString = ScenarioContext.Current.Get<string>("ResponseBody");
      var person = JsonConvert.DeserializeObject<Data.Models.Person>(personResponseString);
      return person;
    }

    private List<Data.Models.Person> ExtractPersonsFromResponse()
    {
      var personResponseString = ScenarioContext.Current.Get<string>("ResponseBody");
      var persons = JsonConvert.DeserializeObject<List<Data.Models.Person>>(personResponseString);
      return persons;
    }

    private List<Data.Rules.BetweenMinMaxRule<Data.Models.Person>> ExtractAgeFGroupsFromResponse()
    {
      var personResponseString = ScenarioContext.Current.Get<string>("ResponseBody");
      var rules = JsonConvert.DeserializeObject<List<Data.Rules.BetweenMinMaxRule<Data.Models.Person>>>(personResponseString);
      return rules;
    }

    [When(@"first name is updated to '(.*)'")]
    public void WhenFirstNameIsUpdatedTo(string p0)
    { 
      var person = ScenarioContext.Current.Get<Data.Models.Person>("person");
      person.FirstName = p0;
      ScenarioContext.Current.Set(person, "person");

      this.api.GetResponse(this.personApiUri, person);
    }

    [When(@"last name is updated to '(.*)'")]
    public void WhenLastNameIsUpdatedTo(string p0)
    {
      var person = ScenarioContext.Current.Get<Data.Models.Person>("person");
      person.LastName = p0;
      ScenarioContext.Current.Set(person, "person");

      this.api.GetResponse(this.personApiUri, person);
    }

    [When(@"age is updated to (.*)")]
    public void WhenAgeIsUpdatedTo(int p0)
    {
      var person = ScenarioContext.Current.Get<Data.Models.Person>("person");
      person.Age = p0;
      ScenarioContext.Current.Set(person, "person");

      this.api.GetResponse(this.personApiUri, person);
    }

    [Then(@"person first name should be '(.*)'")]
    public void ThenPersonFirstNameShouldBe(string p0)
    {
      var person = this.ExtractPersonFromResponse();
      person.FirstName.ShouldEqual(p0);
    }

    [Then(@"person last name should be '(.*)'")]
    public void ThenPersonLastNameShouldBe(string p0)
    {
      var person = this.ExtractPersonFromResponse();
      person.LastName.ShouldEqual(p0);
    }

    [Then(@"person age should be (.*)")]
    public void ThenPersonAgeShouldBe(int p0)
    {
      var person = this.ExtractPersonFromResponse();
      person.Age.ShouldEqual(p0);
    }

    [Then(@"age group of person is '(.*)'")]
    public void ThenAgeGroupOfPersonIs(string p0)
    {
      var groups = p0.Trim().Split(',');
      var person = this.ExtractPersonFromResponse();
      this.api.GetResponse(this.ageGroupApiUri, person);
      var ageGroups = this.ExtractAgeFGroupsFromResponse();

      groups.Length.ShouldEqual(ageGroups.Count());
     var groupsMatch = ageGroups.Where(g => groups.Contains(g.Name));
      groupsMatch.Count().ShouldEqual(groups.Length);
    }

    [When(@"request is made with Person Id")]
    public void WhenRequestIsMadeWithPersonId()
    {
      var person = ScenarioContext.Current.Get<Data.Models.Person>("person");
      ScenarioContext.Current.Set(person,"actualperson");
      this.api.GetResponse(this.personApiUri + person.Id);
     
    }

    [Then(@"person is the same")]
    public void ThenPersonIsTheSame()
    {
      var actual = ScenarioContext.Current.Get<Data.Models.Person>("actualperson");
      var result = ScenarioContext.Current.Get<Data.Models.Person>("person");
      result.ShouldEqual(actual);
    }

    [When(@"request is made with Person Id who does not exist")]
    public void WhenRequestIsMadeWithPersonIdWhoDoesNotExist()
    {
      this.api.GetResponse(this.personApiUri);
      var persons = this.ExtractPersonsFromResponse();

      this.api.GetResponse(this.personApiUri + persons.Last().Id + 1);
      var result = ScenarioContext.Current.Get<HttpResponseMessage>("ResponseMessage");
    }
    [Then(@"response should be '(.*)'")]
    public void ThenResponseShouldBe(string p0)
    {
      var result = ScenarioContext.Current.Get<HttpResponseMessage>("ResponseMessage");
      result.StatusCode.ShouldEqual((HttpStatusCode)Enum.Parse(typeof(HttpStatusCode),p0));
    }


  }
}
