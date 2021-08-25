Feature: BookAnOrder
	Order T-Shirt (and Verify in Order History)

@BookAnOrder
Scenario: Book an T-shirt Order
	Given I launch the automationpractice web application
	And I click on sign in button
	When I sign in using the following details
		| UserName              | Password       |
		| thiyagu.a25@gmail.com | Automation_100 |
	Then I see user account home page
	And I click on 'T-SHIRTS' tab
	Then I choose avaliable T-shirt
	And I click Add to cart and proceed to checkout
	And Continue proceed to checkout and finish the order
	Then verify booked order details in the order history page