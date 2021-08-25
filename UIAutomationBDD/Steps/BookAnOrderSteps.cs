using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UIAutomationBDD.Pages;

namespace UIAutomationBDD.Steps
{
    [Binding]
    public sealed class BookAnOrderSteps
    {
        OrderPage orderPage = null;

        [Given(@"I launch the automationpractice web application")]
        public void GivenILaunchTheAutomationpracticeWebApplication()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            orderPage = new OrderPage(driver);
        }

        [Given(@"I click on sign in button")]
        public void GivenIClickOnSignInButton()
        {
            orderPage.ClickSignIn();
        }

        [When(@"I sign in using the following details")]
        public void WhenISignInUsingTheFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            orderPage.ClickSubmitSignInButton((string)data.UserName,(string)data.Password);
        }

        [Then(@"I see user account home page")]
        public void ThenISeeUserAccountHomePage()
        {
            Assert.That(orderPage.VerifyUserHomePage(),Is.True);
        }

        [Then(@"I click on '(.*)' tab")]
        public void ThenIClickOnTab(string tabName)
        {
            orderPage.SelectTshirtTab(tabName);
        }


        [Then(@"I choose avaliable T-shirt")]
        public void ThenIChooseAvaliableT_Shirt()
        {
            orderPage.ChooseTshirt();
        }

        [Then(@"I click Add to cart and proceed to checkout")]
        public void ThenIClickAddToCartAndProceedToCheckout()
        {
            orderPage.AddToCart();
        }

        [Then(@"Continue proceed to checkout and finish the order")]
        public void ThenContinueProceedToCheckoutAndFinishTheOrder()
        {
            orderPage.BookOrder();
        }

        [Then(@"verify booked order details in the order history page")]
        public void ThenVerifyBookedOrderDetailsInTheOrderHistoryPage()
        {
           orderPage.VerifyOrderHistoryPage();
        }


    }
}
