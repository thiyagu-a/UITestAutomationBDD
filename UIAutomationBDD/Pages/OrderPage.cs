using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIAutomationBDD.Pages
{
    public class OrderPage
    {
        public ChromeDriver Driver { get; }

        public OrderPage(ChromeDriver driver)
        {
            Driver = driver;            
        }
        
        protected string UserNameSelector => "#email";
        protected string PasswordSelector => "#passwd";
        protected string SignInSelector => "div.header_user_info a";
        protected string SubmitSignInSelector => "#SubmitLogin";
        protected string UserAccountInfoSelector => "div.center_column h1";

        protected string TabSelector => "#block_top_menu ul li a";

        protected string ProductListViewSelector = ".right-block a.product-name";
        protected string ProductListGridViewSelector = "ul.product_list li"; 
        protected string AddToCartSelector => "#add_to_cart button";
        protected string AddToCartSelectorText => "Add to cart";
        protected string ProceedToCheckoutPopupSelector => "#layer_cart div.button-container a span";
        protected string CheckoutSelector => "p.cart_navigation span";
        protected string ProceedToCheckoutText => "Proceed to checkout";
        protected string ConfirmOrderText => "I confirm my order";
        protected string TermsConditionSelector = "cgv";
        protected string PaymentSelector = "a.cheque";
        protected string OrderSuccessMessageSelector = "p.alert-success";
        protected string OrderSuccessMessage = "Your order on My Store is complete.";
        protected string OrderListSelector = "#order-list tr";


        /* Login to Application */

        public void ClickSignin() => Driver.FindElement(By.CssSelector(SignInSelector)).Click();        
                   
        public void ClickSubmitSignInButton(string userName, string password)
        {
            Driver.FindElement(By.CssSelector(UserNameSelector)).SendKeys(userName);
            Driver.FindElement(By.CssSelector(PasswordSelector)).SendKeys(password);

            Driver.FindElement(By.CssSelector(SubmitSignInSelector)).Click();
        }

        public void ClickSignIn() => Driver.FindElement(By.CssSelector(SignInSelector)).Click();
        
        public bool VerifyUserHomePage()
        {
            string userAccountInfo = Driver.FindElement(By.CssSelector(UserAccountInfoSelector)).Text;
            return userAccountInfo.Equals("MY ACCOUNT");
        }

        /* Order a T-shirt */

        public void SelectTshirtTab(string tabName)
        {          
            var tabList = Driver.FindElements(By.CssSelector(TabSelector));
            tabList.FirstOrDefault(e => e.Text.Equals(tabName))?.Click();

        }

        public void ChooseTshirt()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            var productListViewSelectorElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(ProductListViewSelector)));
            productListViewSelectorElement.Click();
        }
               
        public void AddToCart()
        {
            var addCartText = Driver.FindElement(By.CssSelector(AddToCartSelector)).Text;
            Assert.True(addCartText.ToLower().Equals(AddToCartSelectorText.ToLower()), $"Button is not having {addCartText}");
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var addToCartSelectorElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(AddToCartSelector)));
            addToCartSelectorElement.Click();
            WebDriverWait waitNew = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var proceedToCheckoutPopupSelectorElement = waitNew.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(ProceedToCheckoutPopupSelector)));            
            proceedToCheckoutPopupSelectorElement.Click();
        }

        public void BookOrder()
        {
            ProceedCheckout(ProceedToCheckoutText);

            ProceedCheckout(ProceedToCheckoutText);

            Driver.FindElement(By.Id(TermsConditionSelector)).Click();

            ProceedCheckout(ProceedToCheckoutText);

            Driver.FindElement(By.CssSelector(PaymentSelector)).Click();

            ProceedCheckout(ConfirmOrderText);

            var successMessageText = Driver.FindElement(By.CssSelector(OrderSuccessMessageSelector)).Text;
            Assert.True(successMessageText.ToLower().Equals(OrderSuccessMessage.ToLower()), $"Button doesnt have {successMessageText}");

        }

        public void VerifyOrderHistoryPage()
        {
            Driver.Navigate().GoToUrl("http://automationpractice.com/index.php?controller=history");
            Assert.True(Driver.FindElements(By.CssSelector(OrderListSelector)).Count > 1, "Booked order information not displayed");
        }


        #region Private Start
        private void ProceedCheckout(string fieldName)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10)); 
            var checkoutSelectortextElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(CheckoutSelector)));
            string checkoutSelectortext = checkoutSelectortextElement.Text;
            Assert.True(checkoutSelectortext.ToLower().Equals(fieldName.ToLower()), $"ProceedToCheckoutSelector doesnt have {checkoutSelectortextElement}");
            checkoutSelectortextElement.Click();
        }

        #endregion Private End


    }
}

