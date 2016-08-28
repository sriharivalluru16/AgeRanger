Feature: PersonApi
	In order to verify the Person Api calls
	As a User
	I want to perform CURD operations

Background: 
	Given Configure the AgeRranger Web API URL to 'http://localhost:8181/'
	And Ensure the AgeRranger Web API is up and running


Scenario: Add a person into the system
Given first name, last name and age of person are 'Age', 'Ranger' and 20
When post request is made to person API
Then person should be added

Scenario: Update a person first name into the system
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When first name is updated to 'AgeEdited'
Then person first name should be 'AgeEdited'

Scenario: Update a person last name into the system
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When last name is updated to 'RangerEdited'
Then person last name should be 'RangerEdited'

Scenario: Get person by Id
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When request is made with Person Id
Then person is the same

Scenario: Get person who does not exist
When request is made with Person Id who does not exist
Then response should be 'NotFound'

Scenario: Age group is Toddler when age is betweeen 0 and 2
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 1
Then age group of person is 'Toddler'

Scenario: Age group is Child when age is betweeen 2 and 14
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 5
Then age group of person is 'Child'

Scenario: Age group is Teenager when age is betweeen 14 and 20
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 17
Then age group of person is 'Teenager'

Scenario: Age group is Early twenties when age is betweeen 20 and 25
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 24
Then age group of person is 'Early twenties'

Scenario: Age group is Almost thirty when age is betweeen 25 and 30
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 27
Then age group of person is 'Almost thirty'

Scenario: Age group is Very adult when age is betweeen 30 and 50
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 40
Then age group of person is 'Very adult'

Scenario: Age group is Kinda old when age is betweeen 50 and 70
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 60
Then age group of person is 'Kinda old'

Scenario: Age group is Old when age is betweeen 70 and 99
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 80
Then age group of person is 'Old'

Scenario: Age group is Very old when age is betweeen 99 and 110
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 100
Then age group of person is 'Very old'

Scenario: Age group is Crazy ancient when age is betweeen 110 and 199
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 120
Then age group of person is 'Crazy ancient'

Scenario: Age group is Vampire when age is betweeen 199 and 4999
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 200
Then age group of person is 'Vampire'

Scenario: Age group is Kauri tree when age is betweeen 4999 and above
Given a person with first name, last name and age of person are 'Age', 'Ranger' and 20
When age is updated to 5000
Then age group of person is 'Kauri tree'
