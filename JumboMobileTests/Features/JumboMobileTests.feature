@Android
Feature: JumboMobileAppTests

Background: 
	Given Jumbo mobile app opened

@Login
Scenario Outline: Login with an existing customer
	Given User is NOT logged into the app
	When User taps on "Inloggen" button on 'Welcome' screen
	Then User redirected to Login screen
	When User taps "Inloggen" button on 'Login' screen
	Then Warning about required fields displayed
	And Warning about required username displayed
	And Warning about required password displayed
	#When User enters valid username into username text input
	#And User taps "Inloggen" button on 'Login' screen
	#Then Warning about required fields displayed
	#And Warning about requred username does NOT displayed
	#And Warning about requred password displayed
	#When User removes username from username text input
	When User enters valid password into password text input
	And User taps "Inloggen" button on 'Login' screen
	Then Warning about required fields displayed
	And Warning about required username displayed
	And Warning about required password does NOT displayed
	When User enters valid username into username text input
	And User enters valid password into password text input
	And User taps "Inloggen" button on 'Login' screen for login
	Then User logged into the app
	And User greeted by their first name "Hallo <first name>" on the home page

Examples: 
| first name |
| Andrei     |

@Smoke
Scenario: Open the Jumbo app
	Given User logged into the app
	Then "Mijn Jumbo", "Producten", "Recepten", "Aanbiedingen" and "Mandje" tabs displayed on navigation bar
	When User taps "Producten" icon on navigation bar
	Then "Producten" screen displayed
	When User taps "Recepten" icon on navigation bar
	Then "Recepten" screen displayed
	When User taps "Aanbiedingen" icon on navigation bar
	Then "Aanbiedingen" screen displayed
	When User taps "Mandje" icon on navigation bar
	Then "Mandje" screen displayed

@Search
Scenario Outline: Search for a specific product
	Given User logged into the app
	And "Mijn Jumbo" screen displayed
	When User performs search by <query> using search field
	Then Search results displayed on screen
	And All search result items has <query> in their names
	And Price is displayed for all search result items
	When User taps "Filteren" button on search result screen
	Then All categories with item count >0 are shown as options to refine the search results

Examples: 
|  query  |
| Unicorn |

@Product
Scenario: Open the product detail page
	Given List of products displayed
	When User taps on random product item to get more details
	Then The product details screen displayed
	And The price of the product is shown
	And Add To Basket option is available
	And Back button is visible

@Basket
Scenario: Add a product to the basket from the product detail page
	Given The product details screen displayed
	When User add the product to a basket
	Then Item count on basket icon is increased
	When User navigates to the basket
	Then Product is shown in the basket with the correct quantity
	When User removes the product from the basket
	Then The product removed from the basket