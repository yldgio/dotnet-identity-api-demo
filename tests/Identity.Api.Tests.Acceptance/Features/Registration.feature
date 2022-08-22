Feature: User Registration

A short summary of the feature


Scenario: 01) A user is registered successfully
	When I request to register a new user with the following correct data 
      | FirstName | LastName | Username | Password |
      | Anyname   | Anylastname | anyname1@email.com | S0meValidpa$$wd |
	Then A user should be able to login with the following credential
      | Username | Password |
      | anyname1@email.com | S0meValidpa$$wd |

Scenario: 02) Cannot register a user twice
    Given the following user in the system
      | FirstName | LastName | Username | Password |
      | Anyname   | Anylastname | anyname2@email.com | S0meValidpa$$wd |
	When I request to register a new user with the following data 
      | FirstName | LastName | Username | Password |
      | Anyname   | Anylastname | anyname2@email.com | S0meValidpa$$wd |
	Then The user is not created and the response contains the error "Username already exists"
